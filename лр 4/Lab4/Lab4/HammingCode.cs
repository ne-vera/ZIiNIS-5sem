using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    abstract class HammingCode
    {
        public static string TextReader(string path)
        {
            string str;
            using (StreamReader reader = new StreamReader(path))
            {
                str = reader.ReadToEnd().ToLower();
            }
            return str;
        }

        public static string ConvertToBinary(string str)
        {
            string binStr = null;
            foreach (char c in str)
            {
                if (c != ' ')
                {
                    binStr += Convert.ToString(c, 2).PadLeft(8, '0');
                }
            }
            return binStr;
        }

        public static int Factorial(int n)
        {
            if (n == 1)
                return 1;
            else 
                return n * Factorial(n - 1);
        }

        public static int NewtonBinom(int wt, int r)
        {
            return Factorial(r) / (Factorial(wt) * Factorial(r - wt));
        }

        static public char[,] MatrixA(int r, int k)
        {
            int countOfColomn = 0;
            int wt = 2;

            char[,] matrixA = new char[r, k];
            while (true)
            {
                int amountAllCombination = NewtonBinom(wt, r);
                int rightCombination = 0;
                int number = 3;

                while (rightCombination != amountAllCombination && countOfColomn != k)
                {

                    string binaryNumber = Convert.ToString(number, 2);
                    int binaryNumberLength = binaryNumber.Length;

                    //Счетчик единиц в бинарной строке
                    int unitCounter = 0;

                    for (int i = 0; i < binaryNumberLength; i++)
                    {
                        if (binaryNumber[i] == '1')
                            unitCounter++;
                    }

                    if (unitCounter == wt)
                    {
                        countOfColomn++;

                        if (binaryNumberLength < r)
                        {
                            for (int j = 0; j < (r - binaryNumberLength); j++)
                            {
                                binaryNumber = "0" + binaryNumber;
                            }
                        }

                        for (int i1 = 0; i1 < r; i1++)
                        {
                            matrixA[i1, countOfColomn - 1] = binaryNumber[i1];
                        }

                        rightCombination++;
                    }
                    number++;
                }

                if (countOfColomn != k)
                    wt++;
                else
                    break;
            }
            return matrixA;
        }

        static public char[,] MatrixI(int r)
        {
            char[,] matrixI = new char[r, r];

            int k = 0;
            for (int i = k; i < r; i++)
            {
                for(int j = 0; j < r; j++)
                {
                    if (j == i - k)
                        matrixI[i, j] = '1';
                    else
                        matrixI[i, j] = '0';
                }
            }
            return matrixI;
        }

        static public char[,] HammingMatrix(int r, int k)
        {
            char[,] hammingMatrix = new char[r, r + k];
            char[,] matrixA = MatrixA(r, k);
            char[,] matrixI = MatrixI(r);

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    hammingMatrix[j, i] = matrixA[j, i];
                }
            }
            int i1 = 1;
            for (int i = k; i < (k + r); i++)
            {
                for (int j = 0; j < r; j++)
                {
                    hammingMatrix[j, i] = matrixI[j, i1 - 1];
                }
                i1++;
            }
            return hammingMatrix;
        }

        static public string XOR(string a, string b)
        {
            if (a.Length < b.Length)
            {
                a = a.PadLeft(b.Length, '0');
            }
            else if (a.Length > b.Length)
            {
                b = b.PadLeft(a.Length, '0');
            }
            string result = null;
            for (int i = 0; i < a.Length; i++)
            {
                if ((a[i] == '0' && b[i] == '0') || (a[i] =='1' && b[i] == '1'))
                {
                    result += '0';
                }
                else
                {
                    result += '1';
                }
            }
            return result;
        }

        static public string RedundancySymbols(string Xk, char[,] hammingMatrix, int r, int k)
        {
            string Xr = null;
            int pairs = 0;
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if (hammingMatrix[i, j] == '1' && Xk[j] == '1')
                        pairs++;
                }
                if (pairs % 2 == 0)
                    Xr += '0';
                else
                    Xr += '1';
                pairs = 0;
            }
            return Xr;
        }

        static public string RandomError(string Xk, int amount)
        {
            char[] Yk = new char[Xk.Length];
            Yk = Xk.ToArray<char>();
            int previosPosition = 100;
            int currentPosition;

            for (int i = 0; i < amount; i++)
            {
                Random random = new Random();
                currentPosition = random.Next(0, Xk.Length);
                while (currentPosition == previosPosition)
                {
                    currentPosition = random.Next(0, Xk.Length);
                }
                Yk[currentPosition] = char.Parse(XOR(Yk[currentPosition].ToString(), '1'.ToString()));
                Thread.Sleep(100);
                previosPosition = currentPosition;
            }
            return new string(Yk);
        }

        static public string ErrorVector(int position, int n)
        {
            string errorVector = null;
            for (int i = 0; i < n; i++)
            {
                if (i != position)
                    errorVector += '0';
                else
                    errorVector += '1';
            }
            return errorVector;
        }

        static public int ErrorPosition(char[,] hammingMatrix, string syndrome, int r, int k)
        {
            int amountCoincidences = 0;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    if (hammingMatrix[j, i] == syndrome[j])
                        amountCoincidences++;
                }

                if (amountCoincidences == r)
                    return i;
                else
                    amountCoincidences = 0;
            }
            return -1;
        }


    }
}
