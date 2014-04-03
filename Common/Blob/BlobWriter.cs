using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace Common.Blob
{
    public class BlobWriter : IDisposable
    {
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
            using (SqlCommand cmdInsert = new SqlCommand(
@"
INSERT INTO [dbo].[Blobs] ([Key], [Hash], [Size], [Content], [IsDeleted]) 
    VALUES (newid(), NULL, NULL, NULL, 0);

SET @id = SCOPE_IDENTITY();
",
                    this.connection))
            {
                SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);
                paramId.Direction = ParameterDirection.Output;
                cmdInsert.Parameters.Add(paramId);

                cmdInsert.ExecuteNonQuery();

                this.id = (int)paramId.Value;

                BlobWriteStream blobStream = new BlobWriteStream(this.connection, null, "dbo", "Blobs", "Content", "BlobId", this.id);

                this.sha1 = new SHA1Managed();
                this.stream = new CryptoStream(blobStream, this.sha1, CryptoStreamMode.Write);

                return this.stream;
            }
        }

        public Guid GetBlobKey()
        {
            // make sure noone writes to the blob after we calculate its hash
            this.stream.Close();

            using (SqlTransaction trn = this.connection.BeginTransaction())
            using (SqlCommand cmdUpdate = new SqlCommand(
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
                    trn))
            {
                cmdUpdate.Parameters.AddWithValue("@id", this.id);
                cmdUpdate.Parameters.AddWithValue("@hash", BitConverter.ToString(this.sha1.Hash).Replace("-", string.Empty));

                SqlParameter paramKey = new SqlParameter("@blobKey", SqlDbType.UniqueIdentifier);
                paramKey.Direction = ParameterDirection.Output;
                cmdUpdate.Parameters.Add(paramKey);

                cmdUpdate.ExecuteNonQuery();

                trn.Commit();

                return (Guid)paramKey.Value;
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
    }
}
