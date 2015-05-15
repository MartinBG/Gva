using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ProcedureAssessmentConformityTypeNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature TypeExamination = new BaseNomenclature("01", "Изследване на типа", "");
        public static readonly BaseNomenclature DeclarationConformity = new BaseNomenclature("02", "Осигуряване на качеството на производството", "");
        public static readonly BaseNomenclature Verification = new BaseNomenclature("03", "Проверка на продукта", "");
        public static readonly BaseNomenclature UnitVerification = new BaseNomenclature("04", "Проверка на единичен продукт", "");

        public ProcedureAssessmentConformityTypeNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                TypeExamination,
                DeclarationConformity,
                Verification,
                UnitVerification,
            };
        }
    }
}
