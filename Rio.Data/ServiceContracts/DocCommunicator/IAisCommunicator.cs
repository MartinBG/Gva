using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.ServiceContracts.DocCommunicator
{
    [ServiceContract(Name = "AisCommunicator", Namespace = "http://egov.bg/2014/AisCommunicator/v2")]
    public interface IAisCommunicator
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
        IEnumerable<NomenclatureItem> SearchNomenclature(string ticketID, NomenclatureType type, NomenclatureLanguage language, int? startIndex, int? offset);
    }

    [DataContract(Namespace = "http://egov.bg/2014/AisCommunicator/v2")]
    public class ReceiverFault
    {
        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Message { get; set; }
    }

    [DataContract(Namespace = "http://egov.bg/2014/AisCommunicator/v2")]
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
    }

    [DataContract(Namespace = "http://egov.bg/2014/AisCommunicator/v2")]
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

    [DataContract(Namespace = "http://egov.bg/2014/AisCommunicator/v2")]
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

    [DataContract(Namespace = "http://egov.bg/2014/AisCommunicator/v2")]
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

    [DataContract(Namespace = "http://egov.bg/2014/AisCommunicator/v2")]
    public enum NomenclatureType
    {
        #region Gva

        [EnumMember]
        Unknown = 0,

        [EnumMember]
        IrregularityTypes = 1,

        // Документ - издаден от
        [EnumMember]
        AuthorityIssuedAttachedDocument = 2,

        // Кандидатствам за - пилоти
        [EnumMember]
        ASCertificateTypePilots = 3,

        // Свидетелство за правоспособност - издадено от
        [EnumMember]
        AviationAdministration = 4,

        // Клас свидетелство за медицинска годност
        [EnumMember]
        AeromedicalFitnessClass = 5,

        // Свидетелство за медицинска годност - издадено от
        [EnumMember]
        AeromedicalCenter = 6,

        // Вид курс
        [EnumMember]
        ASCourseExam = 7,

        // Организация за летателно обучение
        [EnumMember]
        ATO = 8,

        // Level - предпоследно поле (5. заявление)
        [EnumMember]
        ASLLData = 9,

        // Level - последно поле (5. заявление)
        [EnumMember]
        LEC = 10,

        // Клас ВС
        [EnumMember]
        AircraftClass = 11,

        // Тип ВС
        [EnumMember]
        AircraftType = 12,

        // Вид тренаржор
        [EnumMember]
        FlightSimulatorType = 13,

        // Проверяващ
        [EnumMember]
        FlightSkillTestExaminer = 14,

        // Квалификационен клас за Клас ВС
        [EnumMember]
        AircraftClassQualificationClass = 15,

        // Квалификационен клас за Тип ВС
        [EnumMember]
        AircraftTypeQualificationClass = 16,

        // Kатегория за лиценз за техническо обслужване на въздухоплавателно средство
        [EnumMember]
        AircraftMaintenanceCategoryLicense = 17,

        // Индикатор за местоположение на орган на ОВД
        [EnumMember]
        OVDBodyLocationIndicator = 18,

        // Сектор/работно място за наземен авиационен персонал
        [EnumMember]
        SectorWorkplaceAviationGroundStaff = 19,

        // Kвалификационен клас за ОВД без разрешение
        [EnumMember]
        OVDQualificationClassWithoutPermission = 20,

        // Разрешение,  валидно за дейностите по координация и взаимодействие при управлението на въздушното движение
        [EnumMember]
        CoordinationActivitiesInteractionAirTrafficManagementPermission = 21,

        // Тип на опериране на въздухоплавателно средство
        [EnumMember]
        AircraftOperationType = 22,

        // Категория ВС
        [EnumMember]
        CategoryAircraft = 23,

        // Тип двигател
        [EnumMember]
        EngineType = 24,

        // Тип витло
        [EnumMember]
        PropellerType = 25,

        // Категория ЕЛВС
        [EnumMember]
        CategoryELVS = 26,

        // Тип обект
        [EnumMember]
        TypeOfObject = 27,

        // Роля на лице, представляващо или управляващо търговско дружество
        [EnumMember]
        PersonRepresentingTradingCompanyRole = 28,

        // Вид предоставяне обслужване
        [EnumMember]
        ProvidingServiceKind = 29,

        // Част от предоставяне обслужване
        [EnumMember]
        ProvidingServicePart = 30,

        // Подчаст от предоставяне обслужване
        [EnumMember]
        ProvidingServiceSubpart = 31,

        // Тип съоръжение АТМ
        [EnumMember]
        FacilityKindATM = 32,

        // Тип съоръжение Aid
        [EnumMember]
        FacilityKindAid = 33,

        // Местоположение на съоръжение
        [EnumMember]
        FacilityLocation = 34,

        // 
        [EnumMember]
        GroupTypeChecking = 35,

        // Група на вид проверка за авиационна сигурност
        [EnumMember]
        TypeChecking = 36,

        // Авиационен учебен център
        [EnumMember]
        AviationTrainingCenter = 37,

        // Курс
        [EnumMember]
        Course = 38,

        // Тип FSTD
        [EnumMember]
        FSTDType = 39,

        // Разрешена дейност
        [EnumMember]
        PermissibleActivity = 40,

        // Разрешение
        [EnumMember]
        Permission = 41,

        // Функция в ръководството на АО
        [EnumMember]
        DirectionFunction = 42,

        // Модел ВС
        [EnumMember]
        AircraftModel = 43,

        // Модул на обучение - първоначален
        [EnumMember]
        TrainingModuleInitial = 44,

        // Модул на обучение - периодичен
        [EnumMember]
        TrainingModulePeriodical = 45,

        // Кандидатствам за - полетни диспечери
        [EnumMember]
        ASCertificateTypeFlightDispatchers = 46,

        // Кандидатствам за - членове на летателния състав от екипажите на ВС, различни от пилоти
        [EnumMember]
        ASCertificateTypeNotPilots = 47,

        // Кандидатствам за - ръководители на полети
        [EnumMember]
        ASCertificateTypeTrafficControllers = 48,

        // Кандидатствам за - чужди граждани
        [EnumMember]
        ASCertificateTypeForeigners = 49,

        // Кандидатствам за - членовете на кабинния екипаж
        [EnumMember]
        ASCertificateTypeCabinCrew = 50,

        // Вид свидетелство
        [EnumMember]
        ASCertificateTypeChangeCompetent = 51,

        // Видове ВС за инструктор
        [EnumMember]
        AircraftTypeInstructor = 52,

        // Кандидатствам за - инструктори
        [EnumMember]
        ASCertificateTypeInstructor = 53,

        // Вид права за инструктор
        [EnumMember]
        RightsTypeInstructor = 54,

        // Вид права за проверяващ
        [EnumMember]
        RightsTypeExaminer = 55,

        // Допълнителна квалификация
        [EnumMember]
        AdditionalQualification = 56,

        // Ограничение
        [EnumMember]
        LicenseRestriction = 57,

        // Издадено от: атестация
        [EnumMember]
        IssueAttestation = 58,

        #endregion

        #region Mosv

        // До:
        [EnumMember]
        ServiceInstructions = 100,

        #endregion

        #region Aop

        [EnumMember]
        OperationalProgramAop = 200,

        #endregion

        #region Ngo

        [EnumMember]
        Court = 300,

        [EnumMember]
        AnnouncedActKind = 301,

        #endregion

        [EnumMember]
        Dummy = 999,

    }

    [DataContract(Namespace = "http://egov.bg/2014/AisCommunicator/v2")]
    public enum NomenclatureLanguage
    {
        [EnumMember]
        Bulgarian = 0,

        [EnumMember]
        English = 1
    }

    [DataContract(Namespace = "http://egov.bg/2014/AisCommunicator/v2")]
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
        public string ParentValue
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
