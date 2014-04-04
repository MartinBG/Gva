using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Blob
{
    public class BlobReadStream : Stream
    {
        private string connectionStringName;
        private SqlConnection connection;
        private SqlCommand cmdReadChunk;
        private SqlDataReader chunkReader;

        private long readerOffset = 0;

        public BlobReadStream(
            string connectionStringName,
            string schemaName,
            string tableName,
            string blobColumn,
            string keyColumn,
            object keyValue)
        {
            this.connectionStringName = connectionStringName;
            this.cmdReadChunk = new SqlCommand(
                string.Format(@"SELECT [{2}] FROM [{0}].[{1}] WHERE [{3}] = @key", schemaName, tableName, blobColumn, keyColumn));
            this.cmdReadChunk.Parameters.AddWithValue("@key", keyValue);
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
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

        private void CreateConnection()
        {
            this.connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.connectionStringName].ConnectionString);
            this.cmdReadChunk.Connection = this.connection;
        }

        private int GetBytes(byte[] buffer, int offset, int count)
        {
            int read = (int)this.chunkReader.GetBytes(0, this.readerOffset, buffer, offset, count);
            this.readerOffset += read;

            return read;
        }

        private void InitReader()
        {
            if (this.chunkReader == null)
            {
                this.CreateConnection();

                this.connection.Open();
                this.chunkReader = this.cmdReadChunk.ExecuteReader(CommandBehavior.SequentialAccess);
                this.chunkReader.Read();
            }
        }

        private async Task InitReaderAsync(CancellationToken cancellationToken)
        {
            if (this.chunkReader == null)
            {
                this.CreateConnection();

                await this.connection.OpenAsync(cancellationToken);
                this.chunkReader = await this.cmdReadChunk.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);
                await this.chunkReader.ReadAsync(cancellationToken);
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            this.InitReader();
            return this.GetBytes(buffer, offset, count);
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await this.InitReaderAsync(cancellationToken);
            return this.GetBytes(buffer, offset, count);
        }

        public override void Flush()
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

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.connection != null && this.cmdReadChunk != null && this.chunkReader != null)
                {
                    using (this.connection)
                    using (this.cmdReadChunk)
                    using (this.chunkReader)
                    {
                    }
                }
            }
            finally
            {
                this.connection = null;
                this.cmdReadChunk = null;
                this.chunkReader = null;
                base.Dispose(disposing);
            }
        }
    }
}
