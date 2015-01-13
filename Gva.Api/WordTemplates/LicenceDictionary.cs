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
            { "FCL/PPA", new [] { "ENG" } },
            { "FCL/CPH", new [] { "ENG", "BG" } },
            { "FCL/CPA", new [] { "BG" } },
            { "SP(A)", new [] { "3", "4" } },
            { "ATCL", new [] { "ENG", "47A", "48A", "50A", "51A", "52A", "53", "BG" } },
            { "SATCL", new [] { "BTT", "ENG", "RT1", "RT2", "BG" } },
            { "C/AL", new [] { "15", "4", "7" } },
            { "F/CL", new[] { "4", "6" , "15", "25", "ENG"} },
            { "ATPL(A)", new[] { "1", "4", "BG", "5", "ENG", "6", "7" } },
            { "ATPL(H)", new[] { "ENG", "4", "1" } },
            { "CPL(A)", new[] { "ENG", "4", "5", "7", "6", "1" } },
            { "CPL(H)", new[] { "ENG", "1", "08", "4" } },
            { "FDL", new[] { "6", "25", "4" } },
            { "FEL", new[] { "5", "1", "7", "ENG", "4", "6" } },
            { "FOL", new[] { "ENG" } },
            { "PL(FB)", new[] { "6", "7", "ENG" } },
            { "PL(G)", new[] { "ENG" } },
            { "PPL(A)", new[] { "6", "ENG", "1", "5", "4" } },
            { "PPL(H)", new[] { "ENG", "1", "4" } },
            { "PPL(SA)", new[] { "ENG", "5", "4", "1", "6" } },
            { "CATML-SIMI", new[] { "6" , "54", "47A", "48A", "49A", "ENG"} },
            { "CATML", new[] { "54", "47A", "48A", "49A", "6" } },
            { "FDA", new[] { "54", "47A", "48A", "49A", "6" } },
            { "PPH", new [] { "ENG" } },
            { "CPH", new [] { "ENG" } },
            { "ATPA", new [] { "1", "4", "5", "6", "7", "ENG" } },
            { "ATPH", new [] { "ENG" } },
            { "CPA", new [] { "ENG", "1", "4", "5", "6" } },
            { "PPA", new [] { "ENG" } }
        };

        private static Dictionary<string, object> licencePrivilege = new Dictionary<string, object>()
        {
            {
                "dateValid",
                new
                {
                    NO = 100,
                    NAME_BG = "1. Това удостоверение е валидно до: {0}",
                    NAME_TRANS = "1. This licence is to be re-issued not later than: {0}"
                }
            },
            {
                "dateValid2",
                new
                {
                    NO = 100,
                    NAME_BG = "Това свидетелство трябва да бъде преиздадено не по-късно от: {0}",
                    NAME_TRANS = "This licence is to be re-issued not later than: {0}"
                }
            },
            {
                "ATCLratings",
                new
                {
                    NO = 100,
                    NAME_BG = "1. Притежателят на това свидетелство за правоспособност ATCL е упълномощен да изпълнява функции в органите за ОВД за следните квалификационни класове, за които притежава валидно(и) разрешение(я), както следва:",
                    NAME_TRANS = "1. The holder is entitled to exercise the functions of the following rating(s) at the air traffic service unit(s) for which current endorsement(s) is/are held as follows:"
                }
            },
            {
                "medCertClass3",
                new
                {
                    NO = 100,
                    NAME_BG = "1. Към това свидетелство се включва валидно свидетелство за медицинска годност клас 3.",
                    NAME_TRANS = "1. This licence is subject to the inclusion of a valid medical certificate class 3."
                }
            },
            {
                "medCert",
                new
                {
                    NO = 100,
                    NAME_BG = "1. Правата на свидетелството се упражняват единствено ако притежателят има валидно медицинско свидетелство за исканото право.",
                    NAME_TRANS = "1. The privileges of the licence shall be exercised only if the holder has a valid medical certificate for the required privilege."
                }
            },
            {
                "ATSM1",
                new
                {
                    NO = 100,
                    NAME_BG = "1. Притежателят на това свидетелство за правоспособност ATSML е упълномощен да изпълнява функции по техническо обслужване на средствата за управление на въздушното движение в съответствие с вписаните степени, квалификационни и подквалификационни класове.",
                    NAME_TRANS = "1. The holder is entitled to exercise the functions of the written rating(s) for which current endorsement(s)."
                }
            },
            {
                "CATML",
                new
                {
                    NO = 100,
                    NAME_BG = "1. Притежателят на това свидетелство за правоспособност CATML с вписано към него разрешение ASM  и/или ATFM, и/или SAR, и/или FIS, и/или AFIS може да упражнява правата по координация и взаимодействие по УВД, а така също и предоставените му специфични права за длъжността, която заема.",
                    NAME_TRANS = "1. The holder of this CATML which includes the endorsements:ASM, and/or ATFM, and/or SAR, and/or FIs, and/or AFIS is entitled to exercise the rights of ATM coordination and interaction as well as the specific rights related to the position he/she holds."
                }
            },
            {
                "coordinatorSimi1",
                new
                {
                    NO = 100,
                    NAME_BG = "Притежателят на това свидетелство за правоспособност CATML с вписано разрешение SIMI към него може да:",
                    NAME_TRANS = "The holder of this CATML, with registered SIMI endorsementis entitled to:"
                }
            },
            {
                "coordinatorSimi2",
                new
                {
                    NO = 101,
                    NAME_BG = "1. обучава РП в курсове;",
                    NAME_TRANS = "1. train in courses air traffic controllers;"
                }
            },
            {
                "coordinatorSimi3",
                new
                {
                    NO = 102,
                    NAME_BG = "2. участва в провеждането на изпити на РП на тренажор;",
                    NAME_TRANS = "2. to participle testing in the simulator the air traffic controllers;"
                }
            },
            {
                "coordinatorSimi4",
                new
                {
                    NO = 103,
                    NAME_BG = "3. участва при проверки и оценява компетентността на РП на тренажор;",
                    NAME_TRANS = "3. participle in checks and competence assessment of the air traffic controllers in the simulator;"
                }
            },
            {
                "coordinatorSimi5",
                new
                {
                    NO = 104,
                    NAME_BG = "4. Провежда обучение в АУЦ на ученици РП, обучаеми РП и РП с временно прекратена валидност на свидетелство за РП, квалификационен клас и/или разрешение, включително и да им провежда изпити, проверки или да извършва оценяване.",
                    NAME_TRANS = "4. carry out training of the air traffic controller-students in the Training Centre and trainee air traffic controllers and the air traffic controllers with temporarily discontinued validity of ATCL, rating and/or endorsement, including the administration of tests, checks or making assessments."
                }
            },
            {
                "idDoc3",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство се легитимира с документ за самоличност.",
                    NAME_TRANS = "2. A legal identification document has to be carried for the purpose of identification of the licence holder."

                }
            },
            {
                "ATSM2",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на ATSML може да упражнява прават, давани от свидетелството и вписаните в него квалификационни и подквалификационни класове, при условие, че през последните 24 месеца има най-малко 6 месеца трудов стаж по съответните квалификационни и подквалификационн класове.",
                    NAME_TRANS = "2. The holder of the ATSML may exercise the privileges, granted by this licence and the written ratings and subratings, under the condition that during the past 24 months, the holder has exercised the same ratings and subratings for a period of 6 months of service at least."
                }
            },
            {
                "photo",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Трябва да се носи документ със снимка с цел идентифициране на притежателя на свидетелството за правоспособност.",
                    NAME_TRANS = "2. A document containing a photo shall be carried for the purposes of identification of the licence holder."
                }
            },
            {
                "validWithMedCert",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Правата на свидетелството трябва да се упражняват само ако притежателят има валиден медицински сертификат за съответните права.",
                    NAME_TRANS = "2. The privileges of the licence shall be exercised only if the holder has a valid medical certificate for required privileges."
                }
            },
            {
                "instr",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това удостоверение не трябва да лети сам, освен ако не е упълномощен от инструктор.",
                    NAME_TRANS = "2. The holder of this licence shall not fly solo unless authorised by a Flight Instructor"
                }
            },
            {
                "ATCLstudent",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство е упълномощен да изпълнява функции на ученик-ръководител на полети за придобиване на свидетелство ATCL за следните квалификационни класове.",
                    NAME_TRANS = "2. The holder of this is entitled to exercise the functions of a student air controller for the purpose of becoming qualified for the grant of an air traffic controller licence in the following rating as dated:"
                }
            },
            {
                "steward",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство има право да изпълнява полети като стюард/еса на посочените в раздел XII типове ВС.",
                    NAME_TRANS = "2. The holder of this licence is authorised to act as cabin attendant of aeroplane types entered in section XII."
                }
            },
            {
                "12typeVS",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство за правоспособност има право да изпълнява полети като борден съпроводител на пътници на посочените в раздел XII типове ВС.",
                    NAME_TRANS = "2. The holder of this licence shall be exercised only if the holder has a valid medical certificate for the required privilege only."
                }
            },
            {
                "ATPL(A)",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на свидетелство за правоспособност ATPL(A) има право да:",
                    NAME_TRANS = "2. The holder of ATPL(A) is entitled to:"
                }
            },
            {
                "ATPL(H)",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на свидетество за правоспособност ATPL(H) има право да:",
                    NAME_TRANS = "2. The holder of the ATPL(H) is entitled to:"
                }
            },
            {
                "CPL(A)",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство има право да:",
                    NAME_TRANS = "2. The holder of the CPL(A) is entitled to:"
                }
            },
            {
                "CPL(H)",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство има право да:",
                    NAME_TRANS = "2. The holder of the CPL(H) is entitled to: "
                }
            },
            {
                "FDL",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на свидетелство за правоспособност FDL може да изпълнява функциите на полетен диспечер в АО за типовете ВС, за които е прибодил квалификационен клас.",
                    NAME_TRANS = "2. The holder of the FАL may be perform the functions of an airline Flight Dispetcher operating the type(s) of aircraft, for which he has a type rating course attained. "
                }
            },
            {
                "F/EL",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на свидетелство за правоспособност F/EL може да изпълнява функциите на борден инжене в състава на екипажа на типовете ВС, за които е прибодил квалификационен клас.",
                    NAME_TRANS = "2. The holder of the F/EL may be perform the functions of a Flight Engineer as a flight crew member, operating the type(s) of aircraft, for which he has a type rating course attained."
                }
            },
            {
                "F/OL",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на свидетелство за правоспособност F/OL може да изпълнява функциите на борден оператор в състава на екипажа на типовете ВС, за които е придобил квалификационен клас.",
                    NAME_TRANS = "2. The holder of the F/OL may perform the functions of a Flight Operator as a flight crew member, operating the type(s) of aircraft, for which he has a type rating course attained."
                }
            },
            {
                "PL(FB)",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство има право да изпълнява полети с балони по правилата за взиуални полети през деня.",
                    NAME_TRANS = "2. The holder of the PL(FB) is entitled to operate VFR baloon flights during only. "
                }
            },
            {
                "PL(G)",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство има право да:",
                    NAME_TRANS = "2. The holder of the PL(G) is entitled to:"
                }
            },
            {
                "PPL(A)",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство има право да:",
                    NAME_TRANS = "2. The holder of the PPL(A) is entitled to:"
                }
            },
            {
                "PPL(H)",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство има право да:",
                    NAME_TRANS = "2. The holder of the PPL(H) is entitled to:"
                }
            },
            {
                "PPL(SA)",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство има право да:",
                    NAME_TRANS = "2. The holder of hte PPL(SA) is entitled to:"
                }
            },
            {
                "PPL(A)/CPL(A)/IR(A)",
                new
                {
                    NO = 210,
                    NAME_BG = "2а. упражнява правата, давани от PPL(A), CPL(A) и квалификационен клас IR(A);",
                    NAME_TRANS = "2a. exercise the rights granted by hte PPL(A), CPL(A) and IR(A);"
                }
            },
            {
                "PPL(H)/CPL(H)/IR(H)",
                new
                {
                    NO = 210,
                    NAME_BG = "2а. упражнява правата, давани от PPL(H), CPL(H) и квалификационен клас IR(H);",
                    NAME_TRANS = "2a. exercise the rights granted by the PPL(H), CPL(H) and IR(H);"
                }
            },
            {
                "PPL(A)2",
                new
                {
                    NO = 210,
                    NAME_BG = "2а. упражнява правата, на притежател на PPL(A),",
                    NAME_TRANS = "2a. exercise the privileges of the holder of a PPL(A);"
                }
            },
            {
                "PPL(H)2",
                new
                {
                    NO = 210,
                    NAME_BG = "2а. упражнява правата, на притежател на PPL(H);",
                    NAME_TRANS = "2a. exercise all the privileges of the holder of a PPL(H);"
                }
            },
            {
                "VFR",
                new
                {
                    NO = 210,
                    NAME_BG = "2а. изпълнява без възнаграждение нетърговски полети на малки ВС по правилата за визуални полети през деня и при спазване на следните ограничения:",
                    NAME_TRANS = "2a. operate with no renumeration non-commercial flights on small aircraft according VFR, during day-light and comply with the following restrictions:"
                }
            },
            {
                "VFRXII",
                new
                {
                    NO = 210,
                    NAME_BG = "2а. изпълнява без възнаграждение като КВС или втори пилот нетърговски полети по правилата за визуални полети през деня за самолетите посочени в раздел XII.",
                    NAME_TRANS = "2a. act, but not for renumaration non-commercial flights as a pilot-in-command or a co-pilot on aircraft according VFR, during day-light of aeroplane types entered in sextion XII."
                }
            },
            {
                "noPoW",
                new
                {
                    NO = 211,
                    NAME_BG = "- не може да извършва полети над населени места и водни басейни.",
                    NAME_TRANS = "- does not have the right to operate flights over populated areas and aquatic territories."
                }
            },
            {
                "PIC/CO-com",
                new
                {
                    NO = 220,
                    NAME_BG = "2б. действа като КВС или втори пилот на самолети при полети за търговски въздушни превози;",
                    NAME_TRANS = "2b. act as a pilot-in-comand or as a co-pilot of aircraft, operating commercial air transport flights;"
                }
            },
            {
                "PIC/CO",
                new
                {
                    NO = 220,
                    NAME_BG = "2б. действа като КВС или втори пилот на самолети, при изпълнение на полети, различни от търговски въздушен превоз;",
                    NAME_TRANS = "2b. to act as a pilot-in-command or co-piloti in any aeroplane engaged in operations other than commercial air transportation;"
                }
            },
            {
                "PIC",
                new
                {
                    NO = 230,
                    NAME_BG = "2в. действа като КВС на еднопилотни самолети, при изпълнение на полети за търговски въздушен превоз;",
                    NAME_TRANS = "2c. to act as a pilot-in-command in commercial air transportation in aeroplanes certificated for single-pilot operation;"
                }
            },
            {
                "PIC-heli",
                new
                {
                    NO = 230,
                    NAME_BG = "2в. действа като КВС на еднопилотни вертолети, при изпълнение на полети за търговски въздушен превоз;",
                    NAME_TRANS = "2c. to act as pilot-in-command in commercial air transportation in helicopters certificated for single-pilot operation;"
                }
            },
            {
                "CO",
                new
                {
                    NO = 240,
                    NAME_BG = "2г. действа като втори пилот на многопилотни самолети при полети за търговски въздушен превоз;",
                    NAME_TRANS = "2d. to act as a co-pilot in commercial air transportation."
                }
            },
            {
                "CO-heli",
                new
                {
                    NO = 240,
                    NAME_BG = "2г. действа като втори пилот на многопилотни вертолети при полети за търговски въздушен превоз. ",
                    NAME_TRANS = "2d. to act as co-pilot in commercial air transportation."
                }
            },
            {
                "medCert2",
                new
                {
                    NO = 300,
                    NAME_BG = "3. Притежателят на това свидетелство трябва да има валидно свидетелство за медицинска годност от клас, съответстващ на упражняваното право.",
                    NAME_TRANS = "3. The privileges of this licence shall be exercised only if the holder has a valid medical certificate for the required privilege."
                }
            },
            {
                "PL(FB)-noPoW",
                new
                {
                    NO = 300,
                    NAME_BG = "3. Притежателят на това свидетелство няма право да извършва полети над населени места и водни басейни.",
                    NAME_TRANS = "3. The holder of this PL(FB) does not have the right to operate flights over populated areas and over water."
                }
            },
            {
                "medCertClass2",
                new
                {
                    NO = 300,
                    NAME_BG = "3. Притежателят на това свидетелство трябва да има валидно свидетелство за медицинска годност най-малко втори клас.",
                    NAME_TRANS = "3. The privileges of hte licence shall be exercised only if the holder has a current Class 2 Medical Certificate."
                }
            },
            {
                "medCertClass1or2",
                new
                {
                    NO = 300,
                    NAME_BG = "Притежателят на това удостоверение трябва да има валидно свидетелство за медицинска годност първи или втори клас.",
                    NAME_TRANS = "The privileges of the licence should be exercised only if the holder has a valid medical certificate for Class 1 or Class 2."
                }
            },
            {
                "requiresLegalID",
                new
                {
                    NO = 300,
                    NAME_BG = "3. Притежателят на това свидетелство се легитимира с документ за самоличност.",
                    NAME_TRANS = "3. A legal identification document has to be carried for the purpose of identification of the licence holder."
                }
            },
            {
                "idDoc",
                new
                {
                    NO = 400,
                    NAME_BG = "4. Притежателят на това удостоверение се легитимира с документ за самоличност.",
                    NAME_TRANS = "4. A legal identification document has to be carried for the purpose of identification of the licence holder."
                }
            },
            {
                "medCert3",
                new
                {
                    NO = 400,
                    NAME_BG = "4. Притежателят на това свидетелство трябва да има валидно свидетелство за медицинска годност от клас, съответстващ на упражняваното право.",
                    NAME_TRANS = "4. The privileges of this licence shall be exercised only if the holder has a valid medical certificate for the required privilege."
                }
            },
            {
                "idDoc2",
                new
                {
                    NO = 500,
                    NAME_BG = "5. Притежателят на това свидетелство се легитимира с документ за самоличност.",
                    NAME_TRANS = "5. A legal identification document has to be carried for the purpose of identification of the licence holder."
                }
            },
            {
                "FDAL",
                new
                {
                    NO = 100,
                    NAME_BG = "1.Притежателят на това свидетелство за правоспособност FDAL може да извършва дейности по координация и взаимодействие при УВД в органите за ОВД за които притежава валидно(и) разрешение(я), както следва:",
                    NAME_TRANS = "1. The holder of this FDAL is entitled to exercise coordination and coactivities at ATM in the air traffic service unit(s) for which current endorsement(s) is/ are held as follows:"
                }
            },
            {
                "idDoc4",
                new
                {
                    NO = 200,
                    NAME_BG = "2. Притежателят на това свидетелство се легитимира с документ за самоличност.",
                    NAME_TRANS = "2. A legal identification document has to be carried for the purpose of identification of the licence holder"
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
