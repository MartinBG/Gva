using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common.Api.Utils;

namespace Common.Api.Http
{
    public class BlobWriteStream : Stream
    {
        private const int CHUNK_SIZE = 100 * 8040;

        private SqlCommand cmdAppendChunk;
        private SqlCommand cmdFirstChunk;
        private SqlParameter paramChunk;
        private SqlParameter paramLength;

        private int chunkIndex = 0;
        private byte[] chunkBuffer = new byte[CHUNK_SIZE];
        private bool isFirstChunk = true;

        public BlobWriteStream(
            SqlConnection connection,
            SqlTransaction transaction,
            string schemaName,
            string tableName,
            string blobColumn,
            string keyColumn,
            object keyValue)
        {
            this.cmdFirstChunk = new SqlCommand(
                string.Format(@"UPDATE [{0}].[{1}] SET [{2}] = @firstChunk WHERE [{3}] = @key", schemaName, tableName, blobColumn, keyColumn),
                connection,
                transaction);
            this.cmdFirstChunk.Parameters.AddWithValue("@key", keyValue);

            this.cmdAppendChunk = new SqlCommand(
                string.Format(@"UPDATE [{0}].[{1}] SET [{2}].WRITE(@chunk, NULL, @length) WHERE [{3}] = @key", schemaName, tableName, blobColumn, keyColumn),
                connection,
                transaction);
            this.cmdAppendChunk.Parameters.AddWithValue("@key", keyValue);

            this.paramChunk = new SqlParameter("@chunk", SqlDbType.VarBinary);
            this.paramLength = new SqlParameter("@length", SqlDbType.BigInt);
            this.cmdAppendChunk.Parameters.Add(this.paramChunk);
            this.cmdAppendChunk.Parameters.Add(this.paramLength);
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { throw new InvalidOperationException(); }
        }

        public override long Position
        {
            get
            {
                throw new InvalidOperationException();
            }

            set
            {
                throw new InvalidOperationException();
            }
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
        {
            while (count > 0)
            {
                int take = Math.Min(count, CHUNK_SIZE - this.chunkIndex);

                Buffer.BlockCopy(buffer, offset, this.chunkBuffer, this.chunkIndex, take);
                this.chunkIndex += take;
                offset += take;
                count -= take;

                if (this.chunkIndex == CHUNK_SIZE)
                {
                    await this.FlushAsync(cancellationToken);
                }
            }
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            if (this.chunkIndex == 0)
            {
                return;
            }

            byte[] bytesToWrite;

            if (this.chunkIndex < CHUNK_SIZE)
            {
                bytesToWrite = new byte[this.chunkIndex];
                Buffer.BlockCopy(this.chunkBuffer, 0, bytesToWrite, 0, this.chunkIndex);
            }
            else
            {
                bytesToWrite = this.chunkBuffer;
            }

            if (this.isFirstChunk)
            {
                this.cmdFirstChunk.Parameters.AddWithValue("@firstChunk", bytesToWrite);
                this.cmdFirstChunk.ExecuteNonQueryAsync(cancellationToken).Wait();
                this.isFirstChunk = false;
            }
            else
            {
                this.paramChunk.Value = bytesToWrite;
                this.paramLength.Value = this.chunkIndex;
                this.cmdAppendChunk.ExecuteNonQueryAsync(cancellationToken).Wait();
            }

            this.chunkIndex = 0;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.WriteAsync(buffer, offset, count).Wait();
        }

        public override void Flush()
        {
            this.FlushAsync().Wait();
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this.WriteAsync(buffer, offset, count).ToApm(callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            Task task = (Task)asyncResult;
            if (task.Exception != null)
            {
                throw task.Exception.InnerException;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new InvalidOperationException();
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.cmdAppendChunk != null && this.cmdFirstChunk != null)
                {
                    using (this.cmdFirstChunk)
                    using (this.cmdAppendChunk)
                    {
                        this.Flush();
                    }
                }
            }
            finally
            {
                this.cmdFirstChunk = null;
                this.cmdAppendChunk = null;
                base.Dispose(disposing);
            }
        }
    }
}
