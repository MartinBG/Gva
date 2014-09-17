﻿using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personAddresses")]
    [Authorize]
    public class PersonAddressesController : GvaApplicationPartController<PersonAddressDO>
    {
        private INomRepository nomRepository;

        public PersonAddressesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personAddresses", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewAddress(int lotId)
        {
            PersonAddressDO newAddress = new PersonAddressDO();
            newAddress.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new ApplicationPartVersionDO<PersonAddressDO>(newAddress));
        }
    }
}