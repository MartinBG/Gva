using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Common.Json;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.WordTemplates
{
    public class Utils
    {
        internal static string GetPhonesString(PersonDataDO personData)
        {
            string[] phones = {
                personData.Phone5,
                personData.Phone1,
                personData.Phone2,
                personData.Phone3,
                personData.Phone4
            };
            phones = phones.Where(p => !String.IsNullOrEmpty(p)).ToArray();
            return String.Join(", ", phones);
        }

        internal static object GetLicenceHolder(PersonDataDO personData, PersonAddressDO personAddress)
        {
            return new
            {
                NAME = string.Format(
                    "{0} {1} {2}",
                    personData.FirstName,
                    personData.MiddleName,
                    personData.LastName).ToUpper(),
                LIN = personData.Lin,
                EGN = personData.Uin,
                ADDRESS = string.Format(
                    "{0}, {1}",
                    personAddress.Settlement !=null ? personAddress.Settlement.Name : null,
                    personAddress.Address),
                TELEPHONE = GetPhonesString(personData)
            };
        }

        internal static string PadLicenceNumber(int? licenceNumber = 0)
        {
            return licenceNumber.ToString().PadLeft(5, '0');
        }

        internal static List<object> FillBlankData(List<object> data, int size)
        {
            while (data.Count < size)
            {
                data.Add(new object());
            }
            return data;
        }
    }
}
