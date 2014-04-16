using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class Utf8Utils
    {
        public static byte[] GetBytes(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return null;

            UTF8Encoding encoding = new System.Text.UTF8Encoding(false);

            return encoding.GetBytes(str);
        }

        public static string GetString(byte[] bytes)
        {
            if (bytes == null)
                return String.Empty;

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding(false);
            return encoding.GetString(bytes).TrimStart('\uFEFF'); //Removing BOM symbol
        }
    }
}
