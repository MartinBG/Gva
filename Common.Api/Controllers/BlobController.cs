using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Api.Blob;
using Common.Blob;

namespace Common.Api.Controllers
{
    public class BlobController : ApiController
    {
        public HttpResponseMessage Get(Guid fileKey, string mimeType = null, string fileName = null, string dispositionType = null)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new PushStreamContent(
                async (outputStream, httpContent, transportContext) =>
                {
                    using (outputStream)
                    {
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                        {
                            connection.Open();

                            var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", fileKey);
                            await blobStream.CopyToAsync(outputStream);
                        }
                    }
                });
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue(string.IsNullOrEmpty(mimeType) ? "application/octet-stream" : mimeType);
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue(string.IsNullOrEmpty(dispositionType) ? "attachment" : dispositionType)
                {
                    FileName = fileName
                };

            return result;
        }

        public async Task<HttpResponseMessage> Post(CancellationToken cancellationToken)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                await conn.OpenAsync(cancellationToken);

                using (var multipartProvider = new MultipartBlobStreamProvider(conn))
                {
                    var blobProvider = await Request.Content.ReadAsMultipartAsync(multipartProvider, cancellationToken);
                    var firstBlobKey = blobProvider.BlobData.First().BlobKey;

                    return Request.CreateResponse(HttpStatusCode.OK, new { fileKey = firstBlobKey });
                }
            }
        }
    }
}
