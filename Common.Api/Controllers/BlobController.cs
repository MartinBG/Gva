using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Api.Http;

namespace Common.Api.Controllers
{
    public class BlobController : ApiController
    {
        public HttpResponseMessage Get(Guid fileKey, string mimeType = null, string fileName = null)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content =
                new StreamContent(
                    new BlobReadStream(
                        "DbContext", "dbo", "Blobs", "Content", "Key", fileKey));

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue(string.IsNullOrEmpty(mimeType) ? "application/octet-stream" : mimeType);
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

            return result;
        }

        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                await conn.OpenAsync();

                var blobProvider = await Request.Content.ReadAsMultipartAsync(new MultipartBlobStreamProvider(conn));
                var firstBlobKey = blobProvider.BlobData.First().BlobKey;

                return Request.CreateResponse(HttpStatusCode.OK, new { fileKey = firstBlobKey });
            }
        }
    }
}
