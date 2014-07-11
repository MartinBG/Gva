using Common.Api.Models;
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

namespace Gva.DocCommunicator
{
    public class DocCommunicatorService : Rio.Data.ServiceContracts.DocCommunicator.IAISDocumentServiceViewer, IDisposable
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

        #region Not implemented methods

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

                case NomenclatureType.AuthorityIssuedAttachedDocument:
                    {
                        var nom = new Rio.Objects.Enums.AuthorityIssuedAttachedDocumentNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ASCertificateTypePilots:
                    {
                        var nom = new Rio.Objects.Enums.ASCertificateTypeNomenclature();
                        foreach (var item in nom.PilotValues)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ASCertificateTypeFlightDispatchers:
                    {
                        var nom = new Rio.Objects.Enums.ASCertificateTypeNomenclature();
                        foreach (var item in nom.FlightDispatchersValues)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ASCertificateTypeNotPilots:
                    {
                        var nom = new Rio.Objects.Enums.ASCertificateTypeNomenclature();
                        foreach (var item in nom.NotPilotsValues)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ASCertificateTypeTrafficControllers:
                    {
                        var nom = new Rio.Objects.Enums.ASCertificateTypeNomenclature();
                        foreach (var item in nom.TrafficControllersValues)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ASCertificateTypeForeigners:
                    {
                        var nom = new Rio.Objects.Enums.ASCertificateTypeNomenclature();
                        foreach (var item in nom.ForeignersValues)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ASCertificateTypeCabinCrew:
                    {
                        var nom = new Rio.Objects.Enums.ASCertificateTypeNomenclature();
                        foreach (var item in nom.CabinCrewValues)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;


                case NomenclatureType.AviationAdministration:
                    {
                        var nom = new Rio.Objects.Enums.AviationAdministrationNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.AeromedicalFitnessClass:
                    {
                        var nom = new Rio.Objects.Enums.AeromedicalFitnessClassNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.AeromedicalCenter:
                    {
                        var nom = new Rio.Objects.Enums.AeromedicalCenterNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ASCourseExam:
                    {
                        var nom = new Rio.Objects.Enums.ASCourseExamNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ATO:
                    {
                        var nom = new Rio.Objects.Enums.ATONomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ASLLData:
                    {
                        var nom = new Rio.Objects.Enums.ASLLDataNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.LEC:
                    {
                        var nom = new Rio.Objects.Enums.LECNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.AircraftClass:
                case NomenclatureType.AircraftClassQualificationClass:
                    {
                        var nom = new Rio.Objects.Enums.AircraftClassQualificationClassNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.AircraftType:
                case NomenclatureType.AircraftTypeQualificationClass:
                    {
                        var nom = new Rio.Objects.Enums.AircraftTypeQualificationClassNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.FlightSimulatorType:
                    {
                        var nom = new Rio.Objects.Enums.DummyNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.FlightSkillTestExaminer:
                    {
                        var nom = new Rio.Objects.Enums.FlightSkillTestExaminerNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.AircraftMaintenanceCategoryLicense:
                    {
                        var nom = new Rio.Objects.Enums.AircraftMaintenanceCategoryLicenseNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.OVDBodyLocationIndicator:
                    {
                        var nom = new Rio.Objects.Enums.OVDBodyLocationIndicatorNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.SectorWorkplaceAviationGroundStaff:
                    {
                        var nom = new Rio.Objects.Enums.SectorWorkplaceAviationGroundStaffNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.OVDQualificationClassWithoutPermission:
                    {
                        var nom = new Rio.Objects.Enums.OVDQualificationClassWithoutPermissionNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.CoordinationActivitiesInteractionAirTrafficManagementPermission:
                    {
                        var nom = new Rio.Objects.Enums.CoordinationActivitiesInteractionAirTrafficManagementPermissionNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.AircraftOperationType:
                    {
                        var nom = new Rio.Objects.Enums.AircraftOperationTypeNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.CategoryAircraft:
                    {
                        var nom = new Rio.Objects.Enums.CategoryAircraftNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.EngineType:
                    {
                        var nom = new Rio.Objects.Enums.DummyNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.PropellerType:
                    {
                        var nom = new Rio.Objects.Enums.DummyNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.CategoryELVS:
                    {
                        var nom = new Rio.Objects.Enums.CategoryELVSNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.TypeOfObject:
                    {
                        var nom = new Rio.Objects.Enums.TypeOfObjectNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.PersonRepresentingTradingCompanyRole:
                    {
                        var nom = new Rio.Objects.Enums.PersonRepresentingTradingCompanyRoleNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ProvidingServiceKind:
                    {
                        var nom = new Rio.Objects.Enums.ProvidingServiceKindNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.ProvidingServicePart:
                    {
                        var nom = new Rio.Objects.Enums.ProvidingServicePartNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text, ParentValue = item.ParentValue });
                        }
                    } break;

                case NomenclatureType.ProvidingServiceSubpart:
                    {
                        var nom = new Rio.Objects.Enums.ProvidingServiceSubpartNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text, ParentValue = item.ParentValue });
                        }
                    } break;

                case NomenclatureType.FacilityKindATM:
                    {
                        var nom = new Rio.Objects.Enums.FacilityKindNomenclature();
                        foreach (var item in nom.FitnessAutomatedATMValues)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.FacilityKindAid:
                    {
                        var nom = new Rio.Objects.Enums.FacilityKindNomenclature();
                        foreach (var item in nom.NavigationalAidsValues)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.FacilityLocation:
                    {
                        var nom = new Rio.Objects.Enums.FacilityLocationNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.GroupTypeChecking:
                    {
                        var nom = new Rio.Objects.Enums.GroupTypeCheckingNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.TypeChecking:
                    {
                        var nom = new Rio.Objects.Enums.TypeCheckingNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text, ParentValue = item.ParentValue });
                        }
                    } break;

                case NomenclatureType.AviationTrainingCenter:
                    {
                        var nom = new Rio.Objects.Enums.AviationTrainingCenterNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.Course:
                    {
                        var nom = new Rio.Objects.Enums.CourseNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.FSTDType:
                    {
                        var nom = new Rio.Objects.Enums.FSTDTypeNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.PermissibleActivity:
                    {
                        var nom = new Rio.Objects.Enums.PermissibleActivityNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.Permission:
                    {
                        var nom = new Rio.Objects.Enums.PermissionNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.DirectionFunction:
                    {
                        var nom = new Rio.Objects.Enums.DirectionFunctionNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.AircraftModel:
                    {
                        var nom = new Rio.Objects.Enums.DummyNomenclature();
                        foreach (var item in nom.Values)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.TrainingModuleInitial:
                    {
                        var nom = new Rio.Objects.Enums.TrainingModuleNomenclature();
                        foreach (var item in nom.InitialValues)
                        {
                            list.Add(new NomenclatureItem { Value = item.Value, Text = item.Text });
                        }
                    } break;

                case NomenclatureType.TrainingModulePeriodical:
                    {
                        var nom = new Rio.Objects.Enums.TrainingModuleNomenclature();
                        foreach (var item in nom.PeriodicalValues)
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

