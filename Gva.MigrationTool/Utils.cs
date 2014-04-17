using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.Api.Repositories.FileRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Common.Json;
using Gva.Api.ModelsDO;
using Common.Api.UserContext;
using Common.Data;
using Common.Api.Models;
using Docs.Api.Models;
using Gva.Api.Models;
using Gva.Api.LotEventHandlers;
using Regs.Api.LotEvents;
using Gva.Api.LotEventHandlers.AircraftView;
using Gva.Api.LotEventHandlers.InventoryView;
using Gva.Api.LotEventHandlers.ApplicationView;
using Gva.Api.LotEventHandlers.PersonView;
using Gva.Api.LotEventHandlers.OrganizationView;
using Gva.Api.LotEventHandlers.EquipmentView;
using Gva.Api.LotEventHandlers.AirportView;
using Common.Api.Repositories.UserRepository;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Newtonsoft.Json;

namespace Gva.MigrationTool
{
    public class Utils
    {
        public const string DUMMY_FILE_KEY = "7C0604F9-FB44-4CCD-BE0E-66E82142AE76";
        public static readonly JObject DUMMY_PILOT_CASE_TYPE = 
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "Пилот"));
        public static readonly JObject DUMMY_ORGANIZATION =
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "org1"));
        public static readonly JObject DUMMY_AIRCRAFT =
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "ac1"));

        public static JObject ToJObject(object o)
        {
            return JObject.FromObject(o);
        }

        public static JObject Pluck(JObject o, string[] keys)
        {
            if (o == null)
            {
                return null;
            }

            var keysToRemove =
                ((IDictionary<string, JToken>)o).Keys
                .Where(k => !keys.Contains(k))
                .ToList();

            foreach (var key in keysToRemove)
            {
                o.Remove(key);
            }

            return o;
        }

        public static IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(
                new IDbConfiguration[]
                {
                    new RegsDbConfiguration(),
                    new CommonDbConfiguration(),
                    new DocsDbConfiguration(),
                    new GvaDbConfiguration()
                });
        }

        public static ILotEventDispatcher CreateLotEventDispatcher(IUnitOfWork unitOfWork, IUserRepository userRepository, IAircraftRegistrationAwRepository aircraftRegistrationAwRepository)
        {
            return new LotEventDispatcher(new List<ILotEventHandler>()
                {
                    new AircraftRegistrationAwHandler(unitOfWork),
                    new AircraftRegistrationHandler(unitOfWork, aircraftRegistrationAwRepository),
                    new AircraftRegistrationNewAwHandler(unitOfWork),
                    new AircraftRegistrationNumberHandler(unitOfWork),
                    new AircraftViewDataHandler(unitOfWork),
                    new AirportDataHandler(unitOfWork),
                    new ApplicationsViewAircraftHandler(unitOfWork),
                    new ApplicationsViewAirportHandler(unitOfWork),
                    new ApplicationsViewEquipmentHandler(unitOfWork),
                    new ApplicationsViewOrganizationHandler(unitOfWork),
                    new ApplicationsViewPersonHandler(unitOfWork),
                    new EquipmentDataHandler(unitOfWork),
                    new AircraftApplicationHandler(unitOfWork, userRepository),
                    new AircraftDebtHandler(unitOfWork, userRepository),
                    new AircraftInspectionHandler(unitOfWork, userRepository),
                    new AircraftOccurrenceHandler(unitOfWork, userRepository),
                    new AircraftOtherHandler(unitOfWork, userRepository),
                    new AircraftOwnerHandler(unitOfWork, userRepository),
                    new AirportApplicationHandler(unitOfWork, userRepository),
                    new AirportInspectionHandler(unitOfWork, userRepository),
                    new AirportOtherHandler(unitOfWork, userRepository),
                    new AirportOwnerHandler(unitOfWork, userRepository),
                    new EquipmentApplicationHandler(unitOfWork, userRepository),
                    new EquipmentInspectionHandler(unitOfWork, userRepository),
                    new EquipmentOtherHandler(unitOfWork, userRepository),
                    new EquipmentOwnerHandler(unitOfWork, userRepository),
                    new OrganizationApplicationHandler(unitOfWork, userRepository),
                    new OrganizationOtherHandler(unitOfWork, userRepository),
                    new PersonApplicationHandler(unitOfWork, userRepository),
                    new PersonCheckHandler(unitOfWork, userRepository),
                    new PersonDocumentIdHandler(unitOfWork, userRepository),
                    new PersonEducationHandler(unitOfWork, userRepository),
                    new PersonEmploymentHandler(unitOfWork, userRepository),
                    new PersonMedicalHandler(unitOfWork, userRepository),
                    new PersonOtherHandler(unitOfWork, userRepository),
                    new PersonTrainingHandler(unitOfWork, userRepository),
                    new OrganizationViewDataHandler(unitOfWork),
                    new PersonViewDataHandler(unitOfWork),
                    new PersonViewEmploymentHandler(unitOfWork),
                    new PersonViewLicenceHandler(unitOfWork),
                    new PersonViewRatingHandler(unitOfWork)
                });
        }

        public static JObject GetPerson(int? id, IPersonRepository personRepository)
        {
            if (id == null)
            {
                return null;
            }
            var person = personRepository.GetPerson((int)id);
            return JObject.FromObject(new
            {
                nomValueId = person.LotId,
                name = person.Names
            });
        }

        public static JObject GetAircraft(int? id, IAircraftRepository aircraftRepository)
        {
            if (id == null)
            {
                return null;
            }
            var aircraft = aircraftRepository.GetAircraft((int)id);
            return JObject.FromObject(new
            {
                nomValueId = aircraft.LotId,
                name = aircraft.Model
            });
        }

        public static JObject GetOrganization(int? id, IOrganizationRepository organizationRepository)
        {
            if (id == null)
            {
                return null;
            }
            var organization = organizationRepository.GetOrganization((int)id);
            return JObject.FromObject(new
            {
                nomValueId = organization.LotId,
                name = organization.Name
            });
        }
    }
}
