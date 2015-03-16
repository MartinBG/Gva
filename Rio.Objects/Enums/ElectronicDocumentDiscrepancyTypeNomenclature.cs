using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ElectronicDocumentDiscrepancyTypeNomenclature
    {
        public string Text { get; private set; }
        public string Uri { get; private set; }

        public static readonly ElectronicDocumentDiscrepancyTypeNomenclature IncorrectFormat = new ElectronicDocumentDiscrepancyTypeNomenclature { Text = "Подаваното заявление не е в нормативно установения формат", Uri = "0006-000005" };

        public static readonly ElectronicDocumentDiscrepancyTypeNomenclature SizeTooLarge = new ElectronicDocumentDiscrepancyTypeNomenclature { Text = "Размерът на заявлението заедно с приложенията надвишава определения от административния орган размер за електронните административни услуги, предоставяни от съответната администрация", Uri = "0006-000006" };

        public static readonly ElectronicDocumentDiscrepancyTypeNomenclature IncorrectAttachmentsFormat = new ElectronicDocumentDiscrepancyTypeNomenclature { Text = "Приложените към заявлението документи не са в нормативно установения формат (непозволено разширение на файл)", Uri = "0006-000007" };

        public static readonly ElectronicDocumentDiscrepancyTypeNomenclature SuspiciousAttachments = new ElectronicDocumentDiscrepancyTypeNomenclature { Text = "Подаденото заявление и приложенията към него съдържат вируси или друг нежелан софтуер", Uri = "0006-000008" };

        public static readonly ElectronicDocumentDiscrepancyTypeNomenclature NotAuthenticated = new ElectronicDocumentDiscrepancyTypeNomenclature { Text = "Подаденото заявление не съдържа уникален идентификатор на заявителя и на получателя на електронната административна услуга при законово изискване за идентификация", Uri = "0006-000009" };

        public static readonly ElectronicDocumentDiscrepancyTypeNomenclature NoEmail = new ElectronicDocumentDiscrepancyTypeNomenclature { Text = "Заявителят не е посочил електронен пощенски адрес", Uri = "0006-000010" };

        public static readonly ElectronicDocumentDiscrepancyTypeNomenclature CannotAccessPhysicalStorage = new ElectronicDocumentDiscrepancyTypeNomenclature { Text = "Не е налице техническа възможност за достъп до съдържанието на подадения на физически носител електронен документ", Uri = "0006-000011" };

        public static readonly IEnumerable<ElectronicDocumentDiscrepancyTypeNomenclature> Values =
            new List<ElectronicDocumentDiscrepancyTypeNomenclature>
            {
                IncorrectFormat,
                SizeTooLarge,
                IncorrectAttachmentsFormat,
                SuspiciousAttachments,
                NotAuthenticated,
                NoEmail,
                CannotAccessPhysicalStorage
            };
    }
}
