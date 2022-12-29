using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    static class Alphabet
    {
        public static Dictionary<char, int> LettersDict(string text)
        {
            Dictionary<char, int> lettersDict = new Dictionary<char, int>();
            foreach (char c in text)
            {
                if (char.IsDigit(c) || char.IsLetter(c))
                {
                    if (!lettersDict.ContainsKey(c))
                        lettersDict.Add(c, 1);
                    else lettersDict[c]++;
                }
            }
            return lettersDict;
        }

        public static Dictionary<char, double> Probs(string text)
        {
            Dictionary<char, int> lettersDict = LettersDict(text);
            Dictionary<char, double> letters = new Dictionary<char, double>();
            for (int i = 0; i < lettersDict.Values.Count; i++)
            {
                letters.Add(lettersDict.Keys.ToArray()[i], (double)lettersDict.Values.ToArray()[i] / lettersDict.Values.Sum());
            }
            return letters;
        }

        public static double ShenonEntropy(string text)
        {
            double entropy = 0;
            Dictionary<char, double> lettersProbs = Probs(text);
            for (int i = 0; i < lettersProbs.Values.Count; i++)
            {
                entropy += lettersProbs.Values.ToArray()[i] * Math.Log(lettersProbs.Values.ToArray()[i], 2);
            }
            return -entropy;
        }

        public static double HartleyEntropy(int count)
        {
            return Math.Log(count, 2);
        }

        public static string ConvertToAscii(string str)
        {
            string asciStr = null;
            for (int i = 0; i < str.Length; i++)
            {
                asciStr += Convert.ToString(str[i], 2);
            }
            return asciStr;
        }
    }
}
