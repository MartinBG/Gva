using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.Api.WordTemplates
{
    public static class LicenceDictionary
    {
        private static Dictionary<string, string[]> licenceRole = new Dictionary<string, string[]>()
        {
            { "FCL/ATPA", new [] { "BG" } },
            { "FCL/ATPH", new [] { "BG" } },
            { "FCL/CPA", new [] { "BG" } },
            { "SP(A)", new [] { "3", "4" } },
            { "ATCL", new [] { "ENG", "47A", "48A", "50A", "51A", "52A", "53", "BG" } },
            { "SATCL", new [] { "BTT", "ENG", "RT1", "RT2", "BG" } },
            { "C/AL", new [] { "15", "4", "7" } },
            { "F/CL", new[] { "4", "6" , "752", "775", "997"} },
            { "CATML-SIMI", new[] { "6" , "997", "1014", "1015", "1016", "1054"} }
        };

        private static Dictionary<string, object> licencePrivilege = new Dictionary<string, object>()
        {
            {
                "coordinatorSimi1",
                new
                {
                    NAME_BG = "Притежателят на това свидетелство за правоспособност CATML с вписано разрешение SIMI към него може да:"
                }
            },
            {
                "coordinatorSimi1Alt",
                new
                {
                    NAME_TRANS = "The holder of this CATML, with registered SIMI endorsementis entitled to:"
                }
            },
            {
                "coordinatorSimi2",
                new
                {
                    NAME_BG = "1. обучава РП в курсове;"
                }
            },
            {
                "coordinatorSimi2Alt",
                new
                {
                    NAME_TRANS = "1. train in courses air traffic controllers;"
                }
            },
            {
                "coordinatorSimi3",
                new
                {
                    NAME_BG = "2. участва в провеждането на изпити на РП на тренажор;"
                }
            },
             {
                "coordinatorSimi3Alt",
                new
                {
                    NAME_TRANS = "2. to participle testing in the simulator the air traffic controllers;"
                }
            },
            {
                "coordinatorSimi4",
                new
                {
                    NAME_BG = "3. участва при проверки и оценява компетентността на РП на тренажор;"
                }
            },
              {
                "coordinatorSimi4Alt",
                new
                {
                    NAME_TRANS = "3. participle in checks and competence assessment of the air traffic controllers in the simulator;"
                }
            },
            {
                "coordinatorSimi5",
                new
                {
                    NAME_BG = "4. Провежда обучение в АУЦ на ученици РП, обучаеми РП и РП с временно прекратена валидност на свидетелство за РП, квалификационен клас и/или разрешение, включително и да им провежда изпити, проверки или да извършва оценяване.",
                }
            },
            {
                "coordinatorSimi5Alt",
                new
                {
                    NAME_TRANS = "4. carry out training of the air traffic controller-students in the Training Centre and trainee air traffic controllers and the air traffic controllers with temporarily discontinued validity of ATCL, rating and/or endorsement, including the administration of tests, checks or making assessments."
                }
            },
            {
                "ATSM1",
                new
                {
                    NAME_BG = "1.Притежателят на това свидетелство за правоспособност 1.ATSML е     упълномощен да изпълнява функции по техническо обслужване на средствата за управление на въздушното движение в съответствие с вписаните степени, квалификационни и подквалификационни класове.",
                    NAME_TRANS = "1.The holder is entitled to exercise the functions of the written rating(s) for which current endorsement(s)."
                }
            },
            {
                "ATSM2",
                new
                {
                    NAME_BG = "2.Притежателят на ATSML може да упражнява прават, давани от свидетелството и вписаните в него квалификационни и подквалификационни класове, при условие, че през последните 24 месеца има най-малко 6 месеца трудов стаж по съответните квалификационни и подквалификационн класове.",
                    NAME_TRANS = "2.The holder of the ATSML may exercise the privileges, granted by this licence and the written ratings and subratings, under the condition that during the past 24 months, the holder has exercised the same ratings and subratings for a period of 6 months of service at least."
                }
            },
            {
                "ATSM3",
                new
                {
                    NAME_BG = "3.Притежателят на това свидетелство се легитимира с документ за самоличност.",
                    NAME_TRANS = "3.A legal identification document has to be carried for the purpose of identification of the licence holder."
                }
            },
            {
                "medCert",
                new
                {
                    NAME_BG = "Правата на свидетелството се упражняват единствено ако притежателят има валидно медицинско свидетелство за исканото право.",
                    NAME_TRANS = "The privileges of the licence shall be exercised only if the holder has a valid medical certificate for the required privilege."
                }
            },
            {
                "medCert2",
                new
                {
                    NAME_BG = "Притежателят на това свидетелство трябва да има валидно свидетелство за медицинска годност от клас, съответстващ на упражняваното право.",
                    NAME_TRANS = "The privileges of this licence shall be exercised only if the holder has a valid medical certificate for the required privilege."
                }
            },
            {
                "medCertClass1or2",
                new
                {
                    NAME_BG = "Притежателят на това удостоверение трябва да има валидно свидетелство за медицинска годност първи или втори клас.",
                    NAME_TRANS = "The privileges of the licence should be exercised only if the holder has a valid medical certificate for Class 1 or Class 2."
                }
            },
            {
                "medCertClass3",
                new
                {
                    NAME_BG = "Към това свидетелство се включва валидно свидетелство за медицинска годност клас 3.",
                    NAME_TRANS = "This licence is subject to the inclusion of a valid medical certificate class 3."
                }
            },
            {
                "photo",
                new
                {
                    NAME_BG = "Трябва да се носи документ със снимка с цел идентифициране на притежателя на свидетелството за правоспособност.",
                    NAME_TRANS = "A document containing a photo shall be carried for the purposes of identification of the licence holder."
                }
            },
            {
                "dateValid",
                new
                {
                    NAME_BG = "Това удостоверение е валидно до: {0}",
                    NAME_TRANS = "This licence is to be re-issued not later than: {0}"
                }
            },
            {
                "instr",
                new
                {
                    NAME_BG = "Притежателят на това удостоверение не трябва да лети сам, освен ако не е упълномощен от инструктор.",
                    NAME_TRANS = "The holder of this licence shall not fly solo unless authorised by a Flight Instructor"
                }
            },
            {
                "idDoc",
                new
                {
                    NAME_BG = "Притежателят на това удостоверение се легитимира с документ за самоличност.",
                    NAME_TRANS = "A legal identification document has to be carried for the purpose of identification of the licence holder."
                }
            },
            {
                "steward",
                new
                {
                    NAME_BG = "Притежателят на това свидетелство има право да изпълнява полети като стюард/еса на посочените в раздел XII типове ВС.",
                    NAME_TRANS = "The holder of this licence is authorised to act as cabin attendant of aeroplane types entered in section XII."
                }
            },
            {
                "ATCLratings",
                new
                {
                    NAME_BG = "Притежателят на това свидетелство за правоспособност ATCL е упълномощен да изпълнява функции в органите за ОВД за следните квалификационни класове, за които притежава валидно(и) разрешение(я), както следва:",
                    NAME_TRANS = "The holder is entitled to exercise the functions of the following rating(s) at the air traffic service unit(s) for which current endorsement(s) is/are held as follows:"
                }
            },
            {
                "ATCLstudent",
                new
                {
                    NAME_BG = "Притежателят на това свидетелство е упълномощен да изпълнява функции на ученик-ръководител на полети за придобиване на свидетелство ATCL за следните квалификационни класове.",
                    NAME_TRANS = "The holder of this is entitled to exercise the functions of a student air controller for the purpose of becoming qualified for the grant of an air traffic controller licence in the following rating as dated:"
                }
            }
        };

        private static Dictionary<string, object> licenceAbbreviation = new Dictionary<string, object>()
        {
            {
                "Aeroplane",
                new
                {
                    TERM = "A",
                    DESCR_TRANS = "Aeroplane",
                    DESCR = "Самолет"
                }
            },
            {
                "ATPL",
                new
                {
                    TERM = "ATPL",
                    DESCR_TRANS = "Airline Transport Pilot Licence",
                    DESCR = "Свид. за правоспособност на транспортен пилот"
                }
            },
            {
                "ATP",
                new
                {
                    TERM = "ATP",
                    DESCR_TRANS = "Airline transport pilot licence",
                    DESCR = "Свидетелство за правоспособност на транспортен пилот"
                }
            },
            {
                "Co-pilot",
                new
                {
                    TERM = "COP",
                    DESCR_TRANS = "Co-pilot",
                    DESCR = "Втори пилот"
                }
            },
            {
                "CPL",
                new
                {
                    TERM = "CPL",
                    DESCR_TRANS = "Commercial pilot licence",
                    DESCR = "Свидетелство за правоспособност на професионален пилот"
                }
            },
            {
                "CRI",
                new
                {
                    TERM = "CRI",
                    DESCR_TRANS = "Class rating instructor",
                    DESCR = "Инструктор за еднопилотен многодвигателен клас"
                }
            },
            {
                "flightInstr",
                new
                {
                    TERM = "FI",
                    DESCR_TRANS = "Flight Instructor",
                    DESCR = "Летателен инструктор"
                }
            },
            {
                "instrumentRating",
                new
                {
                    TERM = "IR",
                    DESCR_TRANS = "Instrument rating",
                    DESCR = "Полети по прибори"
                }
            },
            {
                "IRI",
                new
                {
                    TERM = "IRI",
                    DESCR_TRANS = "Instrument Rating Instructor",
                    DESCR = "Инструктор за полети по прибори"
                }
            },
            {
                "MEP",
                new
                {
                    TERM = "MEP",
                    DESCR_TRANS = "Multi-engine Piston",
                    DESCR = "Многодвигателни бутални"
                }
            },
            {
                "PIC",
                new
                {
                    TERM = "PIC",
                    DESCR_TRANS = "Pilot-In-Command",
                    DESCR = "Командир"
                }
            },
            {
                "PPL",
                new
                {
                    TERM = "PPL",
                    DESCR_TRANS = "Private Pilot Licence",
                    DESCR = "Private Pilot Licence"
                }
            },
            {
                "R/T",
                new
                {
                    TERM = "R/T",
                    DESCR_TRANS = "Radiotelephony",
                    DESCR = "Радиотелефония"
                }
            },
            {
                "SEP",
                new
                {
                    TERM = "SEP",
                    DESCR_TRANS = "Single engine piston",
                    DESCR = "Еднодвигателни бутални"
                }
            },
            {
                "TMG",
                new
                {
                    TERM = "TMG",
                    DESCR_TRANS = "Touring Motor Glider",
                    DESCR = "Туристически мотопланери"
                }
            },
            {
                "TRI",
                new
                {
                    TERM = "TRI",
                    DESCR_TRANS = "Type Rating Instructor",
                    DESCR = "Инструктор за многопилотен тип"
                }
            },
            {
                "ASM",
                new
                {
                    TERM = "ASM",
                    DESCR_TRANS = "Air Space Management",
                    DESCR = "Планиране и разпределение на въздушното пространство"
                }
            },
            {
                "SP",
                new
                {
                    TERM = "SP",
                    DESCR_TRANS = "Student Pilot Certificate - Aeroplane",
                    DESCR = "Удостоверение за летателно обучение на полет на въздухоплавателно средство"
                }
            },
            {
                "INS-C/AL",
                new
                {
                    TERM = "INS-C/AL",
                    DESCR_TRANS = "Instructor cabin attendant",
                    DESCR = "Стюард/ еса - инструктор"
                }
            },
            {
                "FE",
                new
                {
                    TERM = "FE",
                    DESCR_TRANS = "Flight Examiner",
                    DESCR = "Проверяващ"
                }
            },
            {
                "C/AL",
                new
                {
                    TERM = "C/AL",
                    DESCR_TRANS = "Cabin attendant",
                    DESCR = "Стюард / еса"
                }
            },
            {
                "Types",
                new
                {
                    TERM = "Types",
                    DESCR_TRANS = "Aircraft type",
                    DESCR = "Тип ВС"
                }
            },
            {
                "SEN-C/AL",
                new
                {
                    TERM = "SEN-C/AL",
                    DESCR_TRANS = "Senior cabin attendant",
                    DESCR = "Старша стюард/еса"
                }
            },
            {
                "AFIS",
                new
                {
                    TERM = "AFIS",
                    DESCR_TRANS = "Aerodrome Flight Information Service",
                    DESCR = "Летищно полетно-информационно обслужване"
                }
            },
            {
                "SAR",
                new
                {
                    TERM = "SAR",
                    DESCR_TRANS = "Search and Rescue",
                    DESCR = "Търсене и спасяване"
                }
            },
            {
                "FIS",
                new
                {
                    TERM = "FIS",
                    DESCR_TRANS = "Flight Information Service",
                    DESCR = "Полетно информационно обслужване"
                }
            },
            {
                "FDA",
                new
                {
                    TERM = "FDA",
                    DESCR_TRANS = "Flight Data Assistant",
                    DESCR = "Асистент координатор на полетите"
                }
            },
            {
                "ATFM",
                new
                {
                    TERM = "ATFM",
                    DESCR_TRANS = "Air Traffic Flow Management",
                    DESCR = "Управление на потоците въздушно движение"
                }
            },
            {
                "OJTI",
                new
                {
                    TERM = "OJTI",
                    DESCR_TRANS = "On the Job Training Instructor",
                    DESCR = "Инструктор за обучение на работно място"
                }
            },
            {
                "SIMI",
                new
                {
                    TERM = "SIMI",
                    DESCR_TRANS = "Simulator Instructor",
                    DESCR = "Инструктор на тренажор"
                }
            },
            {
                "GMC",
                new
                {
                    TERM = "GMC",
                    DESCR_TRANS = "Ground Movement Control",
                    DESCR = "КВД по маневрената площ на летището"
                }
            },
            {
                "AIR",
                new
                {
                    TERM = "AIR",
                    DESCR_TRANS = "Air Control",
                    DESCR = "Контрол на въздушното движение"
                }
            },
            {
                "TWR",
                new
                {
                    TERM = "TWR",
                    DESCR_TRANS = "Tower Control",
                    DESCR = "КВД в контролираната зона за \"излитане и кацане\""
                }
            },
            {
                "GMS",
                new
                {
                    TERM = "GMS",
                    DESCR_TRANS = "Ground Movement Surveillance",
                    DESCR = "КВД по маневр. площ на л-щето чрез средствата за обзор"
                }
            },
            {
                "ADS",
                new
                {
                    TERM = "ADS",
                    DESCR_TRANS = "Automatic Dependent Surveillance",
                    DESCR = "КВД чрез автоматичен зависим обзор"
                }
            },
            {
                "RAD",
                new
                {
                    TERM = "RAD",
                    DESCR_TRANS = "Radar",
                    DESCR = "КВД чрез радар"
                }
            },
            {
                "SRA",
                new
                {
                    TERM = "SRA",
                    DESCR_TRANS = "Surveillance Radar Approach",
                    DESCR = "КВД чрез обзорен радар за подход"
                }
            },
            {
                "PAR",
                new
                {
                    TERM = "PAR",
                    DESCR_TRANS = "Precision Approach Radar",
                    DESCR = "КВД чрез прецизен радар за подход"
                }
            },
            {
                "TCL",
                new
                {
                    TERM = "TCL",
                    DESCR_TRANS = "Terminal control",
                    DESCR = "КВД за сепариране на потоците долитащи и отлитащи ВС"
                }
            },
            {
                "ACS",
                new
                {
                    TERM = "ACS",
                    DESCR_TRANS = "Area Control Surveillance",
                    DESCR = "ОВД в контролирания район чрез средства за обзор"
                }
            },
            {
                "ADI",
                new
                {
                    TERM = "ADI",
                    DESCR_TRANS = "Aerodrome Control Instrument",
                    DESCR = "Приборно ОВД на летището"
                }
            }
        };

        public static Dictionary<string, string[]> LicenceRole
        {
            get
            {
                return licenceRole;
            }
        }

        public static Dictionary<string, object> LicencePrivilege
        {
            get
            {
                return licencePrivilege;
            }
        }

        public static Dictionary<string, object> LicenceAbbreviation
        {
            get
            {
                return licenceAbbreviation;
            }
        }
    }
}
