using Common.Api.Models;
using Common.Data;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity;
using System.IO;
using Common.Blob;
using Common.Utils;
using Aop.Portal.Components.EmsUtils;
using Aop.Portal.Components.DocumentSerializer;
using Aop.Portal.Components.VirusScanEngine;
using Aop.Portal.Components.XmlSchemaValidator;
using Aop.Portal.Components.DocumentSigner;
using Aop.Portal.Components.PortalConfigurationManager;
using Aop.Portal.Components.DevelopmentLogger;
using System.Data.SqlClient;
using System.Configuration;

namespace Aop.DocCommunicator
{
    public class DocCommunicatorService : IAISDocumentServiceViewer, IDisposable
    {
        private IUnitOfWork unitOfWork;

        private IDocumentSerializer documentSerializer;
        private IVirusScanEngine virusScanEngine;
        private IXmlSchemaValidator xmlSchemaValidator;
        private IDocumentSigner documentSigner;
        private IPortalConfigurationManager portalConfigurationManager;
        private IDevelopmentLogger developmentLogger;
        private IEmsUtils emsUtils;

        public DocCommunicatorService()
        {
            List<IDbConfiguration> configurations = new List<IDbConfiguration>();
            configurations.Add(new DocsDbConfiguration());
            configurations.Add(new CommonDbConfiguration());

            this.unitOfWork = new UnitOfWork(configurations);

            this.documentSerializer = new DocumentSerializerImpl();
            this.virusScanEngine = new VirusScanEngineImpl();
            this.portalConfigurationManager = new PortalConfigurationManagerImpl();
            this.developmentLogger = new EventLogDevelopmentLoggerImpl(portalConfigurationManager);
            this.xmlSchemaValidator = new XmlSchemaValidatorImpl(developmentLogger);
            this.documentSigner = new DocumentSignerImpl(portalConfigurationManager, documentSerializer);
            this.emsUtils = new EmsUtilsImpl(documentSerializer, xmlSchemaValidator, virusScanEngine, documentSigner);
        }

        public DocumentInfo GetDocumentByTicketId(string ticketId)
        {
            Guid ticketIdGuid = Guid.Parse(ticketId);
            Ticket ticket = this.unitOfWork.DbContext.Set<Ticket>().Single(e => e.TicketId == ticketIdGuid);

            DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>().Include(e => e.Doc).Single(e => e.DocFileId == ticket.DocFileId);
            var fileContent = ReadFromBlob(ticket.OldKey);

            string uri = this.unitOfWork.DbContext.Set<DocFileType>().Single(e => e.DocFileTypeId == docFile.DocFileTypeId).DocTypeUri;

            string xmlContent = Utf8Utils.GetString(fileContent);

            var documentMetaData = emsUtils.GetDocumentMetadataFromXml(xmlContent);

            string signatureXPath = documentMetaData.SignatureXPath;
            Dictionary<string, string> signatureXPathNamespaces = new Dictionary<string, string>(documentMetaData.SignatureXPathNamespaces);

            DocumentInfo documentInfo = new DocumentInfo();
            documentInfo.DocumentXml = xmlContent;
            documentInfo.DocumentTypeURI = uri;
            documentInfo.VisualizationMode = ticket.VisualizationMode.HasValue ? (VisualizationMode)ticket.VisualizationMode.Value : VisualizationMode.DisplayWithoutSignature;
            documentInfo.SignatureXPath = signatureXPath;
            documentInfo.SignatureXPathNamespaces = signatureXPathNamespaces;
            documentInfo.AdministrationUnit = AdministrationUnit.Sofia;
            documentInfo.AdministrationService = AdministrationService.ApprovalTypeInstrumental;

            return documentInfo;
        }

        public List<Error> SaveDocument(string ticketId, string documentXml)
        {
            Guid ticketIdGuid = Guid.Parse(ticketId);
            Guid fileKey = WriteToBlob(Utf8Utils.GetBytes(documentXml));

            Ticket ticket = this.unitOfWork.DbContext.Set<Ticket>().Single(e => e.TicketId == ticketIdGuid);
            ticket.NewKey = fileKey;

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

        public IEnumerable<NomenclatureItem> SearchNomenclature(string ticketID, NomenclatureType type, int? startIndex, int? offset)
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
                        var nom = new Aop.Portal.RioObjects.Enums.OperationalProgramNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                default:
                    {
                        var nom = new Aop.Portal.RioObjects.Enums.DummyNomenclature();
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

