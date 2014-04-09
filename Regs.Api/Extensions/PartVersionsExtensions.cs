using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Regs.Api.Models;

namespace Regs.Api.Extensions
{
    public static class PartVersionExtensions
    {
        public static Newtonsoft.Json.Linq.JToken Get(this PartVersion partVersion, string path)
        {
            return partVersion.Content.SelectToken(path);
        }

        public static string GetString(this PartVersion partVersion, string path)
        {
            return (string)partVersion.Get(path);
        }

        public static bool? GetBool(this PartVersion partVersion, string path)
        {
            return (bool?)partVersion.Get(path);
        }

        public static DateTime? GetDate(this PartVersion partVersion, string path)
        {
            return (DateTime?)partVersion.Get(path);
        }
    }
}
