using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Blob
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

        private bool WriteChunk(byte[] buffer, ref int offset, ref int count)
        {
            int take = Math.Min(count, CHUNK_SIZE - this.chunkIndex);

            Buffer.BlockCopy(buffer, offset, this.chunkBuffer, this.chunkIndex, take);
            this.chunkIndex += take;
            offset += take;
            count -= take;

            return this.chunkIndex == CHUNK_SIZE;
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            while (count > 0)
            {
                if (this.WriteChunk(buffer, ref offset, ref count))
                {
                    await this.FlushAsync(cancellationToken);
                }
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            while (count > 0)
            {
                if (this.WriteChunk(buffer, ref offset, ref count))
                {
                    this.Flush();
                }
            }
        }

        private byte[] GetChunk()
        {
            if (this.chunkIndex == 0)
            {
                return new byte[0];
            }

            byte[] chunk;

            if (this.chunkIndex < CHUNK_SIZE)
            {
                chunk = new byte[this.chunkIndex];
                Buffer.BlockCopy(this.chunkBuffer, 0, chunk, 0, this.chunkIndex);
            }
            else
            {
                //if we have to write the whole chunk reuse its array
                chunk = this.chunkBuffer;
            }

            this.chunkIndex = 0;

            return chunk;
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            byte[] chunk = this.GetChunk();

            if (chunk.Length == 0)
            {
                return;
            }

            if (this.isFirstChunk)
            {
                this.cmdFirstChunk.Parameters.AddWithValue("@firstChunk", chunk);
                await this.cmdFirstChunk.ExecuteNonQueryAsync(cancellationToken);
                this.isFirstChunk = false;
            }
            else
            {
                this.paramChunk.Value = chunk;
                this.paramLength.Value = this.chunkIndex;
                await this.cmdAppendChunk.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public override void Flush()
        {
            byte[] chunk = this.GetChunk();

            if (chunk.Length == 0)
            {
                return;
            }

            if (this.isFirstChunk)
            {
                this.cmdFirstChunk.Parameters.AddWithValue("@firstChunk", chunk);
                this.cmdFirstChunk.ExecuteNonQuery();
                this.isFirstChunk = false;
            }
            else
            {
                this.paramChunk.Value = chunk;
                this.paramLength.Value = this.chunkIndex;
                this.cmdAppendChunk.ExecuteNonQuery();
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
