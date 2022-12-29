using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    static class Base64
    {
        public static string ConvertToBase64(string inputStr)
        {
            var base64chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            var r = "";
            var p = "";
            var c = inputStr.Length % 3;
            // add a right zero pad to make this string a multiple of 3 characters 
            if (c > 0)
            {
                for (; c < 3; c++)
                {
                    p += '=';
                    inputStr += "\0";
                }
            }
            // increment over the length of the string, three characters at a time
            for (c = 0; c < inputStr.Length; c += 3)
            {
                // we add newlines after every 76 output characters, according to the MIME specs
                if (c > 0 && (c / 3 * 4) % 76 == 0)
                {
                    r += "\r\n";
                }
                // these three 8-bit (ASCII) characters become one 24-bit number
                var n = (Convert.ToString(inputStr[c], 2).PadLeft(8, '0') + Convert.ToString(inputStr[c + 1], 2).PadLeft(8, '0') + Convert.ToString(inputStr[c + 2], 2).PadLeft(8, '0'));
                // this 24-bit number gets separated into four 6-bit numbers

                // those four 6-bit numbers are used as indices into the base64 character list 
                var newList = Enumerable.Range(0, n.Count() / 6).Select(x => n.Substring(x * 6, 6)).ToList();

                r+= string.Join("", newList.Select(x => base64chars[Convert.ToByte(x,2)]));
            }
            // add the actual padding string, after removing the zero pad
            return r.Substring(0, r.Length - p.Length) + p;
        }

        public static string XOR(string str1, string str2)
        {
            string result = null;
            if (str1.Length > str2.Length)
            {
                for (int i = 0; i < (str1.Length - str2.Length); i++)
                {
                    str2 += '0';
                }
            }
            else if (str1.Length < str2.Length)
            {
                for (int i = 0; i < (str2.Length - str1.Length); i++)
                {
                    str1 += '0';
                }
            }
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] == str2[i])
                {
                    result += "0";
                }
                else
                {
                    result += "1";
                }
            }
            return result;
        }
        public static double Redundancy(double EntSh, double EntH)
        {
            return ((EntH - EntSh) / EntH) * 100;
        }
    }
}
