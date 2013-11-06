using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Threading;

namespace Gva.Web.Controllers
{
    public class FileController : Controller
    {
        static Dictionary<Guid, byte[]> fileDict = new Dictionary<Guid, byte[]>();

        [HttpGet]
        public ActionResult Index(Guid fileKey, string fileName)
        {
            return File(fileDict[fileKey], "application/octet-stream", fileName);
        }

        [HttpPost]
        public ActionResult Index()
        {
            HttpPostedFileBase postedFile = this.Request.Files[0];

            byte[] content = new byte[postedFile.InputStream.Length];
            using (MemoryStream ms = new MemoryStream(content))
            {
                postedFile.InputStream.CopyTo(ms);
            }

            Guid fileKey = Guid.NewGuid();
            fileDict.Add(fileKey, content);
            Thread.Sleep(250);
            return Content(JsonConvert.SerializeObject(new { fileKey = fileKey }));
        }
    }
}