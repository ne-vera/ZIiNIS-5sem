using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double latEntropy = 0, bulgEntropy = 0, binEntropy = 0;
            //а) рассчитать энтропию болгароского и латышский
            //входной параметр провизвольный электронный документ
            Console.WriteLine("Рассчитать энтропию алфавитов: один – на латинице, другой – на кириллице \n");
            latEntropy = Alphabet.Entropy(Alphabet.TextReader(@"D:\учеба\ЗИиНИС\Лабораторные\лр 2\Lab2\latvian.txt"));
            Console.WriteLine("Энтропия латышского: " + latEntropy);
            bulgEntropy = Alphabet.Entropy(Alphabet.TextReader(@"D:\учеба\ЗИиНИС\Лабораторные\лр 2\Lab2\bulgarian.txt"));
            Console.WriteLine("Энтропия болгарского: " + bulgEntropy);
            //частоты появления символов алфавитов оформить в виде гистограмм
            Alphabet.Serialize(@"D:\учеба\ЗИиНИС\Лабораторные\лр 2\Lab2\probs_latv.xml", Alphabet.Probs(Alphabet.TextReader(@"D:\учеба\ЗИиНИС\Лабораторные\лр 2\Lab2\latvian.txt")));
            Alphabet.Serialize(@"D:\учеба\ЗИиНИС\Лабораторные\лр 2\Lab2\probs_bulg.xml", Alphabet.Probs(Alphabet.TextReader(@"D:\учеба\ЗИиНИС\Лабораторные\лр 2\Lab2\bulgarian.txt")));
            //б) определить энтропию бинарного алфавита
            Console.WriteLine("\nДля входных документов, представленных в бинарных кодах, определить энтропию бинарного алфавита \n");
            binEntropy = Alphabet.Entropy(Alphabet.ConvertToAscii(Alphabet.TextReader(@"D:\учеба\ЗИиНИС\Лабораторные\лр 2\Lab2\latvian.txt")));
            Console.WriteLine("Энтропия бинарного для документа на латышском: " + binEntropy);
            Console.WriteLine("Энтропия бинарного для документа на болгарском: " + Alphabet.Entropy(Alphabet.ConvertToAscii(Alphabet.TextReader(@"D:\учеба\ЗИиНИС\Лабораторные\лр 2\Lab2\bulgarian.txt"))));
            //в) подсчитать кол-во информации в ФИО на исх. алфавите и кодах ASCII
            Console.WriteLine("\nПодсчитать количество информации в сообщении, состоящем из собственных фамилии, имени и отчества\n");
            Console.WriteLine("Количество информации на латышском: " + Alphabet.QuantityOfInformation(latEntropy, "Prigodiča Vera Valerievna"));
            Console.WriteLine("Количество информации на болгарском: " + Alphabet.QuantityOfInformation(bulgEntropy, "Пригодич Вера Валериевна"));
            Console.WriteLine("Количество информации в ASCII: " + Alphabet.QuantityOfInformation(binEntropy, Alphabet.ConvertToAscii("Prigodiča Vera Valerievna")));
            //г) вероятность 0,1
            Console.WriteLine("\nПри условии, что вероятность ошибочной передачи единичного бита сообщения составляет\n");
            Console.WriteLine("Количество информации на латышском с вероятностью 0,1: " + Alphabet.MistakeQuantity(0.1, "Prigodiča Vera Valerievna", latEntropy));
            Console.WriteLine("Количество информации на болгарском с вероятностью 0,1: " + Alphabet.MistakeQuantity(0.1, "Пригодич Вера Валериевна", bulgEntropy));
            Console.WriteLine("Количество информации в ASCII с вероятностью 0,1: " + Alphabet.MistakeQuantity(0.1, Alphabet.ConvertToAscii("Prigodiča Vera Valerievna"), binEntropy));
            //вероятность 0,5
            Console.WriteLine("\nКоличество информации на латышском с вероятностью 0,5: " + Alphabet.MistakeQuantity(0.5, "Prigodiča Vera Valerievna", 1));
            Console.WriteLine("Количество информации на болгарском с вероятностью 0,5: " + Alphabet.MistakeQuantity(0.5, "Пригодич Вера Валериевна", 1));
            Console.WriteLine("Количество информации в ASCII с вероятностью 0,5: " + Alphabet.MistakeQuantity(0.5, Alphabet.ConvertToAscii("Prigodiča Vera Valerievna"), 1));
            //вероятность 1,0
            Console.WriteLine("\nКоличество информации на латышском с вероятностью 1,0: " + Alphabet.MistakeQuantity(0.999, "Prigodiča Vera Valerievna", latEntropy));
            Console.WriteLine("Количество информации на болгарском с вероятностью 1,0: " + Alphabet.MistakeQuantity(0.999, "Пригодич Вера Валериевна", bulgEntropy));
            Console.WriteLine("Количество информации в ASCII с вероятностью 1,0: " + Alphabet.MistakeQuantity(0.999, Alphabet.ConvertToAscii("Prigodiča Vera Valerievna"), binEntropy));
            Console.ReadLine();
        }
    }
}
