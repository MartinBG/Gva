using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Gva.Api.Repositories.FileRepository;
using Microsoft.Office.Interop.Word;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.PrintRepository
{
    public class PrintRepository : IPrintRepository
    {
        private static readonly object syncRoot = new object();
        private const int DEFAULT_BUFFER_SIZE = 81920;
        private static readonly byte[] buffer = new byte[DEFAULT_BUFFER_SIZE];

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

            lock (syncRoot)
            {
                Application wordApplication = null;
                Document document = null;
                try
                {
                    using (var tmpFileStream = File.OpenWrite(tmpDocFile))
                    {
                        this.CopyStream(stream, tmpFileStream);
                    }

                    wordApplication = new Application();
                    document = wordApplication.Documents.Open(
                        ReadOnly: false,
                        FileName: tmpDocFile,
                        ConfirmConversions: false,
                        OpenAndRepair: true,
                        NoEncodingDialog: true);

                    document.ExportAsFixedFormat(tmpPdfFile, WdExportFormat.wdExportFormatPDF);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (document != null)
                    {
                        document.Close(WdSaveOptions.wdDoNotSaveChanges, Missing.Value, Missing.Value);
                        Marshal.FinalReleaseComObject(document);
                    }

                    if (wordApplication != null)
                    {
                        wordApplication.Quit(WdSaveOptions.wdDoNotSaveChanges, Missing.Value, Missing.Value);
                        Marshal.FinalReleaseComObject(wordApplication);
                        Marshal.ReleaseComObject(wordApplication);
                    }

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
    }
}