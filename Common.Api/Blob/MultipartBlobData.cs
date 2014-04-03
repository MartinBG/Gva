using System;
using System.Net.Http.Headers;

namespace Common.Api.Blob
{
    public class MultipartBlobData
    {
        public MultipartBlobData(HttpContentHeaders headers, Guid blobKey)
        {
            this.Headers = headers;
            this.BlobKey = blobKey;
        }

        public HttpContentHeaders Headers { get; private set; }

        public Guid BlobKey { get; private set; }
    }
}
