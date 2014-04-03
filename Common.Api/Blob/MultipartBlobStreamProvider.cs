using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Blob;

namespace Common.Api.Blob
{
    public class MultipartBlobStreamProvider : MultipartStreamProvider, IDisposable
    {
        private List<BlobWriter> blobWriters = new List<BlobWriter>();

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
                this.blobWriters.Add(null);
                return new MemoryStream();
            }
            // if we have a file name then write contents to with the BlobWriter which will write it to the database
            else
            {
                var blobWriter = new BlobWriter(this.connection);
                this.blobWriters.Add(blobWriter);
                return blobWriter.OpenStream();
            }
        }

        public override async Task ExecutePostProcessingAsync()
        {
            NameValueCollection formData = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            List<MultipartBlobData> blobData = new List<MultipartBlobData>();

            for (int index = 0; index < this.Contents.Count; index++)
            {
                BlobWriter blobWriter = this.blobWriters[index];
                HttpContent content = this.Contents[index];
                if (blobWriter == null)
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
                    Guid blobKey = blobWriter.GetBlobKey();
                    blobWriter.Dispose();
                    blobData.Add(new MultipartBlobData(content.Headers, blobKey));
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
            try
            {
                if (disposing && this.blobWriters != null)
                {
                    foreach (var blobWriter in this.blobWriters)
                    {
                        blobWriter.Dispose();
                    }
                }
            }
            finally
            {
                this.blobWriters = null;

                //we are not managing the connection so we are not disposing it
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
