using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1. На основе информационного сообщения, представленного
            символами русского/английского алфавитов, служебными символами и цифрами, 
            содержащегося в некотором текстовом файле
            сформировать информационное сообщение в двоичном виде; 
            длина сообщения в бинарном виде должна быть не менее 16 символов*/
            string origXk, Xk;
            Console.WriteLine("Текст из файла: " + (origXk = HammingCode.TextReader(@"D:\учеба\ЗИиНИС\Лабораторные\лр 4\Lab4\test.txt")));
            Console.WriteLine("\nИнформационное сообщение в двоичном виде: " + (Xk = HammingCode.ConvertToBinary(origXk)));
            /*2. Для полученного информационного слова построить проверочную матрицу Хемминга*/
            int k = Xk.Length;
            int r = (int)Math.Log(k, 2) + 1;

            Console.WriteLine("\nПроверочная матрица Хемминга:");
            char[,] hammingMatrix = HammingCode.HammingMatrix(r, k);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < (k + r); j++)
                {
                    Console.Write(hammingMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            /*3. вычислить избыточные символы (слово Xr). */
            string Xr;
            Console.WriteLine("\nИзбыточные слово: " +(Xr = HammingCode.RedundancySymbols(Xk, hammingMatrix, r, k)));
            string Xn = string.Concat(Xk, Xr);
            Console.WriteLine("Кодовое слов: " + Xn);
            /*4. Принять исходное слово со следующим числом ошибок: 0, 1, 2. 
             Позиция ошибки определяется (генерируется) случайным образом. */
            string Yn = Xn;
            string Yr, errorVector;
            Console.WriteLine("\nЧисло ошибок - 0, Yn: " + Yn);
            /*5. Для полученного слова Yn = Yk, Yr, используя уже известную проверочную матрицу Хемминга, 
             * вновь вычислить избыточные символы (обозначим их Yr’)*/
            Console.WriteLine("Проверочные символы Y`r: " + (Yr = HammingCode.RedundancySymbols(Xk, hammingMatrix, r, k)));
            /*6. Вычислить синдром.
             Если анализ синдрома показал, что информационное сообщение было
            передано с ошибкой (или 2 ошибками), сгенерировать унарный
            вектор ошибки Еn = е1, е2, …, еn и исправить одиночную ошибку*/
            Console.WriteLine("Синдром: " + HammingCode.XOR(Yr, Xr));

            string Yk = HammingCode.RandomError(Xk, 1);
            Console.WriteLine("\nЧисло ошибок - 1, Yn: " + string.Concat(Yk, Xr));
            Console.WriteLine("Проверочные символы Y`r: " + (Yr = HammingCode.RedundancySymbols(Yk, hammingMatrix, r, k)));
            Console.WriteLine("Синдром: " + HammingCode.XOR(Yr, Xr));
            Console.WriteLine("Вектор ошибки: " + (errorVector = HammingCode.ErrorVector(HammingCode.ErrorPosition(hammingMatrix, HammingCode.XOR(Yr, Xr), r, k), r + k)));
            Console.WriteLine("Исправленное сообщение: " + HammingCode.XOR(Yk + Xr, errorVector));

            Yk = HammingCode.RandomError(Xk, 2);
            Console.WriteLine("\nЧисло ошибок - 2, Yn: " + string.Concat(Yk, Xr));
            Console.WriteLine("Проверочные символы Y`r: " + (Yr = HammingCode.RedundancySymbols(Yk, hammingMatrix, r, k)));
            Console.WriteLine("Синдром: " + HammingCode.XOR(Yr, Xr));
            Console.WriteLine("Вектор ошибки: " + (errorVector = HammingCode.ErrorVector(HammingCode.ErrorPosition(hammingMatrix, HammingCode.XOR(Yr, Xr), r, k), r + k)));

            Console.ReadLine();
        }
    }
}