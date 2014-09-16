using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Oracle.DataAccess.Client;

namespace Gva.MigrationTool.Blobs
{
    public class Blob
    {
        private const string DUMMY_FILE_KEY = "7C0604F9-FB44-4CCD-BE0E-66E82142AE76";

        private Func<Owned<BlobDownloader>> downloaderFactory;
        private Func<Owned<BlobUploader>> uploaderFactory;
        private OracleConnection oracleConn;

        public Blob(
            OracleConnection oracleConn,
            Func<Owned<BlobDownloader>> downloaderFactory,
            Func<Owned<BlobUploader>> uploaderFactory)
        {
            this.downloaderFactory = downloaderFactory;
            this.uploaderFactory = uploaderFactory;
            this.oracleConn = oracleConn;
        }

        public Dictionary<int, string> migrateBlobs()
        {
            this.oracleConn.Open();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            var ids = this.oracleConn.CreateStoreCommand(@"SELECT DOC_ID FROM CAA_DOC.DOCLIB_DOCUMENTS")
                .Materialize(r => r.Field<int>("DOC_ID"));

            Dictionary<int, string> blobIdsToFileKeys;

            if (Migration.IsPartial)
            {
                blobIdsToFileKeys = ids.ToDictionary(id => id, id => DUMMY_FILE_KEY);
            }
            else
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken ct = cts.Token;

                ConcurrentDictionary<int, string> blobIdsToFileKeysConcurrent = new ConcurrentDictionary<int, string>();
                BlockingCollection<Tuple<int, MemoryStream>> blobContents = new BlockingCollection<Tuple<int, MemoryStream>>();
                ConcurrentQueue<int> blobIds = new ConcurrentQueue<int>(ids);
                BlockingCollection<long> downloadedBytes = new BlockingCollection<long>();
                BlockingCollection<long> uploadedBytes = new BlockingCollection<long>();


                Task.WaitAll(
                    Task.WhenAll(
                        Utils.RunParallel("ParallelDownloads", ct,
                            () => this.downloaderFactory().Value,
                            (downloader) =>
                            {
                                using (downloader)
                                {
                                    downloader.StartDownloading(blobIds, blobContents, downloadedBytes, cts, ct);
                                }
                            })
                            .ContinueWith((t) =>
                                {
                                    blobContents.CompleteAdding();

                                    if (t.IsFaulted)
                                    {
                                        throw t.Exception;
                                    }
                                }),
                        Utils.RunParallel("ParallelUploads", ct,
                            () => this.uploaderFactory().Value,
                            (uploader) =>
                            {
                                using (uploader)
                                {
                                    uploader.StartUploading(blobContents, blobIdsToFileKeysConcurrent, uploadedBytes, cts, ct);
                                }
                            }))
                        .ContinueWith((t) =>
                            {
                                downloadedBytes.CompleteAdding();
                                uploadedBytes.CompleteAdding();

                                if (t.IsFaulted)
                                {
                                    throw t.Exception;
                                }
                            }),
                    Task.Run(() => PrintInfo(Console.CursorTop, "Downloaded", downloadedBytes)),
                    Task.Run(() => PrintInfo(Console.CursorTop + 1, "Uploaded", uploadedBytes)));

                blobIdsToFileKeys = blobIdsToFileKeysConcurrent.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }

            timer.Stop();
            Console.WriteLine("Blob migration time - {0}", timer.Elapsed.TotalMinutes);

            return blobIdsToFileKeys;
        }

        private void PrintInfo(int cursorTop, string task, BlockingCollection<long> bytes)
        {
            int count = 0;
            double totalSpeedKBPerSec = 0;
            double periodSpeedKBPerSec = 0;

            Stopwatch totalTimer = new Stopwatch();
            totalTimer.Start();
            long totalBytes = 0;

            Stopwatch periodTimer = new Stopwatch();
            periodTimer.Start();
            long periodBytes = 0;
            long periodMs = 5000;

            foreach (var b in bytes.GetConsumingEnumerable())
            {
                count++;
                totalBytes += b;
                periodBytes += b;

                if (periodTimer.ElapsedMilliseconds > periodMs)
                {
                    periodSpeedKBPerSec = (((double)periodBytes) / 1024) / ((((double)periodTimer.ElapsedMilliseconds) + 1) / 1000);
                    periodBytes = 0;
                    periodTimer.Restart();
                }

                totalSpeedKBPerSec = (((double)totalBytes) / 1024) / ((((double)totalTimer.ElapsedMilliseconds) + 1) / 1000);

                Console.SetCursorPosition(0, cursorTop);
                Console.Write(
                    string.Format("{0,-10} {1,6} | Avg: {2,7} KB/s | Current {3,7} KB/s",
                        task,
                        count,
                        Math.Round(totalSpeedKBPerSec, 1).ToString("0.0"),
                        Math.Round(periodSpeedKBPerSec, 1).ToString("0.0"))
                    .PadRight(80));
            }

            totalTimer.Stop();
            periodTimer.Stop();
        }
    }
}
