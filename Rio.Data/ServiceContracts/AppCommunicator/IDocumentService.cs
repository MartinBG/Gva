using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using R_0009_000001;

namespace Rio.Data.ServiceContracts.AppCommunicator
{
    [ServiceContract(Name = "DocumentService", Namespace = "http://portal.ems.bg/2013/DocumentService/v1")]
    [XmlSerializerFormat]
    public interface IDocumentService
    {
        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        DocumentInfo ProcessStructuredDocument(DocumentRequest request);

        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        DocumentInfo GetDocumentInfo(DocumentURI uri, Guid? guid);

        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        Stream GetDocumentContent(DocumentURI uri, Guid? guid);

        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        CaseFileInfo GetCaseFileInfo(DocumentURI uri, string publicAccessCode);

        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        R_0009_000067.ServiceStatus GetServiceStatus(DocumentURI uri, string serviceIdentifier);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(ReceiverFault))]
        void RegisterElectronicPortalPmntInfo(ElectronicPortalPaymentInfo paymentInfo);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(ReceiverFault))]
        CaseStatusInfo GetCaseStatusInfo(Guid guid);

        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        CaseFileInfo GetCaseFileInfoWithCustomUri(string uri);

        [OperationContract]
        [FaultContract(typeof(ReceiverFault))]
        Stream GetDocumentContentWithCustomUri(string uri);
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://portal.ems.bg/2013/Payments")]
    public class ElectronicPortalPaymentInfo
    {
        public Guid PaymentInfoID { get; set; }
        public ElectronicPortalPaymentKinds PaymentKind { get; set; }
        /// <summary>
        /// Ури на документ, по който се прави плащането 
        /// </summary>
        public DocumentURI DocumentUri { get; set; }
        public string Invoice { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        /// <summary>
        /// Дата на иницииране на плащането от страна на потребителя.
        /// </summary>
        public DateTime PaymentStartTime { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Дата на плащане от платежната системата
        /// </summary>
        public DateTime PaymentTime { get; set; }
        public string Stan { get; set; }
        public string BCode { get; set; }
        /// <summary>
        /// Допълнителна информация относно плащането.
        /// </summary>
        public string AdditionalInfo { get; set; }
    }

    public enum ElectronicPortalPaymentKinds
    {
        /// <summary>
        /// Плащане през портал ePay.bg
        /// </summary>
        EPay,

        /// <summary>
        /// Плащане през портал eBg.bg
        /// </summary>
        EBG
    }

    [DataContract(Namespace = "http://portal.ems.bg/2013/DocumentService/v1")]
    public class ReceiverFault
    {
        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
