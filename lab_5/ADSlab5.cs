using System;
using System.Collections.Generic;

namespace ADS_lab_5
{
    class ADSlab5
    {
        static int getCharFromPos(string str, int d)
        {
            if (str.Length <= d)
                return -1;
            else
                return (int)(str[d]);
        }

        static void sortMSD(string[] str, int low, int high, int d)
        {         
            if (high <= low) return;

            Console.WriteLine("low: " + low + ". high: " + high + ". symb: " + d);

            int[] count = new int[257]; //256 + 1
            Dictionary<int, string> temp = new Dictionary<int, string>();

            //добавить единичку к ячейке с индексом - номером в ASCII.
            for (int i = low; i <= high; i++)
            {
                int c = getCharFromPos(str[i], d);
                count[c]++;
            }

            //актуализация данных складыванием
            for(int i = 0; i < 256; i++)
                count[i + 1] += count[i];

            //кладем строчки в словарь, где ключ - их позиция (каунсорт алгоритм)
            for(int i = low; i <= high; i++)
            {
                int c = getCharFromPos(str[i], d);
                count[c]--;
                temp.Add(high - count[c], str[i]);
            }

            printDict(temp);

            for (int i = low; i <= high; i++)
                str[i] = temp[i];

            printArr(str, str.Length);

            for (int i = 0; i < 256; i++)
                sortMSD(str, high - count[i + 1] + 1, high - count[i], d + 1);
        }

        static void printArr(string[] str, int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(str[i] + " ");
            Console.WriteLine();
        }

        static void printDict(Dictionary<int, string> dict)
        {
            foreach(KeyValuePair<int, string> kvp in dict)
            {
                Console.WriteLine(kvp.Key + " - " + kvp.Value);
            }
        }

        static void Main(string[] args)
        {
            string[] str = { "X1234DG", "X1234HG", "A5643JK", "A3483QW", "K7878TF", "K7878TG", "X1234DF", "M5621HG" };
            int n = str.Length;

            Console.Write("{0, 12}", "Unsorted: ");
            printArr(str, n);

            sortMSD(str, 0, n - 1, 0);

            Console.Write("{0, 12}", "Sorted: ");
            printArr(str, n);
        }
    }
}
