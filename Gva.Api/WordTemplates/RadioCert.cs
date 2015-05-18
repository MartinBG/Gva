using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using System.Linq;
using System.Collections.Generic;
using Common.Data;
using Gva.Api.Models.Views.Aircraft;
using System;

namespace Gva.Api.WordTemplates
{
    public class RadioCert : IDataGenerator
    {
        private ILotRepository lotRepository;
        private IUnitOfWork unitOfWork;

        public RadioCert(
            ILotRepository lotRepository,
            IUnitOfWork unitOfWork)
        {
            this.lotRepository = lotRepository;
            this.unitOfWork = unitOfWork;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "radio_cert" };
            }
        }

        public object GetData(int lotId, string path)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            AircraftDataDO aircraftData = lot.Index.GetPart<AircraftDataDO>("aircraftData").Content;
            AircraftCertRadioDO radioCertData = lot.Index.GetPart<AircraftCertRadioDO>(path).Content;

            string inspector = null;
            if(radioCertData.Inspector != null)
            {
                inspector = radioCertData.Inspector.Inspector != null? 
                    string.Format("{0} / {1}", radioCertData.Inspector.Inspector.Name, radioCertData.Inspector.Inspector.NameAlt) :
                    radioCertData.Inspector.Other;
            }
            var transmitters = radioCertData.Entries.Where(e => e.Equipment.Code == "TRM");
            var ELTs = radioCertData.Entries.Where(e => e.Equipment.Code == "ELT");
            var equipment = this.GetEquipment(radioCertData.Entries);

            var json = new
            {
                root = new
                {
                    NUMBER = radioCertData.AslNumber,
                    REG_MARK = radioCertData.RegMark,
                    AIRCRAFT_TYPE = radioCertData.ActType,
                    OWNER = radioCertData.OwnerOper != null ? radioCertData.OwnerOper.Name : null,
                    QTY1 = transmitters.Select(t => new { DATA = t.Count }),
                    MODEL1 = transmitters.Select(t => new { DATA = t.Model }),
                    FREQ1 = transmitters.Select(t => new { DATA = t.Bandwidth }),
                    CLASS1 = transmitters.Select(t => new { DATA = t.Class }),
                    POWER1 = transmitters.Select(t => new { DATA = t.Power }),
                    QTY2 = ELTs.Select(t => new { DATA = t.Count }),
                    MODEL2 = ELTs.Select(t => new { DATA = t.Model }),
                    FREQ2 = ELTs.Select(t => new { DATA = t.Bandwidth }),
                    CLASS2 = ELTs.Select(t => new { DATA = t.Class }),
                    POWER2 = ELTs.Select(t => new { DATA = t.Power }),
                    EQUIPMENT = equipment,
                    SIGNED_BY = inspector 
                }
            };

            return json;
        }

        private List<object> GetEquipment(List<AircraftCertRadioEntryDO> entries)
        {
            var transponders = entries.Where(e => e.Equipment.Code == "TRS");
            var weatherRadars = entries.Where(e => e.Equipment.Code == "WR");
            var TCASs = entries.Where(e => e.Equipment.Code == "TCAS");
            var DMEs = entries.Where(e => e.Equipment.Code == "DME");
            var radioAltimeters = entries.Where(e => e.Equipment.Code == "RA");
            var others = entries.Where(e => e.Equipment.Code == "Otr");

            List<object> equipments = new List<object>();
            equipments.Add(
            new
            {
                NAME = "7.1 Транспондер",
                NAME_ALT = "Transponders",
                QTY = transponders.Select(t => new { DATA = t.Count }),
                MODEL = transponders.Select(t => new { DATA = t.Model }),
                POWER = transponders.Select(t => new { DATA = t.Power }),
                CLASS = transponders.Select(t => new { DATA = t.Class }),
                FREQ = transponders.Select(t => new { DATA = t.Bandwidth })
            });

            equipments.Add(
            new
            {
                NAME = "7.2 Метео радар",
                NAME_ALT = "Weather Radar",
                QTY = weatherRadars.Select(t => new { DATA = t.Count }),
                MODEL = weatherRadars.Select(t => new { DATA = t.Model }),
                POWER = weatherRadars.Select(t => new { DATA = t.Power }),
                CLASS = weatherRadars.Select(t => new { DATA = t.Class }),
                FREQ = weatherRadars.Select(t => new { DATA = t.Bandwidth })
            });

            equipments.Add(
            new
            {
                NAME = "7.3 ТКАС",
                NAME_ALT = "TCAS",
                QTY = TCASs.Select(t => new { DATA = t.Count }),
                MODEL = TCASs.Select(t => new { DATA = t.Model }),
                POWER = TCASs.Select(t => new { DATA = t.Power }),
                CLASS = TCASs.Select(t => new { DATA = t.Class }),
                FREQ = TCASs.Select(t => new { DATA = t.Bandwidth })
            });

            equipments.Add(
            new
            {
                NAME = "7.4 ДМЕ",
                NAME_ALT = "DME",
                QTY = DMEs.Select(t => new { DATA = t.Count }),
                MODEL = DMEs.Select(t => new { DATA = t.Model }),
                POWER = DMEs.Select(t => new { DATA = t.Power }),
                CLASS = DMEs.Select(t => new { DATA = t.Class }),
                FREQ = DMEs.Select(t => new { DATA = t.Bandwidth })
            });

            equipments.Add(
              new
              {
                  NAME = "7.5 Висотомер",
                  NAME_ALT = "Radio Altimeter",
                  QTY = radioAltimeters.Select(t => new { DATA = t.Count }),
                  MODEL = radioAltimeters.Select(t => new { DATA = t.Model }),
                  POWER = radioAltimeters.Select(t => new { DATA = t.Power }),
                  CLASS = radioAltimeters.Select(t => new { DATA = t.Class }),
                  FREQ = radioAltimeters.Select(t => new { DATA = t.Bandwidth })
              });

            int index = 6;
            foreach (var other in others)
            {
                equipments.Add(
                new
                {
                    NAME = string.Format("7.{0} {1}", index, other.OtherType),
                    QTY = new { DATA = other.Count },
                    MODEL = new { DATA = other.Model },
                    POWER = new { DATA = other.Power },
                    CLASS = new { DATA = other.Class },
                    FREQ = new { DATA = other.Bandwidth }
                });
                index++;
            }

            return equipments;
        }
    }
}
