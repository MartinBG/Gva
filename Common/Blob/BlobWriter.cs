using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Blob
{
    public class BlobWriter : IDisposable
    {
        private static object updatingSyncRoot = new Object();

        private SqlConnection connection;
        private Stream stream;
        private int id;
        private SHA1 sha1;

        public BlobWriter(SqlConnection connection)
        {
            this.connection = connection;
        }

        public Stream OpenStream()
        {
            SqlParameter idParam;
            using (SqlCommand cmdInsert = this.CreateInsertCmd(out idParam))
            {
                cmdInsert.ExecuteNonQuery();

                this.id = (int)idParam.Value;

                BlobWriteStream blobStream = new BlobWriteStream(this.connection, null, "dbo", "Blobs", "Content", "BlobId", this.id);

                this.sha1 = new SHA1Managed();
                this.stream = new CryptoStream(blobStream, this.sha1, CryptoStreamMode.Write);

                return this.stream;
            }
        }

        //GetBlobKey is only synchronous as updates need to be executed sequentially
        //it would be hard to implement such a method in a way that would not lead
        //to deadlocking when both the sync and async versions are used
        public Guid GetBlobKey()
        {
            // make sure noone writes to the blob after we calculate its hash
            this.stream.Close();

            SqlParameter blobKeyParam;
            using (SqlTransaction trn = this.connection.BeginTransaction())
            using (SqlCommand cmdUpdate = this.CreateUpdateCmd(trn, out blobKeyParam))
            {
                lock (updatingSyncRoot)
                {
                    cmdUpdate.ExecuteNonQuery();
                    trn.Commit();
                }

                return (Guid)blobKeyParam.Value;
            }
        }

        public async Task<Stream> OpenStreamAsync()
        {
            SqlParameter idParam;
            using (SqlCommand cmdInsert = this.CreateInsertCmd(out idParam))
            {
                await cmdInsert.ExecuteNonQueryAsync();

                this.id = (int)idParam.Value;

                BlobWriteStream blobStream = new BlobWriteStream(this.connection, null, "dbo", "Blobs", "Content", "BlobId", this.id);

                this.sha1 = new SHA1Managed();
                this.stream = new CryptoStream(blobStream, this.sha1, CryptoStreamMode.Write);

                return this.stream;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.sha1 != null && this.stream != null)
                {
                    using (this.sha1)
                    using (this.stream)
                    {
                    }
                }
            }
            finally
            {
                this.sha1 = null;
                this.stream = null;

                //we are not managing the connection so we are not disposing it
                this.connection = null;
            }
        }

        private SqlCommand CreateUpdateCmd(SqlTransaction trn, out SqlParameter blobKeyParam)
        {
            var cmd = new SqlCommand(
@"
DECLARE @size INT;

SELECT @size = DATALENGTH([Content]) FROM [dbo].[Blobs] WHERE [BlobId] = @id;

SELECT @blobKey = [Key] FROM [dbo].[Blobs] WHERE [Hash] = @hash AND [Size] = @size;

IF (@blobKey IS NULL)
BEGIN
    UPDATE [dbo].[Blobs] SET [Hash] = @hash, [Size] = @size WHERE [BlobId] = @id;
    SELECT @blobKey = [Key] FROM [dbo].[Blobs] WHERE [BlobId] = @id;
END
ELSE
BEGIN
    UPDATE [dbo].[Blobs] SET [IsDeleted] = 1 WHERE [BlobId] = @id;
END
",
                    this.connection,
                    trn);

            cmd.Parameters.AddWithValue("@id", this.id);
            cmd.Parameters.AddWithValue("@hash", BitConverter.ToString(this.sha1.Hash).Replace("-", string.Empty));

            blobKeyParam = new SqlParameter("@blobKey", SqlDbType.UniqueIdentifier);
            blobKeyParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(blobKeyParam);

            return cmd;
        }

        private SqlCommand CreateInsertCmd(out SqlParameter idParam)
        {
            var cmd = new SqlCommand(
@"
INSERT INTO [dbo].[Blobs] ([Key], [Hash], [Size], [Content], [IsDeleted]) 
    VALUES (newid(), NULL, NULL, NULL, 0);

SET @id = SCOPE_IDENTITY();
",
                    this.connection);

            idParam = new SqlParameter("@id", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(idParam);

            return cmd;
        }
    }
}
