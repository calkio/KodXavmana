using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritm2.Model
{
    internal class ConvertStringToByte
    {
        public static string GetConvertTextToByte(string text)
        {
            if (IsValid(text)) return null;

            byte[] bytes = GetBytesToText(text);
            string strByte = Formatting1or0(bytes);
            return strByte;
        }

        private static bool IsValid(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            return false;
        }

        private static byte[] GetBytesToText(string text)
        {
            return System.Text.Encoding.Unicode.GetBytes(text);
        }

        private static string Formatting1or0(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes)
            {
                result += (Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return result;
        }
    }
}
