using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Aop.DocCommunicator
{
    [ServiceContract(Name = "DocumentEVApplication", Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public interface IAISDocumentServiceViewer
    {
        /// <summary>
        /// Returns document data. 
        /// Additional data must be specified(by AIS) first.
        /// </summary>
        /// <param name="ticketId">ID of the requested document.</param>
        /// <returns>DocumentInfo.</returns>
        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        DocumentInfo GetDocumentByTicketId(string ticketId);

        /// <summary>
        /// Saves the document in the database.
        /// </summary>
        /// <param name="ticketId">ID of the edited document.</param>
        /// <param name="documentXml">Xml of the document to be saved.</param>
        /// <returns>List(DocumentServiceError).</returns>
        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        List<Error> SaveDocument(string ticketId, string documentXml);

        /// <summary>
        /// Internal method for managing service cache. 
        /// Used by AIS to specify data needed to display the document.
        /// </summary>
        /// <param name="documentInfoCache">DocumentInfoCache.</param>
        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        void SetDocumentInfoCache(DocumentInfoCache documentInfoCache);

        /// <summary>
        /// Internal method for managing service cache. 
        /// Removes an object from cache.
        /// </summary>
        /// <param name="ticketID">ID of the removed cache item.</param>
        /// <returns>True if the cache item is removed, otherwise false.</returns>
        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        bool ClearDocumentInfoCache(string ticketID);

        /// <summary>
        /// Method for search nomenclature items.
        /// </summary>
        /// <param name="type">Nomenclature Type.</param>
        /// <param name="startIndex">Start Index.</param>
        /// <param name="offset">Offset</param>
        /// <returns>List(NomenclatureItem)</returns>
        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        IEnumerable<NomenclatureItem> SearchNomenclature(string ticketID, NomenclatureType type, int? startIndex, int? offset);
    }
    [DataContract(Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public class ReceiverFault
    {
        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Message { get; set; }
    }

    [DataContract(Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public class DocumentInfo
    {
        [DataMember]
        public string DocumentXml { get; set; }

        [DataMember]
        public string DocumentTypeURI { get; set; }

        [DataMember]
        public string SignatureXPath { get; set; }

        [DataMember]
        public Dictionary<string, string> SignatureXPathNamespaces { get; set; }

        [DataMember]
        public VisualizationMode VisualizationMode { get; set; }

        [DataMember]
        public AdministrationUnit AdministrationUnit { get; set; }

        [DataMember]
        public AdministrationService AdministrationService { get; set; }
    }

    [DataContract(Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public enum VisualizationMode
    {
        [EnumMember]
        NewWithoutSignature = 0,

        [EnumMember]
        NewWithSignature = 1,

        [EnumMember]
        EditWithoutSignature = 2,

        [EnumMember]
        EditWithSignature = 3,

        [EnumMember]
        DisplayWithoutSignature = 4,

        [EnumMember]
        DisplayWithSignature = 5,
    }

    [DataContract(Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public enum AdministrationUnit
    {
        [EnumMember]
        Sofia = 1,
        [EnumMember]
        Blagoevgrad = 2,
        [EnumMember]
        Vratsa = 3,
        [EnumMember]
        Vidin = 4,
        [EnumMember]
        Plovdiv = 5,
        [EnumMember]
        Gabrovo = 6,
        [EnumMember]
        Lovetch = 7,
        [EnumMember]
        Pleven = 8,
        [EnumMember]
        Ruse = 9,
        [EnumMember]
        VelikoTarnovo = 10,
        [EnumMember]
        Shumen = 11,
        [EnumMember]
        Varna = 12,
        [EnumMember]
        Sliven = 13,
        [EnumMember]
        Burgas = 14,
        [EnumMember]
        StaraZagora = 15,
        [EnumMember]
        Haskovo = 16,
    }

    [DataContract(Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public enum AdministrationService
    {
        [EnumMember]
        ApprovalTypeInstrumental = 1,
        [EnumMember]
        InitialVerification = 2,
        [EnumMember]
        SubsequentVerification = 3,
        [EnumMember]
        MetrologicalЕxaminations = 4,
        [EnumMember]
        ProvidingInformation = 5,
        [EnumMember]
        ApprovalPlayingGround = 6,
        [EnumMember]
        ApprovalType = 7,
        [EnumMember]
        ApprovalProgramModifications = 8,
        [EnumMember]
        Expertise = 9,
        [EnumMember]
        MatchingIntegrationSystem = 10,
        [EnumMember]
        ControlTesting = 11,
        [EnumMember]
        ApprovalTypeElectronic = 12,
        [EnumMember]
        ConformityAssessmentScales = 13,
        [EnumMember]
        ConformityAssessmentProducts = 14,
        [EnumMember]
        CalibrationEtalons = 15,
        [EnumMember]
        SpreadingCalibration = 16,
        [EnumMember]
        SertificationInstrumental = 17,
        [EnumMember]
        MeasurementMetrologic = 18,
        [EnumMember]
        TestingValidationSoftware = 19,
        [EnumMember]
        ReceiptNotAcknowledge = 21,
        [EnumMember]
        ReceiptAcknowledge = 22,
        [EnumMember]
        RemovingIrregularities = 23,
    }

    [DataContract(Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public class Error
    {
        [DataMember]
        public string Message
        {
            get;
            set;
        }

        [DataMember]
        public int? Code
        {
            get;
            set;
        }
    }

    [DataContract(Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public class DocumentInfoCache
    {
        [DataMember]
        public string TicketID { get; set; }

        [DataMember]
        public long DocID { get; set; }

        [DataMember]
        public Guid ContextID { get; set; }

        [DataMember]
        public Guid ContentID { get; set; }

        [DataMember]
        public VisualizationMode VisualizationMode { get; set; }
    }

    [DataContract(Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public enum NomenclatureType
    {
        [EnumMember]
        IrregularityTypes = 1,

        #region Aop
        [EnumMember]
        OperationalProgramAop = 0,
        #endregion

        [EnumMember]
        Dummy = 100,

    }

    [DataContract(Namespace = "http://portal.ems.bg/2013/AISDocumentServiceViewer/v1")]
    public class NomenclatureItem
    {
        [DataMember]
        public int? ItemID
        {
            get;
            set;
        }

        [DataMember]
        public NomenclatureType Type
        {
            get;
            set;
        }

        [DataMember]
        public string Text
        {
            get;
            set;
        }

        [DataMember]
        public string Value
        {
            get;
            set;
        }

        [DataMember]
        public string Description
        {
            get;
            set;
        }
    }
}
