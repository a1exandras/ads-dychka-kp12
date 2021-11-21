using System;
using static System.Console;
using static System.Math;

namespace ADS_lab2
{
    class Program
    {
        static int[,] generateMatrix(int N)
        {
            Random rnd = new Random();

            int[,] matrix = new int[N, N];
            //вводимо змінну для наглядності
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    matrix[i, j] = rnd.Next(1, 100);
            return matrix;
        }
        
        static void printMatrix(int[,] matrix)
        {
            //матриця квадратна, достатньо одного значення
            int N = matrix.GetLength(0);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Write("{0, 5}", matrix[i, j]);
                WriteLine();
            }
            WriteLine();
        }

        static void Main(string[] args)
        {
            //за умови N = M, тому ввожу лише одне значення
            int N;
            //просимо ввести N, доки воно буде непарне
            while (true)
            {
                Write("Input size of matrix:"); N = Convert.ToInt32(ReadLine());
                if (N % 2 == 1)
                    break;
            }
            int[,] matrix = generateMatrix(N);
            printMatrix(matrix);

            //основна частина завдання
            int koeff = -1;
            int i = N / 2, j = N / 2;
            int maxEl = int.MinValue, maxCol = 0, maxRow = 0;
            for (int counter = 1;  counter <= N; counter++)
            {
                for (int k = 0; k < counter; k++)
                {
                    Write(matrix[i, j] + ", ");
                    if (matrix[i, j] > maxEl)
                    {
                        maxEl = matrix[i, j];
                        maxCol = j;
                        maxRow = i;
                    }
                    j += koeff;
                }
                if (counter == N)
                    break;
                for (int k = 0; k < counter; k++)
                {
                    Write(matrix[i, j] + ", ");
                    if (matrix[i, j] > maxEl)
                    {
                        maxEl = matrix[i, j];
                        maxCol = j;
                        maxRow = i;
                    }
                    i += koeff;
                }
                koeff *= -1;
            }
            Write($"\nmaxEl = {maxEl} ");
            if (maxCol > maxRow)
                Write("above main diagonal");
            else if (maxCol < maxRow)
                Write("below main diagonal");
            else
                Write("on the main diagonal");
        }
    }
}
