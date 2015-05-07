using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Common.Blob;
using Common.Data;
using Gva.Api.Models;
using SautinSoft;

namespace Gva.Api.Repositories.PrintRepository
{
    public class PrintRepository : IPrintRepository
    {
        private IUnitOfWork unitOfWork;

        public PrintRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private static readonly object syncRoot = new object();
        private const int DEFAULT_BUFFER_SIZE = 81920;
        private static readonly byte[] buffer = new byte[DEFAULT_BUFFER_SIZE];

        public Guid SaveStreamToBlob(Stream stream, string connectionString)
        {
            Guid licenceEditionDocBlobKey;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var blobWriter = new BlobWriter(connection))
                using (var blobStream = blobWriter.OpenStream())
                {
                    stream.CopyTo(blobStream);
                    licenceEditionDocBlobKey = blobWriter.GetBlobKey();
                }
            }

            return licenceEditionDocBlobKey;
        }

        public Stream ConvertWordStreamToPdfStream(Stream stream)
        {
            var tmpDocFile = Path.GetTempFileName();
            var tmpPdfFile = Path.GetTempFileName();

            lock (syncRoot)
            {
                try
                {
                    using (var tmpFileStream = File.OpenWrite(tmpDocFile))
                    {
                        this.CopyStream(stream, tmpFileStream);
                    }

                    UseOffice useOffice = new UseOffice();
                    useOffice.Serial = ConfigurationManager.AppSettings["Gva.Api:UseOfficeSerialNumber"];

                    useOffice.InitWord();
                    useOffice.ConvertFile(tmpDocFile, tmpPdfFile, UseOffice.eDirection.DOCX_to_PDF);
                    useOffice.CloseWord();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    File.Delete(tmpDocFile);
                }

            }

            return new FileStream(tmpPdfFile,
                FileMode.Open,
                FileAccess.Read,
                FileShare.None,
                DEFAULT_BUFFER_SIZE,
                FileOptions.DeleteOnClose);
        }

        private void CopyStream(Stream input, Stream output)
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public int SaveNewFile(string name, Guid blobKey)
        {
            var newFile = new GvaFile()
            {
                Filename = name,
                FileContentId = blobKey,
                MimeType = "application/pdf"
            };

            this.unitOfWork.DbContext.Set<GvaFile>().Add(newFile);

            this.unitOfWork.Save();

            return newFile.GvaFileId;
        }

        public HttpResponseMessage ReturnResponseMessage(string url)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.Redirect);
            result.Headers.Location = new Uri(url, UriKind.Relative);
            result.Headers.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true,
                MustRevalidate = true
            };

            return result;
        }
    }
}