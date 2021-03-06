﻿using Common.Api.Models;
using Common.Data;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.IO;
using Common.Blob;
using Common.Utils;
using System.Data.SqlClient;
using System.Configuration;
using Rio.Data.Utils.RioDocumentParser;
using Rio.Data.ServiceContracts.DocCommunicator;

namespace Aop.DocCommunicator
{
    public class DocCommunicatorService : Rio.Data.ServiceContracts.DocCommunicator.IAisCommunicator, IDisposable
    {
        private IUnitOfWork unitOfWork;
        private IRioDocumentParser rioDocumentParser;

        public DocCommunicatorService()
        {
            List<IDbConfiguration> configurations = new List<IDbConfiguration>();
            configurations.Add(new DocsDbConfiguration());
            configurations.Add(new CommonDbConfiguration());

            this.unitOfWork = new UnitOfWork(configurations, Enumerable.Empty<IDbContextInitializer>());
            this.rioDocumentParser = new RioDocumentParser();
        }

        public DocumentInfo GetDocumentByTicketId(string ticketId)
        {
            Guid ticketIdGuid = Guid.Parse(ticketId);
            Ticket ticket = this.unitOfWork.DbContext.Set<Ticket>().Single(e => e.TicketId == ticketIdGuid);

            DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>().Include(e => e.Doc).Single(e => e.DocFileId == ticket.DocFileId);
            var fileContent = ReadFromBlob(ticket.BlobOldKey.Value);

            string uri = this.unitOfWork.DbContext.Set<DocFileType>().Single(e => e.DocFileTypeId == docFile.DocFileTypeId).DocTypeUri;

            string xmlContent = Utf8Utils.GetString(fileContent);

            var documentMetaData = rioDocumentParser.GetDocumentMetadataFromXml(xmlContent);

            string signatureXPath = documentMetaData.SignatureXPath;
            Dictionary<string, string> signatureXPathNamespaces = new Dictionary<string, string>(documentMetaData.SignatureXPathNamespaces);

            DocumentInfo documentInfo = new DocumentInfo();
            documentInfo.DocumentXml = xmlContent;
            documentInfo.DocumentTypeURI = uri;
            documentInfo.VisualizationMode = ticket.VisualizationMode.HasValue ? (VisualizationMode)ticket.VisualizationMode.Value : VisualizationMode.DisplayWithoutSignature;
            documentInfo.SignatureXPath = signatureXPath;
            documentInfo.SignatureXPathNamespaces = signatureXPathNamespaces;

            return documentInfo;
        }

        public List<Error> SaveDocument(string ticketId, string documentXml)
        {
            Guid ticketIdGuid = Guid.Parse(ticketId);
            Guid fileKey = WriteToBlob(Utf8Utils.GetBytes(documentXml));

            Ticket ticket = this.unitOfWork.DbContext.Set<Ticket>().Single(e => e.TicketId == ticketIdGuid);
            ticket.BlobNewKey = fileKey;

            this.unitOfWork.Save();

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.unitOfWork != null)
                {
                    using (this.unitOfWork)
                    {
                    }
                }
            }
            finally
            {
                this.unitOfWork = null;
            }
        }

        public IEnumerable<NomenclatureItem> SearchNomenclature(string ticketID, NomenclatureType type, NomenclatureLanguage language, int? startIndex, int? offset)
        {
            List<NomenclatureItem> list = new List<NomenclatureItem>();

            switch (type)
            {
                case NomenclatureType.IrregularityTypes:
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            list.Add(new NomenclatureItem { Type = NomenclatureType.IrregularityTypes, Value = "Нередовност " + i, Text = "Нередовност " + i, Description = "Описание " + i });
                        }
                    } break;

                case NomenclatureType.OperationalProgramAop:
                    {
                        var nom = new Rio.Objects.Enums.OperationalProgramNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                default:
                    {
                        var nom = new Rio.Objects.Enums.DummyNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Type = NomenclatureType.Dummy, Value = item.Value, Text = item.Text });
                        }
                    } break;
            }



            return list;
        }

        #region Not implemented methods

        public void SetDocumentInfoCache(DocumentInfoCache documentInfoCache)
        {
            throw new NotImplementedException();
        }

        public bool ClearDocumentInfoCache(string ticketID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        private Guid WriteToBlob(byte[] content)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                connection.Open();
                using (var blobWriter = new BlobWriter(connection))
                using (var stream = blobWriter.OpenStream())
                {
                    stream.Write(content, 0, content.Length);
                    return blobWriter.GetBlobKey();
                }
            }
        }

        private byte[] ReadFromBlob(Guid key)
        {
            var blob = this.unitOfWork.DbContext.Set<Blob>().SingleOrDefault(e => e.Key == key);

            return blob != null ? blob.Content : null;
        }

        private void SaveXmlToDisc(string xmlContent)
        {
            try
            {
                StreamWriter writer = new StreamWriter("G:\\" + Guid.NewGuid() + ".xml");
                writer.Write(xmlContent);
                writer.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion        
    }
}

