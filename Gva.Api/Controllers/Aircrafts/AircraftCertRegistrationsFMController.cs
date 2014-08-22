using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.FileRepository;
using Common.Api.UserContext;
using Regs.Api.Models;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertRegistrationsFM")]
    [Authorize]
    public class AircraftCertRegistrationsFMController : GvaApplicationPartController<AircraftCertRegistrationFMDO>
    {
        private string path;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private IUnitOfWork unitOfWork;
        private ILotEventDispatcher lotEventDispatcher;

        public AircraftCertRegistrationsFMController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("aircraftCertRegistrationsFM", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
            this.path = "aircraftCertRegistrationsFM";
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.unitOfWork = unitOfWork;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertRegFM ()
        {
            AircraftCertRegistrationFMDO newCertRegFM = new AircraftCertRegistrationFMDO()
            {
                IsActive = true,
                IsCurrent = true,
                OwnerIsOrg = true,
                OperIsOrg = true,
                LessorIsOrg = true
            };

            return Ok(new ApplicationPartVersionDO<AircraftCertRegistrationFMDO>(newCertRegFM));
        }

        [Route("{partIndex}/debts")]
        public IHttpActionResult GetRegistrationDebts(int lotId, int partIndex)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).Index.GetParts("aircraftDocumentDebtsFM").Where(e => e.Content.Get<int>("registration.partIndex") == partIndex);

            return Ok(parts.Select(pv => new PartVersionDO(pv)));
        }

        [Route("view")]
        [Route("{partIndex}/view")]
        public IHttpActionResult GetRegistrationView(int lotId, int? partIndex = null)
        {
            var index = this.lotRepository.GetLotIndex(lotId).Index;
            var registrations = index.GetParts("aircraftCertRegistrationsFM");
            var airworthinesses = index.GetParts("aircraftCertAirworthinessesFM");
            if (registrations.Length > 0)
            {
                return Ok(CreateRegistrationView(registrations, airworthinesses, partIndex));
            }
            else
            {
                return Ok();
            }
        }

        public override IHttpActionResult GetParts(int lotId)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).Index.GetParts(this.path).OrderByDescending(e => e.Content.Get<int>("actNumber"));

            return Ok(parts.Select(pv => new ApplicationPartVersionDO<AircraftCertRegistrationFMDO>(pv)));
        }

        public override IHttpActionResult DeletePart(int lotId, int partIndex)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            var partVersion = lot.DeletePart(string.Format("{0}/{1}", this.path, partIndex), userContext);
            this.fileRepository.DeleteFileReferences(partVersion);
            this.applicationRepository.DeleteApplicationRefs(partVersion);
            var registrations = this.lotRepository.GetLotIndex(lotId).Index.GetParts(this.path).ToList();
            if (registrations.Count > 0)
            {
                var lastRegistration = registrations.FirstOrDefault();
                lastRegistration.Content.Property("isCurrent").Value = true;
                lot.UpdatePart(this.path + "/" + lastRegistration.Part.Index, lastRegistration.Content, userContext);
            }
            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        public RegistrationViewDO CreateRegistrationView(IEnumerable<PartVersion> registrations, IEnumerable<PartVersion> airworthinesses, int? regPartIndex)
        {
            var regs =
                (from r in registrations
                 join aw in airworthinesses on r.Part.Index equals aw.Content.Get<int>("registration.partIndex") into gaws
                 from aw in gaws.DefaultIfEmpty()
                 group aw by r into aws
                 orderby aws.Key.Content.Get<int>("actNumber") descending
                 select aws)
                .Select((aws, i) =>
                    new
                    {
                        Position = i,
                        Reg = aws.Key,
                        Aws = aws.Where(aw => aw != null).OrderByDescending(a => a.Content.Get<DateTime>("issueDate")).AsEnumerable()
                    })
                .ToList();

            var currentReg =
                //used to create a variable from this annonymous type
                new
                {
                    Position = default(int),
                    Reg = default(PartVersion),
                    Aws = Enumerable.Empty<PartVersion>()
                };

            if (regPartIndex.HasValue)
            {
                currentReg = regs.Where(r => r.Reg.Part.Index == regPartIndex).FirstOrDefault();
            }
            else
            {
                currentReg = regs.FirstOrDefault();
            }

            RegistrationViewDO regView = new RegistrationViewDO();
            regView.CurrentIndex = currentReg.Reg.Part.Index;

            var currentRegAw = regs
                .Where(r => r.Position >= currentReg.Position && r.Aws.Any())
                .Select(r =>
                    new
                    {
                        Aw = r.Aws.First(),
                        IsForCurrentReg = r == currentReg
                    })
                .FirstOrDefault();

            if (currentRegAw != null)
            {
                regView.AirworthinessIndex = currentRegAw.Aw.Part.Index;
                regView.HasAirworthiness = currentRegAw.IsForCurrentReg;
            }
            else
            {
                regView.AirworthinessIndex = null;
                regView.HasAirworthiness = false;
            }

            int position = currentReg.Position;

            regView.LastIndex = regs[0].Reg.Part.Index;
            regView.NextIndex = position > 0 ? regs[position - 1].Reg.Part.Index : (int?)null;
            regView.PrevIndex = position < regs.Count - 1 ? regs[position + 1].Reg.Part.Index : (int?)null;
            regView.FirstIndex = regs[regs.Count - 1].Reg.Part.Index;

            regView.LastReg = regs[0].Reg.Content;
            regView.FirstReg = regs[regs.Count - 1].Reg.Content;

            return regView;
        }
    }
}