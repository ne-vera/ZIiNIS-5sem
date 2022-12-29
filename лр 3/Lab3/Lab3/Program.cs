using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            1.конвертировать произвольный документ(а) на латинице  в документ(б) формата base64
            входной параметр случайное число от 999999
            Random rand = new Random(999999);
            int input = rand.Next();
            string baseString;
            double shenonA;
            double shenonB;
            double hartleyA;
            double hartleyB;
            string name = "Vera";
            string lastname = "Prigodich";
            Console.WriteLine("Входной параметр: " + input);
            Console.WriteLine("Base64: " + (baseString = Base64.ConvertToBase64(input.ToString())));
            //2. получить распределение частотных свойств алфавитов по документам(а) и(б).
            Console.WriteLine("\nЭнтропия по Шеннону(а): " + (shenonA = Alphabet.ShenonEntropy(input.ToString())));
            Console.WriteLine("Энтропия по Шеннону(b): " + (shenonB = Alphabet.ShenonEntropy(baseString)));
            //Вычислить энтропию Хартли и Шеннона, а также избыточность алфавитов.
            Console.WriteLine("\nЭнтропия по Хартли(a): " + (hartleyA = Alphabet.HartleyEntropy(26)));
            Console.WriteLine("Энтропия по Хартли(b): " + (hartleyB = Alphabet.HartleyEntropy(64)));
            Console.WriteLine("\nИзбыточность(а): " + Base64.Redundancy(shenonA, hartleyA));
            Console.WriteLine("Избыточность(b): " + Base64.Redundancy(shenonB, hartleyB));
            //3. Написать функцию, которая принимает в качестве аргументов два буфера (а и b) одинакового размера и возвращает XOR 
            //(собственная фамилия(а) и имя(b);
            //Входные аргументы представлять: 1) в кодах ASCII;
            Console.WriteLine("\nXOR(ASCII): " + Base64.XOR(Alphabet.ConvertToAscii(name), Alphabet.ConvertToAscii(lastname)));
            //2) в кодах base64.
            Console.WriteLine("XOR(Base64): " + Base64.XOR("010101100110010101110010011000010000", "010100000111001001101001011001110110111101100100011010010110001101101000"));
            Console.WriteLine("faea8766 в Base64: " + Base64.ConvertToBase64("faea8766"));
            Console.WriteLine(Base64.XOR(Alphabet.ConvertToAscii("faea8766"), Alphabet.ConvertToAscii("1f180308050d024c030a4c18")));
            Console.ReadLine();
        }
    }
}
