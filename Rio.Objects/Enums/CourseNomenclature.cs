using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class CourseNomenclature : BaseNomenclature
    {
        public CourseNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                new BaseNomenclature("010", "ACCIDENT INVESTIGATION AND PREVENTION AIG"),
                new BaseNomenclature("011", "Aircraft Accident Investigation and Prevention AIG"),
                new BaseNomenclature("019", "Other courses AIG OTH"),

                new BaseNomenclature("020", "AERONAUTICAL INFORMATION SERVICES AIS"),
                new BaseNomenclature("021", "Aeronautical Information Officer AIS GEN"),
                new BaseNomenclature("022", "Aeronautical Cartography AIS MAP 029 Other courses AIS OTH"),

                new BaseNomenclature("030", "AERONAUTICAL METEOROLOGICAL SERVICES MET"),
                new BaseNomenclature("031", "Meteorologist	MET"),
                new BaseNomenclature("034", "Meteorologist Technician MET TEC"),
                new BaseNomenclature("039", "Other courses 	MET OTH"),

                new BaseNomenclature("040", "ENVIRONMENT	ENV"),
                new BaseNomenclature("049", "Environment other courses	ENV OTH"),

                new BaseNomenclature("050", "AIR TRAFFIC CONTROL AND SEARCH AND RESCUE SERVICES 	ATC SAR"),
                new BaseNomenclature("051", "Air Traffic Control Assistant/Basic Induction 	ATC ASS"),
                new BaseNomenclature("052", "Aerodrome Control 	ATC TWR"),
                new BaseNomenclature("053", "Approach Control -Non-Radar (Procedural) 	ATC APP"),
                new BaseNomenclature("054", "Radar Control ATC 	RDR"),
                new BaseNomenclature("055", "Area Control -Non-Radar (Procedural) 	ATC ACC"),
                new BaseNomenclature("056", "Airspace Planning 	ATC PLN"),
                new BaseNomenclature("057", "Search and Rescue 	SAR"),
                new BaseNomenclature("058", "Air Traffic Control Automation 	ATC EDP"),
                new BaseNomenclature("059", "Other courses 	ATC/SAR OTH"),

                new BaseNomenclature("060", "AIR TRANSPORT 	ATE"),
                new BaseNomenclature("061", "Air Transport Statistics 	ATE STA"),
                new BaseNomenclature("062", "Air Transport Economics 	ATE CRS"),
                new BaseNomenclature("069", "Other courses 	ATE OTH"),

                new BaseNomenclature("070", "AIRCRAFT MAINTENANCE AND AIRWORTHINESS 	AMT AIR"),
                new BaseNomenclature("071", "Aircraft Maintenance -Airframe Systems 	AMT AFR"),
                new BaseNomenclature("072", "Aircraft Maintenance -Powerplant Systems 	AMT PWR"),
                new BaseNomenclature("073", "Aircraft Maintenance -Airframe and Powerplant Systems 	AMT AFR PWR"),
                new BaseNomenclature("074", "Aircraft Maintenance -Aircraft Instruments 	AMT IST"),
                new BaseNomenclature("075", "Aircraft Structural Repair Techniques	AMT STR"),
                new BaseNomenclature("076", "Aircraft Maintenance -Avionics 	AMT AVC"),
                new BaseNomenclature("078", "Airworthiness 	AIR"),
                new BaseNomenclature("079", "Other courses 	AMT AIR OTH"),

                new BaseNomenclature("100", "AIRPORT ENGINEERING AND MAINTENANCE 	AGA ENG MTC"),
                new BaseNomenclature("101", "Airport Engineering -Planning, Design and Construction 	AGA ENG"),
                new BaseNomenclature("104", "Airport Maintenance -Electrical, including Airport Lighting and Power Generator 	AGA MTC ELC"),
                new BaseNomenclature("105", "Airport Maintenance -Mechanical, including Air-conditioning Diesel Engines and Fire Vehicles 	AGA MTC MEC"),
                new BaseNomenclature("106", "Airport Maintenance -Pavement 	AGA MTC PAV"),
                new BaseNomenclature("108", "Material Procurement and Stock Control 	AGA PRO"),
                new BaseNomenclature("109", "Other courses 	AGA OTH"),

                new BaseNomenclature("110", "AIRPORT RESCUE AND FIRE FIGHTING SERVICES 	RFF"),
                new BaseNomenclature("111", "Airport Fire Fighter -Basic and Firemanship 	RFF BAS"),
                new BaseNomenclature("112", "Airport Fire Officer -Junior 	RFF JUN"),
                new BaseNomenclature("113", "Airport Fire Officer -Senior 	RFF SEN"),
                new BaseNomenclature("119", "Other courses 	RFF OTH"),

                new BaseNomenclature("120", "MANAGEMENT 	MGT"),
                new BaseNomenclature("121", "Management -General 	MGT GEN"),
                new BaseNomenclature("122", "Management -Air Traffic Services 	MGT ATS"),
                new BaseNomenclature("123", "Management -Civil Aviation Training 	MGT TRG"),
                new BaseNomenclature("124", "Management -Civil Aviation Administration 	MGT CAA"),
                new BaseNomenclature("125", "Airport Management -Administration 	MGT AGA ADM"),
                new BaseNomenclature("126", "Airport Management -Commercial 	MGT AGA CML"),
                new BaseNomenclature("127", "Airport Management -Technical 	MGT AGA TEC"),
                new BaseNomenclature("128", "Airport Certification AMPAP						MGT CER"),
                new BaseNomenclature("129", "other courses 	MGT OTH"),

                new BaseNomenclature("130", "AVIATION SECURITY	AVSEC"),
                new BaseNomenclature("131", "Aviation Security Professional Management	 - AMPAP			ASPM"),
                new BaseNomenclature("139", "Aviation Security other courses	AVSEC OTH"),

                new BaseNomenclature("140", "AVIATION MEDICINE 	MED"),
                new BaseNomenclature("141", "Civil Aviation Medicine 	MED"),
                new BaseNomenclature("149", "Aviation Medicine -Other courses 	MED OTH"),

                new BaseNomenclature("150", "CIVIL AVIATION ADMINISTRATION AND LEGISLATION 	CAA"),
                new BaseNomenclature("151", "Government Safety Inspector -Operations 	CAA OPS INSP"),
                new BaseNomenclature("152", "Civil Aviation Administration -Electronic Data Processing Applications 	CAA EDP"),
                new BaseNomenclature("153", "Civil Aviation Administration -Financing and Accounting Procedures 	CAA FIN"),
                new BaseNomenclature("154", "Air and Space Law 	CAA LAW"),
                new BaseNomenclature("155", "Procedures for Air Navigation Services -Operations (PANS-OPS) 	CAA PAN"),
                new BaseNomenclature("156", "Government Safety Inspector -Airworthiness 	CAA AIR INSP"),
                new BaseNomenclature("159", "Other courses 	CAA OTH"),

                new BaseNomenclature("160", "AERONAUTICAL COMMUNICATIONS AND NAVAIDS MAINTENANCE	COM MTC"),
                new BaseNomenclature("161", "Aeronautical Electronics and Radio Theory 	COM MTC RDO"),
                new BaseNomenclature("162", "Solid State Applications, Digital Logic and Microprocessors 	COM MTC EDP"),
                new BaseNomenclature("163", "Electronics Maintenance -Communications Equipment and Systems 	COM MTC ELC EQP"),
                new BaseNomenclature("164", "Navigational Aids Maintenance 	VOR/ILS/DME COM MTC NAV"),
                new BaseNomenclature("165", "Radar Systems Theory and Application 	COM MTC RDR"),
                new BaseNomenclature("166", "Teletypewriter Equipment Maintenance 	COM MTC RTT"),
                new BaseNomenclature("167", "Global Navigation Satellite System 	GNSS"),
                new BaseNomenclature("169", "Other courses 	COM MTC OTH"),

                new BaseNomenclature("170", "AERONAUTICAL COMMUNICATIONS OPERATIONS COM OPS"),
                new BaseNomenclature("171", "Aeronautical Mobile Service Operator 	COM OPS AMS"),
                new BaseNomenclature("172", "Aeronautical Fixed Service Operator 	COM OPS AFS"),
                new BaseNomenclature("173", "Aeronautical Station Operator/Fixed Services Operator Radiotelegraphy Rating 	COM OPS AFS WT"),
                new BaseNomenclature("174", "Aeronautical Station Operator/Fixed Services Operator Radiotelephony, Teletypewriter and Radiotelegraphy Ratings (Combined) 	COM OPS AMS AFS WT"),
                new BaseNomenclature("176", "Aeronautical Communications Service Supervisor 	COM OPS SUP"),
                new BaseNomenclature("177", "Message Checking and Accounting 	COM OPS ACT"),
                new BaseNomenclature("179", "Other courses 	COM OPS OTH"),

                new BaseNomenclature("180", "DANDEROUS GOODS	DNG"),
                new BaseNomenclature("189", "Dangerous Goods other courses	DNG OTH"),

                new BaseNomenclature("190", "MACHINE READABLE TRAVEL DOCUMENTS	MRTD"),
                new BaseNomenclature("190", "Machine readable travel documents other courses	MRTD OTH"),

                new BaseNomenclature("200", "SAFETY MANAGEMENT	FSAHF"),
                new BaseNomenclature("201", "Safety Management Systems	SMS"),
                new BaseNomenclature("202", "State Safety Programme	SSP"),
                new BaseNomenclature("203", "Fatigue Risk Management System	FRMS"),
                new BaseNomenclature("204", "Threat and Error Management 	TEM"),
                new BaseNomenclature("205", "European Co-ordination Centre for Aviation Incident Reporting System	ECCAIRS"),
                new BaseNomenclature("209", "Safety Management other courses	SMS OTH"),

                new BaseNomenclature("210", "TRAINING TECHNOLOGY INS"),
                new BaseNomenclature("211", "Instructional Techniques -Basic 	INS BAS"),
                new BaseNomenclature("212", "Instructional Techniques -Advanced 	INS ADV"),
                new BaseNomenclature("214", "Course Design/Development 	INS SYL"),
                new BaseNomenclature("216", "Training Aids Maintenance 	INS TRG MTC"),
                new BaseNomenclature("219", "Other courses 	INS OTH"),

                new BaseNomenclature("230", "FLIGHT OPERATIONS SERVICES PIL"),
                new BaseNomenclature("231", "Fixed Wing Aircraft -Private Pilot Licence and Flight Radio Telephone Operator Licence 	PIL PPL PLN"),
                new BaseNomenclature("232", "Fixed Wing Aircraft -Commercial Pilot Licence 	PIL CPL PLN"),
                new BaseNomenclature("233", "Fixed Wing Aircraft -Instrument Rating 	PIL IFR PLN"),
                new BaseNomenclature("234", "Fixed Wing Aircraft -Instrument Rating Multi-Engine Land 	PIL IFR MFL PLN"),
                new BaseNomenclature("236", "Fixed Wing Aircraft -Commercial Pilot Licence with Instrument Rating and Class Rating Multi-Engine Land 	PIL CPL IFR MFL"),
                new BaseNomenclature("237", "Fixed Wing Aircraft -Flight Instructor Rating 	PIL INS FLT"),
                new BaseNomenclature("241", "Helicopter -Private Pilot Licence and Flight Radio Telephone Operator Licence 	PIL PL HEL"),
                new BaseNomenclature("242", "Helicopter -Commercial Pilot Licence 	PIL CPL HEL"),
                new BaseNomenclature("243", "Helicopter -Instrument Rating 	PIL IFR HEL"),
                new BaseNomenclature("244", "Helicopter -Flight Instructor Rating 	PIL INS HEL"),
                new BaseNomenclature("251", "Airline Transport Pilot Licence -Theory 	PIL ADV ATP"),
                new BaseNomenclature("253", "Courses for Jet Aircraft Operations 	PIL ADV JET"),
                new BaseNomenclature("254", "Pilot Flight Examiners/Check Captains 	PIL ADV EXM FLT"),
                new BaseNomenclature("255", "Flight Engineer 	PIL ENG"),
                new BaseNomenclature("259", "Other courses 	PIL OTH"),

                new BaseNomenclature("260", "FLIGHT CALIBRATION 	FLT CAL"),
                new BaseNomenclature("261", "Flight Calibration -Flight Panel Operator 	FLT PNL"),
                new BaseNomenclature("262", "Flight Calibration -Ground Operator 	FLT CAL GND"),
                new BaseNomenclature("264", "Flight Calibration Pilot	FLT CAL PIL"),
                new BaseNomenclature("269", "Other courses 	FLT CAL OTH"),

                new BaseNomenclature("270", "AIRLINE CABIN AND SUPPORT SERVICES 	ACS"),
                new BaseNomenclature("271", "Airline Ground Services -Passenger, Cargo, Ticketing and Reservations 	ACS GND"),
                new BaseNomenclature("275", "Airline Cabin Services 	ACS CAB"),
                new BaseNomenclature("278", "Airline Flight Operations Officer/Dispatcher 	ACS DIS"),
                new BaseNomenclature("279", "Other courses 	ACS OTH"),

                new BaseNomenclature("290", "LANGUAGE TRAINING 	LAN"),
                new BaseNomenclature("291", "English Language 	LAN ENG"),
                new BaseNomenclature("292", "French Language 	LAN FRE"),
                new BaseNomenclature("293", "Russian Language	LAN RUS"),
                new BaseNomenclature("294", "Spanish Language 	LAN SPA"),
                new BaseNomenclature("295", "Language Proficiency Testing 	LAN TST"),
                new BaseNomenclature("299", "Other Languages 	LAN OTH"),

                new BaseNomenclature("300", "OTHER CIVIL AVIATION TRAINING COURSES"),

            };
        }
    }
}
