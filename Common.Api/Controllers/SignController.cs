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
using Common.Utils;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SigningTools.OfficeSigner;
using SigningTools.XmlSigner;

namespace Common.Api.Controllers
{
    public class SignController : ApiController
    {
        public Task<HttpResponseMessage> PostSignXml(Guid fileKey, string signatureXPath, Dictionary<string, string> signatureXPathNamespaces)
        {
            return SignDocumentAsync(SigningDocType.Xml, fileKey, signatureXPath, signatureXPathNamespaces);
        }

        public Task<HttpResponseMessage> PostSignOffice(Guid fileKey)
        {
            return SignDocumentAsync(SigningDocType.Office, fileKey);
        }

        private async Task<HttpResponseMessage> SignDocumentAsync(SigningDocType signingDocType, Guid fileKey, string signatureXPath = null, IDictionary<string, string> signatureXPathNamespaces = null)
        {
            string serialNumber = System.Configuration.ConfigurationManager.AppSettings["Common.Api:CertificateSerialNumber"];
            string pinCode = System.Configuration.ConfigurationManager.AppSettings["Common.Api:CertificatePinCode"];

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                await connection.OpenAsync();

                Stream signedStream = null;
                using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", fileKey))
                {
                    if (signingDocType == SigningDocType.Xml)
                    {
                        XmlSigner xmlSigner = new XmlSigner(serialNumber, pinCode);
                        signedStream = xmlSigner.Sign(blobStream, Encoding.UTF8, signatureXPath, signatureXPathNamespaces);
                    }
                    else if (signingDocType == SigningDocType.Office)
                    {
                        OfficeSigner xmlSigner = new OfficeSigner(serialNumber, pinCode);

                        try
                        {
                            signedStream = new MemoryStream();
                            blobStream.CopyTo(signedStream);
                            signedStream.Position = 0;

                            xmlSigner.SignInPlace(signedStream);
                        }
                        catch (Exception)
                        {
                            signedStream.Dispose();
                            throw;
                        }
                    }
                }

                using (signedStream)
                {
                    using (var blobWriter = new BlobWriter(connection))
                    using (var stream = await blobWriter.OpenStreamAsync())
                    {
                        signedStream.Position = 0;
                        signedStream.CopyTo(stream);

                        var blobKey = await blobWriter.GetBlobKeyAsync();

                        return Request.CreateResponse(HttpStatusCode.OK, new { fileKey = blobKey });
                    }
                }
            }
        }

        private enum SigningDocType
        {
            Xml,
            Office
        }
    }
}
