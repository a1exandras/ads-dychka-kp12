using System;
using static System.Console;

namespace mainEX
{
    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            int m = 10, n = 9, k = 7;//рандомні числа

            var matrix = generateMatrix(m, n);

            WriteLine("Unsorted Matrix: ");
            printMatrix(matrix);

            for (int j = 0; j < n; j++)
                if (toSort(k, j + 1))
                    matrix = sortColumn(matrix, j);

            WriteLine("Sorted Matrix: ");
            printMatrix(matrix);

            ReadLine();
        }

        static int[,] sortColumn(int[,] matrix, int col)
        {
            int m = matrix.GetLength(0);

            for (int l = 0; l < m; l++)
                for (int i = 0; i < m - 1; i++)
                    if (matrix[i, col] > matrix[i + 1, col])
                    {
                        var temp = matrix[i, col];
                        matrix[i, col] = matrix[i + 1, col];
                        matrix[i + 1, col] = temp;
                    }

            return matrix;
        }

        static bool toSort(int k, int col)
        {
            int a = col, b = k;
            bool toSort = false;

            while(a != b)
            {
                if (b > a)
                    b -= a;
                else
                    a -= b;
            }

            if ((col * k / a) % 2 == 1)
                toSort = true;

            return toSort;
        }

        static int[,] generateMatrix(int m, int n)
        {
            int[,] matrix = new int[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = rnd.Next(100);

            return matrix;
        }

        static void printMatrix(int[,] matrix)
        {
            int m = matrix.GetLength(0), n = matrix.GetLength(1);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    Write($"{matrix[i, j],3}");

                WriteLine();
            }
        }
    }
}
