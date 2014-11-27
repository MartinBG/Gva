using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Common.Json;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Microsoft.Office.Interop.Word;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.PrintRepository
{
    public class PrintRepository : IPrintRepository
    {
        private readonly object syncRoot = new object();
        private const int DEFAULT_BUFFER_SIZE = 81920;

        private IUnitOfWork unitOfWork;
        private UserContext userContext;
        private ILotRepository lotRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public PrintRepository(
            IUnitOfWork unitOfWork,
            UserContext userContext,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.userContext = userContext;
            this.lotEventDispatcher = lotEventDispatcher;
        }

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

            try
            {
                using (var tmpFileStream = File.OpenWrite(tmpDocFile))
                {
                    stream.CopyTo(tmpFileStream);
                }

                lock (syncRoot)
                {
                    var document = new Application().Documents.Open(tmpDocFile, false);
                    document.ExportAsFixedFormat(tmpPdfFile, WdExportFormat.wdExportFormatPDF);
                    document.Close();
                }
            }
            finally
            {
                File.Delete(tmpDocFile);
            }

            return new FileStream(tmpPdfFile,
                FileMode.Open,
                FileAccess.Read,
                FileShare.None,
                DEFAULT_BUFFER_SIZE,
                FileOptions.DeleteOnClose);
        }
    }
}