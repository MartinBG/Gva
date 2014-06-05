using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Aop.RioBridge
{
    public class AopRioBridgeMapperProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<RioObjects.IHeaderFooterDocumentsRioApplication, Common.Rio.PortalBridge.RioObjects.RioApplication>()
                .ForMember(a => a.ApplicationSigningTime, o => o.MapFrom(a => a.ApplicationSigningTime))
                .ForMember(a => a.AttachedDocuments, o => o.MapFrom(a => a.AttachedDocuments))
                .ForMember(a => a.DocumentTypeName, o => o.MapFrom(a => a.DocumentTypeName))
                .ForMember(a => a.DocumentTypeURI, o => o.MapFrom(a => a.DocumentTypeURI))
                .ForMember(a => a.DocumentURI, o => o.MapFrom(a => a.DocumentURI))
                .ForMember(a => a.ElectronicAdministrativeServiceFooter, o => o.MapFrom(a => a.ElectronicAdministrativeServiceFooter))
                .ForMember(a => a.ElectronicAdministrativeServiceHeader, o => o.MapFrom(a => a.ElectronicAdministrativeServiceHeader))
                .ForMember(a => a.ElectronicServiceProviderBasicData, o => o.MapFrom(a => a.ElectronicServiceProviderBasicData))
                .ForMember(a => a.EmailAddress, o => o.MapFrom(a => a.EmailAddress))
                .ForMember(a => a.SendApplicationWithReceiptAcknowledgedMessage, o => o.MapFrom(a => a.SendApplicationWithReceiptAcknowledgedMessage))
                .ForMember(a => a.SUNAUServiceName, o => o.MapFrom(a => a.SUNAUServiceName))
                .ForMember(a => a.SUNAUServiceURI, o => o.MapFrom(a => a.SUNAUServiceURI));

            Mapper.CreateMap<RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature, Common.Rio.PortalBridge.RioObjects.ElectronicDocumentDiscrepancyTypeNomenclature>();

            Mapper.CreateMap<Common.Rio.PortalBridge.RioObjects.ReceiptNotAcknowledgedMessage, R_0009_000017.ReceiptNotAcknowledgedMessage>();
            Mapper.CreateMap<Common.Rio.PortalBridge.RioObjects.ReceiptAcknowledgedMessage, R_0009_000019.ReceiptAcknowledgedMessage>();

            Mapper.CreateMap<R_0009_000152.ElectronicAdministrativeServiceHeader, Common.Rio.PortalBridge.RioObjects.ElectronicAdministrativeServiceHeader>();
            Mapper.CreateMap<R_0009_000139.AttachedDocument, Common.Rio.PortalBridge.RioObjects.AttachedDocument>();
            Mapper.CreateMap<R_0009_000153.ElectronicAdministrativeServiceFooter, Common.Rio.PortalBridge.RioObjects.ElectronicAdministrativeServiceFooter>();

            Mapper.CreateMap<R_0009_000137.ElectronicServiceApplicantContactData, Common.Rio.PortalBridge.RioObjects.ElectronicServiceApplicantContactData>().ReverseMap();
            Mapper.CreateMap<R_0009_000137.PhoneNumbers, Common.Rio.PortalBridge.RioObjects.PhoneNumbers>().ReverseMap();
            Mapper.CreateMap<R_0009_000137.FaxNumbers, Common.Rio.PortalBridge.RioObjects.FaxNumbers>().ReverseMap();
            Mapper.CreateMap<R_0009_000016.ElectronicServiceApplicant, Common.Rio.PortalBridge.RioObjects.ElectronicServiceApplicant>().ReverseMap();
            Mapper.CreateMap<R_0009_000016.RecipientGroup, Common.Rio.PortalBridge.RioObjects.RecipientGroup>().ReverseMap();
            Mapper.CreateMap<R_0009_000015.ElectronicServiceRecipient, Common.Rio.PortalBridge.RioObjects.ElectronicServiceRecipient>().ReverseMap();
            Mapper.CreateMap<R_0009_000014.ForeignEntityBasicData, Common.Rio.PortalBridge.RioObjects.ForeignEntityBasicData>().ReverseMap();
            Mapper.CreateMap<R_0009_000012.ElectronicStatementAuthor, Common.Rio.PortalBridge.RioObjects.ElectronicStatementAuthor>().ReverseMap();
            Mapper.CreateMap<R_0009_000008.PersonBasicData, Common.Rio.PortalBridge.RioObjects.PersonBasicData>().ReverseMap();
            Mapper.CreateMap<R_0009_000006.PersonIdentifier, Common.Rio.PortalBridge.RioObjects.PersonIdentifier>().ReverseMap();
            Mapper.CreateMap<R_0009_000005.PersonNames, Common.Rio.PortalBridge.RioObjects.PersonNames>().ReverseMap();
            Mapper.CreateMap<R_0009_000011.ForeignCitizenBasicData, Common.Rio.PortalBridge.RioObjects.ForeignCitizenBasicData>().ReverseMap();
            Mapper.CreateMap<R_0009_000007.ForeignCitizenNames, Common.Rio.PortalBridge.RioObjects.ForeignCitizenNames>().ReverseMap();
            Mapper.CreateMap<R_0009_000009.ForeignCitizenPlaceOfBirth, Common.Rio.PortalBridge.RioObjects.ForeignCitizenPlaceOfBirth>().ReverseMap();
            Mapper.CreateMap<R_0009_000010.ForeignCitizenIdentityDocument, Common.Rio.PortalBridge.RioObjects.ForeignCitizenIdentityDocument>().ReverseMap();
            Mapper.CreateMap<R_0009_000003.DocumentTypeURI, Common.Rio.PortalBridge.RioObjects.DocumentTypeURI>().ReverseMap();
            Mapper.CreateMap<R_0009_000001.DocumentURI, Common.Rio.PortalBridge.RioObjects.DocumentURI>().ReverseMap();
            Mapper.CreateMap<R_0009_000017.Discrepancies, Common.Rio.PortalBridge.RioObjects.Discrepancies>().ReverseMap();
            Mapper.CreateMap<R_0009_000002.ElectronicServiceProviderBasicData, Common.Rio.PortalBridge.RioObjects.ElectronicServiceProviderBasicData>().ReverseMap();
            Mapper.CreateMap<R_0009_000013.EntityBasicData, Common.Rio.PortalBridge.RioObjects.EntityBasicData>().ReverseMap();
            Mapper.CreateMap<R_0009_000019.RegisteredBy, Common.Rio.PortalBridge.RioObjects.RegisteredBy>().ReverseMap();
            Mapper.CreateMap<R_0009_000019.Officer, Common.Rio.PortalBridge.RioObjects.Officer>().ReverseMap();
            Mapper.CreateMap<R_0009_000018.AISUserNames, Common.Rio.PortalBridge.RioObjects.AISUserNames>().ReverseMap();

            Common.Rio.PortalBridge.MapperEx.CreateCollectionMap<R_0009_000016.RecipientGroupCollection, R_0009_000016.RecipientGroup, Common.Rio.PortalBridge.RioObjects.RecipientGroupCollection, Common.Rio.PortalBridge.RioObjects.RecipientGroup>();
            Common.Rio.PortalBridge.MapperEx.CreateCollectionMap<R_0009_000017.DiscrepancyCollection, string, Common.Rio.PortalBridge.RioObjects.DiscrepancyCollection, string>();
            Common.Rio.PortalBridge.MapperEx.CreateCollectionMap<R_0009_000016.ElectronicStatementAuthorCollection, R_0009_000012.ElectronicStatementAuthor, Common.Rio.PortalBridge.RioObjects.ElectronicStatementAuthorCollection, Common.Rio.PortalBridge.RioObjects.ElectronicStatementAuthor>();
            Common.Rio.PortalBridge.MapperEx.CreateCollectionMap<R_0009_000016.ElectronicServiceRecipientCollection, R_0009_000015.ElectronicServiceRecipient, Common.Rio.PortalBridge.RioObjects.ElectronicServiceRecipientCollection, Common.Rio.PortalBridge.RioObjects.ElectronicServiceRecipient>();
            Common.Rio.PortalBridge.MapperEx.CreateCollectionMap<R_0009_000137.PhoneNumberCollection, string, Common.Rio.PortalBridge.RioObjects.PhoneNumberCollection, string>();
            Common.Rio.PortalBridge.MapperEx.CreateCollectionMap<R_0009_000137.ElectronicServiceApplicantFaxNumberCollection, string, Common.Rio.PortalBridge.RioObjects.ElectronicServiceApplicantFaxNumberCollection, string>();
        }
    }
}
