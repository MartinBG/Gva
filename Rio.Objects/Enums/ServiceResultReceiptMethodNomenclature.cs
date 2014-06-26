using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ServiceResultReceiptMethodNomenclature
    {
        public string Text { get; private set; }
        public string Uri { get; private set; }

        public static readonly ServiceResultReceiptMethodNomenclature ByEmailOrWebApplication =
            new ServiceResultReceiptMethodNomenclature { Text = "Чрез електронна поща/уеб базирано приложение", Uri = "0006-000076" };
        public static readonly ServiceResultReceiptMethodNomenclature OnDesk =
            new ServiceResultReceiptMethodNomenclature { Text = "На гише", Uri = "0006-000077" };
        public static readonly ServiceResultReceiptMethodNomenclature OnDeskInOtherAdministration =
            new ServiceResultReceiptMethodNomenclature { Text = "На гише в друга общинска администрация", Uri = "0006-000078" };
        public static readonly ServiceResultReceiptMethodNomenclature ByPostOnContactAddress =
            new ServiceResultReceiptMethodNomenclature { Text = "Чрез пощенски куриерски служби, на посочения адрес за кореспонденция", Uri = "0006-000079" };
        public static readonly ServiceResultReceiptMethodNomenclature ByPostOnOtherAddress =
            new ServiceResultReceiptMethodNomenclature { Text = "Чрез пощенски куриерски служби, на друг адрес", Uri = "0006-000080" };
        public static readonly ServiceResultReceiptMethodNomenclature ByPostOnPostOfficeBox =
            new ServiceResultReceiptMethodNomenclature { Text = "Чрез пощенски куриерски служби, пощенска кутия", Uri = "0006-000081" };

        public static readonly IEnumerable<ServiceResultReceiptMethodNomenclature> Values =
            new List<ServiceResultReceiptMethodNomenclature>
            {
                ByEmailOrWebApplication,
                OnDesk,
                OnDeskInOtherAdministration,
                ByPostOnContactAddress,
                ByPostOnOtherAddress,
                ByPostOnPostOfficeBox,
            };
    }
}
