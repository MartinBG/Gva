using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using AutoMapper;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.LotEvents;
using Gva.Api.Mappers.Resolvers;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/apps")]
    public class ApplicationsController : GvaLotsController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public ApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            FileResolver fileResolver)
            : base(lotRepository, fileRepository, unitOfWork, lotEventDispatcher, fileResolver)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("")]
        public IHttpActionResult GetApplications(DateTime? fromDate = null, DateTime? toDate = null, string lin = null)
        {
            var applications = this.applicationRepository.GetApplications(fromDate, toDate, lin);

            return Ok(applications);
        }
    }
}