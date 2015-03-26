using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AircraftTypeQualificationClassNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature A300_310 = new BaseNomenclature("A 300/310", "A 300/310", "");
        public static readonly BaseNomenclature A319_320 = new BaseNomenclature("A 319 / 320", "A 319 / 320", "");
        public static readonly BaseNomenclature A320 = new BaseNomenclature("A 320", "A 320", "");
        public static readonly BaseNomenclature A320_IR = new BaseNomenclature("A 320/ IR", "A 320/ IR", "");
        public static readonly BaseNomenclature A330 = new BaseNomenclature("A 330", "A 330", "");
        public static readonly BaseNomenclature A330_IR = new BaseNomenclature("A 330/IR", "A 330/IR", "");
        public static readonly BaseNomenclature A330_340 = new BaseNomenclature("A 330-340", "A 330-340", "");
        public static readonly BaseNomenclature A340 = new BaseNomenclature("A 340", "A 340", "");
        public static readonly BaseNomenclature A109 = new BaseNomenclature("A109", "A109", "");
        public static readonly BaseNomenclature AN12 = new BaseNomenclature("AN 12", "AN 12", "");
        public static readonly BaseNomenclature AN24 = new BaseNomenclature("AN 24", "AN 24", "");
        public static readonly BaseNomenclature AN26 = new BaseNomenclature("AN 26", "AN 26", "");
        public static readonly BaseNomenclature AS365_N1 = new BaseNomenclature("AS 365 N1", "AS 365 N1", "");
        public static readonly BaseNomenclature ATR_42_72 = new BaseNomenclature("ATR 42/72", "ATR 42/72", "");
        public static readonly BaseNomenclature ATR42 = new BaseNomenclature("ATR42", "ATR42", "");
        public static readonly BaseNomenclature AVRORJ_BAe146 = new BaseNomenclature("AVRORJ/BAe146", "AVRORJ/BAe146", "");
        public static readonly BaseNomenclature AVRORJ_BAe146_IR = new BaseNomenclature("AVRORJ/BAe146/IR", "AVRORJ/BAe146/IR", "");
        public static readonly BaseNomenclature AW109 = new BaseNomenclature("AW 109", "AW 109", "");
        public static readonly BaseNomenclature B737_300_500 = new BaseNomenclature("B 737 300-500", "B 737 300-500", "");
        public static readonly BaseNomenclature B737_300_900 = new BaseNomenclature("B 737 300-900", "B 737 300-900", "");
        public static readonly BaseNomenclature B737_600_800 = new BaseNomenclature("B 737 600-800", "B 737 600-800", "");
        public static readonly BaseNomenclature B747_400 = new BaseNomenclature("B 747-400", "B 747-400", "");
        public static readonly BaseNomenclature B777 = new BaseNomenclature("B 777", "B 777", "");
        public static readonly BaseNomenclature B737 = new BaseNomenclature("B737", "Boeing 737", "");
        public static readonly BaseNomenclature B757_767 = new BaseNomenclature("B757/767", "B757/767", "");
        public static readonly BaseNomenclature BAe146 = new BaseNomenclature("BAe 146", "BAe 146", "");
        public static readonly BaseNomenclature BAeATPJetstream_61 = new BaseNomenclature("BAe/ATP/Jetstream 61", "BAe/ATP/Jetstream 61", "");
        public static readonly BaseNomenclature BE20 = new BaseNomenclature("BE 20", "BE 20", "");
        public static readonly BaseNomenclature BE90 = new BaseNomenclature("BE 90", "BE 90", "");
        public static readonly BaseNomenclature BE90_99_100_200 = new BaseNomenclature("BE 90/99/100/200", "BE 90/99/100/200", "");
        public static readonly BaseNomenclature Bell430 = new BaseNomenclature("Bell 430", "Bell 430", "");
        public static readonly BaseNomenclature Blanick = new BaseNomenclature("Blanick", "Blanick", "");
        public static readonly BaseNomenclature BO105_CBS = new BaseNomenclature("BO 105 CBS", "BO 105 CBS", "");
        public static readonly BaseNomenclature C500_550_560 = new BaseNomenclature("C 500/550/560", "C 500/550/560", "");
        public static readonly BaseNomenclature C500_550_560_IR = new BaseNomenclature("C 500/550/560/IR", "C 500/550/560/IR", "");
        public static readonly BaseNomenclature C525 = new BaseNomenclature("C 525", "C 525", "");
        public static readonly BaseNomenclature C550 = new BaseNomenclature("C 550", "C 550", "");
        public static readonly BaseNomenclature Cessna100 = new BaseNomenclature("Cessna 100", "Cessna 100", "");
        public static readonly BaseNomenclature Cessna150 = new BaseNomenclature("Cessna 150", "Cessna 150", "");
        public static readonly BaseNomenclature Cessna150_172 = new BaseNomenclature("Cessna 150/172", "Cessna 150/172", "");
        public static readonly BaseNomenclature Cessna152 = new BaseNomenclature("Cessna 152", "Cessna 152", "");
        public static readonly BaseNomenclature Cessna152Lycoming = new BaseNomenclature("Cessna 152(Lycoming)", "Cessna 152(Lycoming)", "");
        public static readonly BaseNomenclature Cessna152_172 = new BaseNomenclature("Cessna 152/172", "Cessna 152/172", "");
        public static readonly BaseNomenclature Cessna172 = new BaseNomenclature("Cessna 172", "Cessna 172", "");
        public static readonly BaseNomenclature Cessna208Land = new BaseNomenclature("CESSNA 208 LAND", "CESSNA 208 LAND", "");
        public static readonly BaseNomenclature Cessna208Sea = new BaseNomenclature("Cessna 208 SEA", "Cessna 208 SEA", "");
        public static readonly BaseNomenclature Cessna510 = new BaseNomenclature("Cessna 510", "Cessna 510", "");
        public static readonly BaseNomenclature Cessna152_172_182 = new BaseNomenclature("Cessna-152/172/182", "Cessna-152/172/182", "");
        public static readonly BaseNomenclature CessnaSET = new BaseNomenclature("CessnaSET", "CessnaSET", "");
        public static readonly BaseNomenclature CL60 = new BaseNomenclature("CL 60", "CL 60", "");
        public static readonly BaseNomenclature CL600 = new BaseNomenclature("CL 600", "CL 600", "");
        public static readonly BaseNomenclature CL604 = new BaseNomenclature("CL 604", "CL 605", "");
        public static readonly BaseNomenclature CL605 = new BaseNomenclature("CL 605", "CL 605", "");
        public static readonly BaseNomenclature CL600_601 = new BaseNomenclature("CL600/601", "CL600/601", "");
        public static readonly BaseNomenclature CL604_605 = new BaseNomenclature("CL604/605", "CL604/605", "");
        public static readonly BaseNomenclature CL604_605_IR = new BaseNomenclature("CL604/605/ IR", "CL604/605/ IR", "");
        public static readonly BaseNomenclature CRI_A = new BaseNomenclature("CRI(A)", "CRI(A)", "");
        public static readonly BaseNomenclature CRI_MEA = new BaseNomenclature("CRI(MEA)", "CRI(MEA)", "");
        public static readonly BaseNomenclature CRI_SEA = new BaseNomenclature("CRI(SEA)", "CRI(SEA)", "");
        public static readonly BaseNomenclature CT_2K = new BaseNomenclature("CT-2K", "CT-2K", "");
        public static readonly BaseNomenclature DA_40 = new BaseNomenclature("DA-40", "DA-40", "");
        public static readonly BaseNomenclature DA_42 = new BaseNomenclature("DA-42", "DA-42", "");
        public static readonly BaseNomenclature DC9_80_MD88 = new BaseNomenclature("DC9 80/MD 88", "DC9 80/MD 88", "");
        public static readonly BaseNomenclature DC9_80_MD88_MD90 = new BaseNomenclature("DC9 80/MD88/MD90", "DC9 80/MD88/MD90", "");
        public static readonly BaseNomenclature E135 = new BaseNomenclature("E 135", "E 135", "");
        public static readonly BaseNomenclature EMB170 = new BaseNomenclature("EMB170", "EMB170", "");
        public static readonly BaseNomenclature ENF280 = new BaseNomenclature("ENF 280", "ENF 280", "");
        public static readonly BaseNomenclature ENF480 = new BaseNomenclature("ENF 480", "ENF 480", "");
        public static readonly BaseNomenclature ENF28 = new BaseNomenclature("ENF28", "ENF28", "");
        public static readonly BaseNomenclature ENF_48 = new BaseNomenclature("ENF-48", "ENF-48", "");
        public static readonly BaseNomenclature Enstrom280_FX = new BaseNomenclature("Enstrom 280 FX", "Enstrom 280 FX", "");
        public static readonly BaseNomenclature Enstrom480 = new BaseNomenclature("Enstrom 480", "Enstrom 480", "");
        public static readonly BaseNomenclature EnstromF28_280 = new BaseNomenclature("Enstrom F28/280", "Enstrom F28/280", "");
        public static readonly BaseNomenclature F2000_IR = new BaseNomenclature("F 2000/IR", "F 2000/IR", "");
        public static readonly BaseNomenclature Falcon2000_2000EX = new BaseNomenclature("Falcon2000/2000EX", "Falcon2000/2000EX", "");
        public static readonly BaseNomenclature Falcon2000_2000EX_IR = new BaseNomenclature("Falcon2000/2000EX/IR", "Falcon2000/2000EX/IR", "");
        public static readonly BaseNomenclature G200 = new BaseNomenclature("G200", "G200", "");
        public static readonly BaseNomenclature G_V = new BaseNomenclature("G-V", "G-V", "");
        public static readonly BaseNomenclature IR_H = new BaseNomenclature("IR (H)", "IR (H)", "");
        public static readonly BaseNomenclature IR_SEA = new BaseNomenclature("IR (SEA)", "IR (SEA)", "");
        public static readonly BaseNomenclature IR_SP_A = new BaseNomenclature("IR SP (A)", "IR SP (A)", "");
        public static readonly BaseNomenclature IR_MEA = new BaseNomenclature("IR(MEA)", "Полети по прибори", "");
        public static readonly BaseNomenclature IRE_A = new BaseNomenclature("IRE(A)", "IRE(A)", "");
        public static readonly BaseNomenclature IRI_A = new BaseNomenclature("IRI(A)", "IRI(A)", "");
        public static readonly BaseNomenclature IRI_MEA = new BaseNomenclature("IRI(MEA)", "IRI(MEA)", "");
        public static readonly BaseNomenclature Ka_32 = new BaseNomenclature("Ka-32", "Ka-32", "");
        public static readonly BaseNomenclature Learjet60 = new BaseNomenclature("Learjet 60", "Learjet 60", "");
        public static readonly BaseNomenclature LetL410 = new BaseNomenclature("LetL410", "Let L-410 Turbolet", "");
        public static readonly BaseNomenclature LetL410_IR = new BaseNomenclature("LetL410/IR", "LetL410/IR", "");
        public static readonly BaseNomenclature LR_JET = new BaseNomenclature("LR-JET", "LR-JET", "");
        public static readonly BaseNomenclature M_18 = new BaseNomenclature("M-18", "M-18", "");
        public static readonly BaseNomenclature McDonnellDouglasMD80 = new BaseNomenclature("MD80", "McDonnell Douglas MD80", "");
        public static readonly BaseNomenclature MI_2 = new BaseNomenclature("MI-2", "MI-2", "");
        public static readonly BaseNomenclature MI_8_IR = new BaseNomenclature("MI-8/IR", "MI-8/IR", "");
        public static readonly BaseNomenclature N_G = new BaseNomenclature("N(G)", "N(G)", "");
        public static readonly BaseNomenclature P2002JF = new BaseNomenclature("P 2002 JF", "P 2002 JF", "");
        public static readonly BaseNomenclature P92JS = new BaseNomenclature("P 92 JS", "P 92 JS", "");
        public static readonly BaseNomenclature P2002_P2006 = new BaseNomenclature("P2002/P2006", "P2002/P2006", "");
        public static readonly BaseNomenclature P_92_2002 = new BaseNomenclature("P-92/2002", "P-92/2002", "");
        public static readonly BaseNomenclature PA31_42 = new BaseNomenclature("PA 31/42", "PA 31/42", "");
        public static readonly BaseNomenclature Pa_23AZTEC = new BaseNomenclature("Pa-23 AZTEC", "Pa-23 AZTEC", "");
        public static readonly BaseNomenclature PA_34 = new BaseNomenclature("PA-34", "PA-34", "");
        public static readonly BaseNomenclature Piaggo180 = new BaseNomenclature("Piaggo 180", "Piaggo 180", "");
        public static readonly BaseNomenclature PipistrelSinus = new BaseNomenclature("Pipistrel Sinus", "Pipistrel Sinus", "");
        public static readonly BaseNomenclature PZLM18_PZL = new BaseNomenclature("PZL M 18 (PZL)", "PZL M 18 (PZL)", "");
        public static readonly BaseNomenclature R_22 = new BaseNomenclature("R-22", "R-22", "");
        public static readonly BaseNomenclature RA390 = new BaseNomenclature("RA390", "RA390", "");
        public static readonly BaseNomenclature RotorWay162F = new BaseNomenclature("Rotor way 162F", "Rotor way 162F", "");
        public static readonly BaseNomenclature SAAB340 = new BaseNomenclature("SAAB 340", "SAAB 340", "");
        public static readonly BaseNomenclature SAAB340_IR= new BaseNomenclature("SAAB 340/ IR", "SAAB 340/ IR", "");
        public static readonly BaseNomenclature SocataTBM850 = new BaseNomenclature("Socata TBM 850", "Socata TBM 850", "");
        public static readonly BaseNomenclature TBM = new BaseNomenclature("TBM", "TBM", "");
        public static readonly BaseNomenclature TL2000STING = new BaseNomenclature("TL 2000 STING", "TL 2000 STING", "");
        public static readonly BaseNomenclature Tu134 = new BaseNomenclature("Tu 134", "Tu 134", "");
        public static readonly BaseNomenclature TU154M = new BaseNomenclature("TU-154M", "TU-154M", "");
        public static readonly BaseNomenclature Yak40 = new BaseNomenclature("Yak 40", "Yak 40", "");
        public static readonly BaseNomenclature Z_37 = new BaseNomenclature("Z-37", "Z-37", "");
        public static readonly BaseNomenclature Zlin_143L = new BaseNomenclature("Zlin-143L", "Zlin-143L", "");
        public static readonly BaseNomenclature Tu154 = new BaseNomenclature("Тu 154", "Tу 154", "");

        public AircraftTypeQualificationClassNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                A300_310,
                A319_320,
                A320,
                A320_IR,
                A330,
                A330_IR,
                A330_340,
                A340,
                A109,
                AN12,
                AN24,
                AN26,
                AS365_N1,
                ATR_42_72,
                ATR42,
                AVRORJ_BAe146,
                AVRORJ_BAe146_IR ,
                AW109,
                B737_300_500,
                B737_300_900,
                B737_600_800,
                B747_400,
                B777,
                B737,
                B757_767,
                BAe146,
                BAeATPJetstream_61,
                BE20,
                BE90,
                BE90_99_100_200,
                Bell430,
                Blanick,
                BO105_CBS,
                C500_550_560,
                C500_550_560_IR,
                C525,
                C550,
                Cessna100,
                Cessna150,
                Cessna150_172,
                Cessna152,
                Cessna152Lycoming, 
                Cessna152_172, 
                Cessna172 ,
                Cessna208Land,
                Cessna208Sea,
                Cessna510,
                Cessna152_172_182,
                CessnaSET,
                CL60,
                CL600,
                CL604 ,
                CL605,
                CL600_601,
                CL604_605,
                CL604_605_IR,
                CRI_A,
                CRI_MEA,
                CRI_SEA,
                CT_2K,
                DA_40,
                DA_42,
                DC9_80_MD88,
                DC9_80_MD88_MD90,
                E135,
                EMB170,
                ENF280,
                ENF480,
                ENF28,
                ENF_48,
                Enstrom280_FX,
                Enstrom480,
                EnstromF28_280,
                F2000_IR,
                Falcon2000_2000EX,
                Falcon2000_2000EX_IR,
                G200,
                G_V,
                IR_H,
                IR_SEA,
                IR_SP_A ,
                IR_MEA,
                IRE_A,
                IRI_A,
                IRI_MEA,
                Ka_32,
                Learjet60,
                LetL410,
                LetL410_IR,
                LR_JET,
                M_18 ,
                McDonnellDouglasMD80,
                MI_2,
                MI_8_IR,
                N_G,
                P2002JF,
                P92JS,
                P2002_P2006,
                P_92_2002,
                PA31_42,
                Pa_23AZTEC,
                PA_34,
                Piaggo180,
                PipistrelSinus,
                PZLM18_PZL,
                R_22,
                RA390,
                RotorWay162F,
                SAAB340,
                SAAB340_IR,
                SocataTBM850,
                TBM,
                TL2000STING,
                Tu134,
                TU154M,
                Yak40,
                Z_37,
                Zlin_143L,
                Tu154,
            };
        }
    }
}
