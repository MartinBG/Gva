using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using Gva.Api.CommonUtils;
using Oracle.DataAccess.Client;

namespace Gva.MigrationTool.Blobs
{
    public class BlobDownloader : IDisposable
    {
        private bool disposed = false;
        private OracleConnection oracleConn;

        public BlobDownloader(OracleConnection oracleConn)
        {
            this.oracleConn = oracleConn;
        }

        public void StartDownloading(
            //intput
            ConcurrentQueue<int> blobIds,
            RateLimiter rateLimiter,
            //output
            BlockingCollection<Tuple<int, MemoryStream>> blobContents,
            BlockingCollection<long> downloadedBytes,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            try
            {
                this.oracleConn.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            int blobId;
            while (blobIds.TryDequeue(out blobId))
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    MemoryStream ms = new MemoryStream();

                    using (var cmd = (OracleCommand)oracleConn.CreateStoreCommand(
                        @"SELECT CONTENTS FROM CAA_DOC.DOCLIB_DOCUMENTS WHERE {0}",
                        new DbClause("DOC_ID = {0}", blobId)))
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        using (var blob = reader.GetOracleBlob(reader.GetOrdinal("CONTENTS")))
                        {
                            blob.CopyTo(ms);
                            ms.Position = 0;
                        }
                    }

                    downloadedBytes.Add(ms.Length);

                    rateLimiter.Increment(ms.Length);
                    blobContents.Add(Tuple.Create(blobId, ms));
                }
                catch (Exception)
                {
                    cts.Cancel();
                    throw;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            try
            {
                if (disposing && !disposed)
                {
                    this.oracleConn.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
