using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertRegistrationsFM")]
    [Authorize]
    public class AircraftCertRegistrationsFMController : GvaCaseTypePartController<AircraftCertRegistrationFMDO>
    {
        private string path;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;
        private INomRepository nomRepository;
        private IUnitOfWork unitOfWork;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;


        public AircraftCertRegistrationsFMController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,

            UserContext userContext)
            : base("aircraftCertRegistrationsFM", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "aircraftCertRegistrationsFM";
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.nomRepository = nomRepository;
            this.unitOfWork = unitOfWork;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertRegFM(int lotId)
        {
            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("aircraft").Single();
            CaseDO caseDO = new CaseDO()
            {
                CaseType = new NomValue()
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                }
            };
            NomValue status = null;
            var registrations = this.lotRepository.GetLotIndex(lotId).Index
                    .GetParts<AircraftCertRegistrationFMDO>(this.path)
                    .ToList();

            if (registrations.Count > 0)
            {
                status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "lastActiveReg");
            }
            else
            {
                status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "firstReg");
            }

            AircraftCertRegistrationFMDO newCertRegFM = new AircraftCertRegistrationFMDO()
            {
                IsActive = true,
                IsCurrent = true,
                OwnerIsOrg = true,
                OperIsOrg = true,
                LessorIsOrg = true,
                Status = status
            };

            return Ok(new CaseTypePartDO<AircraftCertRegistrationFMDO>(newCertRegFM, caseDO));
        }

        [Route("view")]
        [Route("{partIndex}/view")]
        public IHttpActionResult GetRegistrationView(int lotId, int? partIndex = null)
        {
            var index = this.lotRepository.GetLotIndex(lotId).Index;
            var registrations = index.GetParts<AircraftCertRegistrationFMDO>(this.path);
            var airworthinesses = index.GetParts<AircraftCertAirworthinessDO>("aircraftCertAirworthinessesFM");

            if (registrations.Length > 0)
            {
                return Ok(this.CreateRegistrationView(registrations, airworthinesses, partIndex));
            }
            else
            {
                return Ok();
            }
        }

        public override IHttpActionResult DeletePart(int lotId, int partIndex)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var partVersion = lot.DeletePart<AircraftCertRegistrationFMDO>(string.Format("{0}/{1}", this.path, partIndex), this.userContext);

                var registrations = this.lotRepository.GetLotIndex(lotId).Index
                    .GetParts<AircraftCertRegistrationFMDO>(this.path)
                    .OrderByDescending(r => r.Content.ActNumber)
                    .ToList();

                if (registrations.Count > 0)
                {
                    var lastRegistration = registrations.FirstOrDefault();
                    lastRegistration.Content.IsCurrent = true;
                    lot.UpdatePart(this.path + "/" + lastRegistration.Part.Index, lastRegistration.Content, this.userContext);
                }

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        [Route("{partIndex}/removeDereg")]
        public IHttpActionResult RemoveDereg(int lotId, int partIndex, CaseTypePartDO<AircraftCertRegistrationFMDO> partVersionDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var registrations = this.lotRepository.GetLotIndex(lotId).Index
                   .GetParts<AircraftCertRegistrationFMDO>(this.path)
                   .ToList();

                partVersionDO.Part.IsActive = true;
                partVersionDO.Part.Removal = null;

                if (registrations.Count > 1)
                {
                    partVersionDO.Part.Status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "lastActiveReg");
                }
                else
                {
                    partVersionDO.Part.Status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "firstReg");
                }

                PartVersion<AircraftCertRegistrationFMDO> partVersion = lot.UpdatePart(
                    string.Format("{0}/{1}", this.path, partIndex),
                    partVersionDO.Part,
                    this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, partVersionDO.Case);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        private RegistrationViewDO CreateRegistrationView(
            IEnumerable<PartVersion<AircraftCertRegistrationFMDO>> registrations,
            IEnumerable<PartVersion<AircraftCertAirworthinessDO>> airworthinesses,
            int? regPartIndex)
        {
            var airworthinessesWithReg = airworthinesses.Where(
                a => a.Content.Registration != null &&
                     a.Content.AirworthinessCertificateType.Alias != "f25" &&
                     a.Content.AirworthinessCertificateType.Alias != "f24");

            var regs =
                (from r in registrations
                 join aw in airworthinessesWithReg on r.Part.Index equals aw.Content.Registration.NomValueId into gaws
                 from aw in gaws.DefaultIfEmpty()
                 group aw by r into aws
                 orderby aws.Key.Content.ActNumber descending
                 select aws)
                .Select((aws, i) =>
                    new
                    {
                        Position = i,
                        Reg = aws.Key,
                        Aws = aws.Where(aw => aw != null).OrderByDescending(a => a.Content.IssueDate).AsEnumerable()
                    })
                .ToList();

            var currentReg =
                //used to create a variable from this annonymous type
                new
                {
                    Position = default(int),
                    Reg = default(PartVersion<AircraftCertRegistrationFMDO>),
                    Aws = Enumerable.Empty<PartVersion<AircraftCertAirworthinessDO>>()
                };

            if (regPartIndex.HasValue)
            {
                currentReg = regs.Where(r => r.Reg.Part.Index == regPartIndex).FirstOrDefault();
            }
            else
            {
                currentReg = regs.FirstOrDefault();
            }

            var currentRegAw = regs
                .Where(r => r.Position >= currentReg.Position && r.Aws.Any())
                .Select(r =>
                    new
                    {
                        Aw = r.Aws.First(),
                        IsForCurrentReg = r == currentReg
                    })
                .FirstOrDefault();

            int position = currentReg.Position;

            return new RegistrationViewDO() { 
                CurrentIndex = currentReg.Reg.Part.Index,
                AirworthinessIndex =  currentRegAw != null ? currentRegAw.Aw.Part.Index : (int?)null,
                HasAirworthiness = currentRegAw != null ? currentRegAw.IsForCurrentReg : false,
                LastIndex = regs[0].Reg.Part.Index,
                NextIndex = position > 0 ? regs[position - 1].Reg.Part.Index : (int?)null,
                PrevIndex = position < regs.Count - 1 ? regs[position + 1].Reg.Part.Index : (int?)null,
                FirstIndex = regs[regs.Count - 1].Reg.Part.Index,
                LastReg = regs[0].Reg.Content,
                FirstReg = regs[regs.Count - 1].Reg.Content
            };
        }

        public override IHttpActionResult PostNewPart(int lotId, CaseTypePartDO<AircraftCertRegistrationFMDO> partVersionDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                PartVersion<AircraftCertRegistrationFMDO> certRegistartionPartVersion = null;

                var registrations = lot.Index
                    .GetParts<AircraftCertRegistrationFMDO>(this.path)
                    .OrderByDescending(r => r.CreateDate)
                    .ToList();

                if (registrations.Count > 0)
                {
                    //update previous registration
                    var oldRegistration = registrations.First();

                    oldRegistration.Content.IsActive = false;
                    oldRegistration.Content.IsCurrent = false;

                    if (oldRegistration.Content.Status.Alias == "firstReg" || oldRegistration.Content.Status.Alias == "lastActiveReg")
                    {
                        oldRegistration.Content.Status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "rereged");
                    }

                    PartVersion<AircraftCertRegistrationFMDO> oldRegistrationPartVersion = lot.UpdatePart(
                        string.Format("{0}/{1}", this.path, oldRegistration.Part.Index),
                        oldRegistration.Content,
                        this.userContext);
                    this.fileRepository.AddFileReference(oldRegistrationPartVersion.Part, partVersionDO.Case);

                    lot.Commit(this.userContext, lotEventDispatcher);
                    this.unitOfWork.Save();
                    this.lotRepository.ExecSpSetLotPartTokens(oldRegistrationPartVersion.PartId);

                    //crete new registration
                    certRegistartionPartVersion = lot.CreatePart<AircraftCertRegistrationFMDO>(this.path + "/*", partVersionDO.Part, this.userContext);
                    this.fileRepository.AddFileReference(certRegistartionPartVersion.Part, partVersionDO.Case);

                    lot.Commit(this.userContext, lotEventDispatcher);
                    this.unitOfWork.Save();
                }
                else
                {
                    certRegistartionPartVersion = lot.CreatePart<AircraftCertRegistrationFMDO>(this.path + "/*", partVersionDO.Part, this.userContext);
                    this.fileRepository.AddFileReference(certRegistartionPartVersion.Part, partVersionDO.Case);
                    lot.Commit(this.userContext, lotEventDispatcher);
                    this.unitOfWork.Save();
                }

                this.lotRepository.ExecSpSetLotPartTokens(certRegistartionPartVersion.PartId);
                transaction.Commit();

                return Ok(new CaseTypePartDO<AircraftCertRegistrationFMDO>(certRegistartionPartVersion));
            }
        }

        public override IHttpActionResult PostPart(int lotId, int partIndex, CaseTypePartDO<AircraftCertRegistrationFMDO> partVersionDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                if (partVersionDO.Part.Removal != null && partVersionDO.Part.Removal.Reason != null)
                {
                    switch (partVersionDO.Part.Removal.Reason.Alias)
                    {
                        case "order":
                            partVersionDO.Part.Status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "removedByOrder");
                            break;
                        case "expiredContract":
                            partVersionDO.Part.Status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "expiredContract");
                            break;
                        case "changedOwnership":
                            partVersionDO.Part.Status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "changedOwnership");
                            break;
                        case "totaled":
                            partVersionDO.Part.Status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "totaled");
                            break;
                        default:
                            partVersionDO.Part.Status = this.nomRepository.GetNomValue("aircraftRegStatsesFm", "removed");
                            break;
                    }
                }

                PartVersion<AircraftCertRegistrationFMDO> partVersion = lot.UpdatePart(
                    string.Format("{0}/{1}", this.path, partIndex),
                    partVersionDO.Part,
                    this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, partVersionDO.Case);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }
    }
}