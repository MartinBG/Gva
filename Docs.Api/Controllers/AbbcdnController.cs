using Abbcdn;
using Rio.Data.Abbcdn;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Web.Http;

namespace Docs.Api.Controllers
{
    //[Authorize]
    public class AbbcdnController : ApiController
    {
        public HttpResponseMessage Get(Guid fileKey, string mimeType = null, string fileName = null)
        {
            DownloadFileInfo downloadFileInfo = null;

            using (var channelFactory = new ChannelFactory<IAbbcdn>("WSHttpBinding_IAbbcdn"))
            using (var abbcdnStorage = new AbbcdnStorage(channelFactory))
            {
                downloadFileInfo = abbcdnStorage.DownloadFile(fileKey);
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new PushStreamContent(
                (outputStream, httpContent, transportContext) =>
                {
                    using (outputStream)
                    {
                        outputStream.Write(downloadFileInfo.ContentBytes, 0, downloadFileInfo.ContentBytes.Length);
                    }
                });
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue(string.IsNullOrEmpty(mimeType) ? "application/octet-stream" : mimeType);
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

            return result;
        }
    }
}
