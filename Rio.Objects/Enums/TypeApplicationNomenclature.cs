using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class TypeApplicationNomenclature
    {
        public string Text { get; private set; }
        public string Value { get; private set; }
        public string Description { get; private set; }

        public static readonly TypeApplicationNomenclature ApprovalTypeInstrumental = new TypeApplicationNomenclature("01", "Одобряване на типа на средствата за измерване, в т.ч. всяка модификация или допълнение на одобрен тип  средства за измерване", "");
        public static readonly TypeApplicationNomenclature ApprovalTypeElectronic = new TypeApplicationNomenclature("12", "Издаване на удостоверение за одобряване на типа на електронните таксиметрови апарати с фискална памет/ЕТАФП/", "");

        public static readonly TypeApplicationNomenclature InitialVerification = new TypeApplicationNomenclature("02", "Първоначална проверка на средства за измерване", "");
        public static readonly TypeApplicationNomenclature SubsequentVerification = new TypeApplicationNomenclature("03", "Последваща проверка на средства за измерване", "");

        public static readonly TypeApplicationNomenclature MetrologicalЕxaminations = new TypeApplicationNomenclature("04", "Метрологична експертиза на средствата за измерване", "");

        public static readonly TypeApplicationNomenclature ProvidingInformation = new TypeApplicationNomenclature("05", "Предоставяне на справки от регистъра за одобрените за използване типове средства за измерване", "");

        public static readonly TypeApplicationNomenclature ApprovalPlayingGround = new TypeApplicationNomenclature("06", "Одобряване на типа на игрални съоръжения, в т.ч. на модификации на игрални автомати и други игрални съоръжения, които могат да се произвеждат, внасят и експлоатират в страната както и на лабораториите по чл. 79, ал. 2 от Закон за хазарта", "");

        public static readonly TypeApplicationNomenclature ApprovalType = new TypeApplicationNomenclature("07", "Одобряване на типа на фискални устройства", "");
        public static readonly TypeApplicationNomenclature ApprovalProgramModifications = new TypeApplicationNomenclature("08", "Одобряване на програмни модификации на одобрен тип фискални устройства", "");
        public static readonly TypeApplicationNomenclature Expertise = new TypeApplicationNomenclature("09", "Извършване на  експертиза на ФУ  /проверка за съответствие на устройството с одобрения тип ФУ/ ", "");
        public static readonly TypeApplicationNomenclature MatchingIntegrationSystem = new TypeApplicationNomenclature("10", "Издаване на свидетелство за съответствие на интегрирана автоматизирана система за управление на търговската дейност  с изискванията по Наредба № Н-18 - одобряване на интегрирана автоматизирана система за управление на търговската дейност", "");
        public static readonly TypeApplicationNomenclature ControlTesting = new TypeApplicationNomenclature("11", "Извършване на контролните изпитвания на одобрените типове ФУ и на единични бройки фискални устройства по предложение на органите на Националната агенция за приходите или на лица по чл. 3 от Наредба № Н-18, когато възникнат съмнения за несъответствие на фискални устройства с изискванията", "");

        public static readonly TypeApplicationNomenclature ConformityAssessmentScales = new TypeApplicationNomenclature("13", "Оценяване на съответствието на везни с неавтоматично действие, съгласно Разрешение № 002-ОС/18.09.2007 г, издадено от председателя на Държавната агенция за метрологичен и технически надзор", "");

        public static readonly TypeApplicationNomenclature ConformityAssessmentProducts = new TypeApplicationNomenclature("14", "Оценяване на съответствието на продукти", "");

        public static readonly TypeApplicationNomenclature CalibrationEtalons = new TypeApplicationNomenclature("15", "Калибриране на еталони и средства за измерване, които не подлежат на контрол съгласно ЗИ и нормативните актове по прилагането му", "");
        public static readonly TypeApplicationNomenclature SpreadingCalibration = new TypeApplicationNomenclature("16", "Разпространяване чрез калибриране на единиците от националните еталони към следващите по точност еталони в страната", "");
        public static readonly TypeApplicationNomenclature SertificationInstrumental = new TypeApplicationNomenclature("17", "Извършване на сертификация на сравнителни материали", "");
        public static readonly TypeApplicationNomenclature MeasurementMetrologic = new TypeApplicationNomenclature("18", "Извършване на измервания и метрологични изследвания, съобразно техническите възможности на националните еталонни лаборатории", "");
        public static readonly TypeApplicationNomenclature TestingValidationSoftware = new TypeApplicationNomenclature("19", "Изпитване и валидиране на софтуер за обработка на резултати от измервания", "");

        public TypeApplicationNomenclature()
        {
            this.Values = new List<TypeApplicationNomenclature>()
            {
                ApprovalTypeInstrumental,
                ApprovalTypeElectronic,
                InitialVerification,
                SubsequentVerification,
                MetrologicalЕxaminations,
                ProvidingInformation,
                ApprovalPlayingGround,
                ApprovalType,
                ApprovalProgramModifications,
                Expertise,
                MatchingIntegrationSystem,
                ControlTesting,
                ConformityAssessmentScales,
                ConformityAssessmentProducts,
                CalibrationEtalons,
                SpreadingCalibration,
                SertificationInstrumental,
                MeasurementMetrologic,
                TestingValidationSoftware,
            };
        }

        public TypeApplicationNomenclature(string value, string text, string description)
        {
            Value = value;
            Text = text;
            Description = description;
        }

        public List<TypeApplicationNomenclature> Values;
    }
}
