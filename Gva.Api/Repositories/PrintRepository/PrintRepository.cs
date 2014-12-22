using System;
using System.Data.SqlClient;
using System.IO;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using SautinSoft;

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
                try
                {
                    using (var tmpFileStream = File.OpenWrite(tmpDocFile))
                    {
                        this.CopyStream(stream, tmpFileStream);
                    }

                    UseOffice useOffice = new UseOffice();
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
    }
}