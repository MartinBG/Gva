using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using Common.Blob;

namespace Gva.MigrationTool.Blobs
{
    public class BlobUploader : IDisposable
    {
        private bool disposed = false;
        private SqlConnection sqlConn;

        public BlobUploader(SqlConnection sqlConn)
        {
            this.sqlConn = sqlConn;
        }

        public void StartUploading(
            //intput
            BlockingCollection<Tuple<int, MemoryStream>> blobContents,
            //output
            ConcurrentDictionary<int, string> blobIdsToFileKeys,
            BlockingCollection<long> uploadedBytes,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            try
            {
                this.sqlConn.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            foreach (var blobContent in blobContents.GetConsumingEnumerable())
            {
                ct.ThrowIfCancellationRequested();

                var blobId = blobContent.Item1;
                var content = blobContent.Item2;

                try
                {
                    using (var blobWriter = new BlobWriter(sqlConn))
                    {
                        long length;
                        using (var stream = blobWriter.OpenStream())
                        using (content)
                        {
                            length = content.Length;
                            content.CopyTo(stream);
                        }

                        if (!blobIdsToFileKeys.TryAdd(blobId, blobWriter.GetBlobKey().ToString()))
                        {
                            throw new Exception("blobId already present in dictionary");
                        }
                        uploadedBytes.Add(length);
                    }
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
                    this.sqlConn.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
