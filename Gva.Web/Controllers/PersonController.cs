using Common.Api.UserContext;
using Common.Data;
using Gva.Web.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gva.Web.Controllers
{
    public class PersonController : ApiController
    {
        private UserContext userContext;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;

        public PersonController(IUserContextProvider userContextProvider, IUnitOfWork unitOfWork, ILotRepository lotRepository)
        {
            this.userContext = userContextProvider.GetCurrentUserContext();
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
        }

        public HttpResponseMessage GetPerson(int lotId)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personDataPart = JObject.Parse(lot.GetPart("personData").TextContent);
            var personEmployment = lot.GetParts("personDocumentEmployments")
                .OrderByDescending(pv => pv.Part.Index)
                .FirstOrDefault();

            var personBirthDate = Convert.ToDateTime(personDataPart.Value<string>("dateOfBirth"));
            DateTime now = DateTime.Today;
            int age = now.Year - personBirthDate.Year;
            if (now < personBirthDate.AddYears(age))
            {
                age--;
            }

            Person person = new Person()
            {
                Id = lot.LotId,
                Lin = personDataPart.Value<string>("lin"),
                Uin = personDataPart.Value<string>("uin"),
                Names = string.Format(
                    "{0} {1} {2}",
                    personDataPart.Value<string>("firstName"),
                    personDataPart.Value<string>("middleName"),
                    personDataPart.Value<string>("lastName")),
                Age = age
            };

            if (personEmployment != null)
            {
                var personEmploymentPart = JObject.Parse(personEmployment.TextContent);

                var organization = personEmploymentPart.Value<JObject>("organization");
                person.Organization = organization == null ? null : organization.Value<string>("name");
                person.Employment = personEmploymentPart.Value<JObject>("employmentCategory").Value<string>("name");
            }

            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                person);
        }

        public HttpResponseMessage PostPerson(JObject person)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var newLot = this.lotRepository.GetSet("Person").CreateLot(this.userContext);

                newLot.CreatePart("personData", person.Value<JObject>("personData"), this.userContext);

                newLot.CreatePart("personDocumentIds/*", person.Value<JObject>("personDocumentId"), this.userContext);

                newLot.CreatePart("personAddresses/*", person.Value<JObject>("personAddress"), this.userContext);

                newLot.Commit(this.userContext);

                transaction.Commit();
            }
            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
