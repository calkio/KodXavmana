using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KodXavmanaMatrix.Model
{
    internal class ConvertTextToByte
    {
        public int[] GetConvertTextToByte(string text)
        {
            if (IsValid(text)) return null;

            byte[] bytes = GetBytesToText(text);
            string strByte = Formatting1or0(bytes);
            return ConvertStrToIntArray(strByte);
        }

        private bool IsValid(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            return false;
        }

        private byte[] GetBytesToText(string text)
        {
            return System.Text.Encoding.Unicode.GetBytes(text);
        }

        private string Formatting1or0(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes)
            {
                result += (Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return result;
        }

        private int[] ConvertStrToIntArray(string text)
        {
            return text.Select(c => int.Parse(c.ToString())).ToArray();
        }
    }
}
