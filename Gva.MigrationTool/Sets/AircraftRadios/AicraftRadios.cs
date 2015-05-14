using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Common.Api.Models;
using Common.Json;
using Excel;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.MigrationTool.Nomenclatures;
using Newtonsoft.Json.Linq;

namespace Gva.MigrationTool.AircarftRadios
{
    public class AircraftRadios
    {
        public Dictionary<int, List<AircraftCertRadioDO>> Get(
            string radiosFilePath,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            ConcurrentDictionary<string, int> regMarkToLotId,
            Func<string, JObject> getOrgByFmOrgName)
        {
            IExcelDataReader excelReader;
            using (var fs = new FileStream(radiosFilePath, FileMode.Open, FileAccess.Read))
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(fs);
            }

            Dictionary<int, List<AircraftCertRadioDO>> certRadios = new Dictionary<int, List<AircraftCertRadioDO>>();
            int row = 0;
            while (excelReader.Read())
            {
                Console.Write("Migrating row {0} from Radio_Certificates.xsl \n", row);
                row++;
                string aslNumber =  (string)excelReader[0];
                string regMark = (string)excelReader[2];

                if(string.IsNullOrEmpty(aslNumber) || aslNumber == "ASL_No" || !regMarkToLotId.ContainsKey(regMark))
                {
                    continue;
                }

                double? date = excelReader[1] != null? (double)excelReader[1] : (double?)null;
                string orgNameEn = (string)excelReader[4];
                JObject org = !string.IsNullOrEmpty(orgNameEn) ? getOrgByFmOrgName(orgNameEn) : null;
                AircraftCertRadioDO radio = new AircraftCertRadioDO()
                {
                    RegMark = regMark,
                    IssueDate = date != null ? DateTime.FromOADate((double)date) : (DateTime?)null,
                    AslNumber = int.Parse(aslNumber),
                    ActType = (string)excelReader[3],
                    OwnerOper = org != null ? new NomValue()
                    {
                        Name = org.Get<string>("Name"),
                        NomValueId = org.Get<int>("NomValueId"),
                        Code = org.Get<string>("Code")
                    } : null,
                    Inspector = new AircraftInspectorDO() 
                    {
                        Other = (string)excelReader[5]
                    }
                };

                //tr1
                object tr1Count = excelReader[7];
                object tr1Model = excelReader[8];
                object tr1Power = excelReader[9];
                object tr1Class = excelReader[10];
                object tr1Bandwidth = excelReader[11];
                if (tr1Count != null ||
                    tr1Bandwidth != null ||
                    tr1Class != null ||
                    tr1Power != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                            Count = tr1Count != null ? tr1Count.ToString() : null,
                            Model = tr1Model != null ? tr1Model.ToString() : null,
                            Bandwidth = tr1Bandwidth != null ? tr1Bandwidth.ToString() : null,
                            Class = tr1Class != null ? tr1Class.ToString() : null,
                            Power = tr1Power != null ? tr1Power.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("TRM")
                        });
                }

                //tr2
                object tr2Count = excelReader[12];
                object tr2Model = excelReader[13];
                object tr2Power = excelReader[14];
                object tr2Class = excelReader[15];
                object tr2Bandwidth = excelReader[16];
                if (tr2Count != null ||
                    tr2Bandwidth != null ||
                    tr2Class != null ||
                    tr2Power != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                            Count = tr2Count != null ? tr2Count.ToString() : null,
                            Model = tr2Model != null ? tr2Model.ToString() : null,
                            Bandwidth = tr2Bandwidth != null ? tr2Bandwidth.ToString() : null,
                            Class = tr2Class != null ? tr2Class.ToString() : null,
                            Power = tr2Power != null ? tr2Power.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("TRM")
                        });
                }

                //ELT
                object ELTCount = excelReader[17];
                object ELTModel = excelReader[18];
                object ELTPower = excelReader[19];
                object ELTClass = excelReader[20];
                object ELTBandwidth = excelReader[21];
                if(ELTCount != null ||
                    ELTBandwidth != null ||
                    ELTClass != null ||
                    ELTPower != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                            Count = ELTCount != null ? ELTCount.ToString() : null,
                            Model = ELTModel != null ? ELTModel.ToString() : null,
                            Bandwidth = ELTBandwidth != null ? ELTBandwidth.ToString() : null,
                            Class = ELTClass != null ? ELTClass.ToString() : null,
                            Power = ELTPower != null ? ELTPower.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("ELT")
                        });
                }

                //TRS
                object tsCount = excelReader[22];
                object tsModel = excelReader[23];
                object tsPower = excelReader[24];
                object tsClass = excelReader[25];
                object tsBandwidth = excelReader[26];
                if(tsCount != null ||
                    tsBandwidth != null ||
                    tsClass != null ||
                    tsPower != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                            Count = tsCount != null ? tsCount.ToString() : null,
                            Model = tsModel != null ? tsModel.ToString() : null,
                            Bandwidth = tsBandwidth != null ? tsBandwidth.ToString() : null,
                            Class = tsClass != null ? tsClass.ToString() : null,
                            Power = tsPower != null ? tsPower.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("TRS")
                        });
                }

                //WR
                object WRCount = excelReader[27];
                object WRModel = excelReader[28];
                object WRPower = excelReader[29];
                object WRClass = excelReader[30];
                object WRBandwidth = excelReader[31];
                if(WRCount != null ||
                    WRBandwidth != null ||
                    WRClass != null ||
                    WRPower != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                            Count = WRCount != null ? WRCount.ToString() : null,
                            Model = WRModel != null ? WRModel.ToString() : null,
                            Bandwidth = WRBandwidth != null ? WRBandwidth.ToString() : null,
                            Class = WRClass != null ? WRClass.ToString() : null,
                            Power = WRPower != null ? WRPower.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("WR")
                        });
                }

                //TCAS
                object tcasCount = excelReader[32];
                object tcasModel = excelReader[33];
                object tcasPower = excelReader[34];
                object tcasClass = excelReader[35];
                object tcasBandwidth = excelReader[36];
                if(tcasCount != null ||
                    tcasBandwidth != null ||
                    tcasClass != null ||
                    tcasPower != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                            Count = tcasCount != null ? tcasCount.ToString() : null,
                            Model = tcasModel != null ? tcasModel.ToString() : null,
                            Bandwidth = tcasBandwidth != null ? tcasBandwidth.ToString() : null,
                            Class = tcasClass != null ? tcasClass.ToString() : null,
                            Power = tcasPower != null ? tcasPower.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("TCAS")
                        });
                }

                //DME
                object dmeCount = excelReader[37];
                object dmeModel = excelReader[38];
                object dmePower = excelReader[39];
                object dmeClass = excelReader[40];
                object dmeBandwidth = excelReader[41];
                if(dmeCount != null ||
                    dmeBandwidth != null ||
                    dmeClass != null ||
                    dmePower != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                            Count = dmeCount != null ? dmeCount.ToString() : null,
                            Model = dmeModel != null ? dmeModel.ToString() : null,
                            Bandwidth = dmeBandwidth != null ? dmeBandwidth.ToString() : null,
                            Class = dmeClass != null ? dmeClass.ToString() : null,
                            Power = dmePower != null ? dmePower.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("DME")
                        });
                }

                //RA
                object raCount = excelReader[42];
                object raModel = excelReader[43];
                object raPower = excelReader[44];
                object raClass = excelReader[45];
                object raBandwidth = excelReader[46];
                if(raCount != null ||
                    raBandwidth != null ||
                    raClass != null ||
                    raPower != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                            Count = raCount != null ? raCount.ToString() : null,
                            Model = raModel != null ? raModel.ToString() : null,
                            Bandwidth = raBandwidth != null ? raBandwidth.ToString() : null,
                            Class = raClass != null ? raClass.ToString() : null,
                            Power = raPower != null ? raPower.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("RA")
                        });
                }

                //Otr
                object otr8Type = excelReader[47];
                object otr8Count = excelReader[48];
                object otr8Model = excelReader[49];
                object otr8Power = excelReader[50];
                object otr8Class = excelReader[51];
                object otr8Bandwidth = excelReader[52];
                if (otr8Type != null|
                    otr8Count != null ||
                    otr8Bandwidth != null ||
                    otr8Class != null ||
                    otr8Power != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                            Count = otr8Count != null ? otr8Count.ToString() : null,
                            Model = otr8Model != null ? otr8Model.ToString() : null,
                            Bandwidth = otr8Bandwidth != null ? otr8Bandwidth.ToString() : null,
                            Class = otr8Class != null ? otr8Class.ToString() : null,
                            Power = otr8Power != null ? otr8Power.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("Otr"),
                            OtherType = otr8Type != null ? otr8Type.ToString() : null,
                        });
                }

                object otr9Type = excelReader[53];
                object otr9Count = excelReader[54];
                object otr9Model = excelReader[55];
                object otr9Power = excelReader[56];
                object otr9Class = excelReader[57];
                object otr9Bandwidth = excelReader[58];
                if (otr9Type != null|
                    otr9Count != null ||
                    otr9Bandwidth != null ||
                    otr9Class != null ||
                    otr9Power != null)
                {
                    radio.Entries.Add(
                        new AircraftCertRadioEntryDO(){
                           Count = otr9Count != null ? otr9Count.ToString() : null,
                            Model = otr9Model != null ? otr9Model.ToString() : null,
                            Bandwidth = otr8Bandwidth != null ? otr9Bandwidth.ToString() : null,
                            Class = otr9Class != null ? otr9Class.ToString() : null,
                            Power = otr9Power != null ? otr9Power.ToString() : null,
                            Equipment = noms["aircraftRadioTypes"].ByCode("Otr"),
                            OtherType = otr9Type != null ? otr9Type.ToString() : null,
                        });
                }

                int lotId = regMarkToLotId[regMark];
                if (certRadios.ContainsKey(lotId))
                {
                    certRadios[lotId].Add(radio);
                }
                else
                {
                    certRadios.Add(lotId, new List<AircraftCertRadioDO>(){ radio });
                }
            
            }
            excelReader.Close();

            return certRadios;
        }
    }
}
