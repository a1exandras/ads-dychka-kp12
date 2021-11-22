using System;
using static System.Console;
using static System.Math;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, y, z, a = 0, b = 0;
            Write("Input x, y and z:");
            x = Convert.ToDouble(ReadLine());
            y = Convert.ToDouble(ReadLine());
            z = Convert.ToDouble(ReadLine());
            if (Abs(y - x) != Exp(-2) && Abs(x + z) > 0 && Abs(y - x) > 0)
            {
                a = Log10(Abs(x + z)) / (1 + Log(Abs(y - x)) / 2) + 2 * y;
                WriteLine($"a = {a}");
                if (a > -z && a != 0)
                {
                    b = Log(a + z) / Pow(a, 2) + Pow(x, -a);
                    WriteLine($"b = {b}");
                }
                else
                    WriteLine("Wrong Input");
            }
            else
                WriteLine("Wrong Input");
            ReadLine();
        }
    }
}
