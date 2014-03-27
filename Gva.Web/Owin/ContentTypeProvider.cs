using System.Collections.Generic;
using Microsoft.Owin.StaticFiles.ContentTypes;

namespace Gva.Web.Owin
{
    public class ContentTypeProvider : FileExtensionContentTypeProvider
    {
        public ContentTypeProvider()
            :base(new Dictionary<string, string>()
            {
                {".htm", "text/html"},
                {".html", "text/html"},
                {".js", "application/javascript"},
                {".css", "text/css"},
                {".map", "application/json"},

                {".ttf", "application/octet-stream"},
                {".eot", "application/octet-stream"},
                {".svg", "application/octet-stream"},
                {".otf", "application/octet-stream"},
                {".woff", "application/font-woff"},

                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".gif", "image/gif"}
            })
        {
        }
    }
}