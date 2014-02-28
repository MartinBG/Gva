using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Common.Api.Utils;

namespace Common.Api.Http
{
    public class MultipartBlobStreamProvider : MultipartStreamProvider, IDisposable
    {
        private class Blob
        {
            public int Id { get; set; }

            public SHA1 SHA1 { get; set; }
        }

        private List<Blob> blobs = new List<Blob>();

        private SqlConnection connection;

        public MultipartBlobStreamProvider(SqlConnection connection)
        {
            this.connection = connection;
        }

        public NameValueCollection FormData { get; private set; }

        public ICollection<MultipartBlobData> BlobData { get; private set; }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;

            if (contentDisposition == null)
            {
                // for form data, Content-Disposition header is a requirement
                throw new InvalidOperationException("Did not find required 'Content-Disposition' header field in MIME multipart body part.");
            }

            // if no filename parameter was found in the Content-Disposition header then return a memory stream.
            if (string.IsNullOrEmpty(contentDisposition.FileName))
            {
                this.blobs.Add(null);
                return new MemoryStream();
            }
            // if we have a file name then write contents out to our BlobWriteStream which will write it to the database
            else
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

                    int id = (int)paramId.Value;

                    BlobWriteStream blobStream = new BlobWriteStream(this.connection, null, "dbo", "Blobs", "Content", "BlobId", id);

                    SHA1 sha1 = new SHA1Managed();
                    CryptoStream cryptoStream = new CryptoStream(blobStream, sha1, CryptoStreamMode.Write);

                    this.blobs.Add(new Blob { Id = id, SHA1 = sha1 });

                    // wrap the CryptoStream in our StreamApmToAsyncBridge since the crypto stream implements only
                    // the *Async methods but the WebApi calls the APM style Begin/End methods
                    return new StreamApmToAsyncBridge(cryptoStream);
                }
            }
        }

        public override async Task ExecutePostProcessingAsync()
        {
            NameValueCollection formData = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            List<MultipartBlobData> blobData = new List<MultipartBlobData>();

            for (int index = 0; index < this.Contents.Count; index++)
            {
                Blob blob = this.blobs[index];
                HttpContent content = this.Contents[index];
                if (blob == null)
                {
                    // Extract name from Content-Disposition header. We know from earlier that the header is present.
                    ContentDispositionHeaderValue contentDisposition = content.Headers.ContentDisposition;
                    string formFieldName = UnquoteToken(contentDisposition.Name) ?? string.Empty;

                    // Read the contents as string data and add to form data
                    string formFieldValue = await content.ReadAsStringAsync();
                    formData.Add(formFieldName, formFieldValue);
                }
                else
                {
                    using (SqlTransaction trn = await Task.Run(() => this.connection.BeginTransaction()))
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
                        cmdUpdate.Parameters.AddWithValue("@id", blob.Id);
                        cmdUpdate.Parameters.AddWithValue("@hash", BitConverter.ToString(blob.SHA1.Hash).Replace("-", string.Empty));

                        SqlParameter paramKey = new SqlParameter("@blobKey", SqlDbType.UniqueIdentifier);
                        paramKey.Direction = ParameterDirection.Output;
                        cmdUpdate.Parameters.Add(paramKey);

                        await cmdUpdate.ExecuteNonQueryAsync();

                        await Task.Run(() => trn.Commit());

                        blobData.Add(new MultipartBlobData(content.Headers, (Guid)paramKey.Value));
                    }
                }
            }

            this.FormData = new NameValueCollection(formData);
            this.BlobData = blobData.AsReadOnly();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && this.blobs != null)
            {
                foreach (var blob in this.blobs)
                {
                    blob.SHA1.Dispose();
                }

                this.blobs = null;
                this.connection = null;
            }
        }

        /// <summary>
        /// Remove bounding quotes on a token if present
        /// NOTE: A copy of the WebApi internal System.Net.Http.FormattingUtilities.UnquoteToken
        /// </summary>
        /// <param name="token">Token to unquote.</param>
        /// <returns>Unquoted token.</returns>
        private static string UnquoteToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
            {
                return token.Substring(1, token.Length - 2);
            }

            return token;
        }
    }
}
