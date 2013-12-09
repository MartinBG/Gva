using System;
using System.Net.Http.Headers;

namespace Common.Api.Http
{
    public class MultipartBlobData
    {
        public MultipartBlobData(HttpContentHeaders headers, Guid blobKey)
        {
            Headers = headers;
            BlobKey = blobKey;
        }

        public HttpContentHeaders Headers { get; private set; }

        public Guid BlobKey { get; private set; }
    }
}
