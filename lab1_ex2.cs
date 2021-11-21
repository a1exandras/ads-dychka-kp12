using System;
using static System.Console;

namespace Ex2
{
    class Program
    {
        static void Main(string[] args)
        {
            int d1, d2, m1, m2, y1, y2, years = 0, days = 0, counter = 0;
            int[] months = new int[] {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
            Write("Input year 1: "); y1 = Convert.ToInt32(ReadLine());
            Write("Input year 2: "); y2 = Convert.ToInt32(ReadLine());
            while (true)
            {
                Write("Input month 1: "); m1 = Convert.ToInt32(ReadLine());
                if (m1 <= 12)
                    break;
            }
            while (true)
            {
                Write("Input month 2: "); m2 = Convert.ToInt32(ReadLine());
                if (m2 <= 12)
                    break;
            }
            while (true)
            {
                Write("Input day 1: "); d1 = Convert.ToInt32(ReadLine());
                if (d1 <= months[m1 - 1])
                    break;
            }
            while (true)
            {
                Write("Input day 2: "); d2 = Convert.ToInt32(ReadLine());
                if (d2 <= months[m2 - 1])
                    break;
            }
            //main
            if (y1 < y2)
            {
                for (int i = 0; i < m1; i++)
                {
                    if (i == 1)
                    {
                        if (y1 % 400 == 0)
                            d1++;
                        else if (y1 % 100 != 0 && y1 % 4 == 0)
                            d1++;
                    }
                    d1 += months[i];
                }
                for (int i = 0; i < m2; i++)
                {
                    if (i == 1)
                    {
                        if (y2 % 400 == 0)
                            d2++;
                        else if (y2 % 100 != 0 && y2 % 4 == 0)
                            d2++;
                    }
                    d2 += months[i];
                }
                for (int i = y1; i <= y2; i++)
                {
                    if (i % 400 == 0)
                    {
                        counter++;
                    }
                    else if (i % 100 != 0 && i % 4 == 0)
                    {
                        counter++;
                    }
                }
                d2 += (y2 - y1) * 365 + counter;
            }
            else if (y2 < y1)
            {
                for (int i = 0; i < m2; i++)
                {
                    if (i == 1)
                    {
                        if (y2 % 400 == 0)
                            d2++;
                        else if (y2 % 100 != 0 && y2 % 4 == 0)
                            d2++;
                    }
                    d2 += months[i];
                }
                for (int i = 0; i < m1; i++)
                {
                    if (i == 1)
                    {
                        if (y1 % 400 == 0)
                            d1++;
                        else if (y1 % 100 != 0 && y1 % 4 == 0)
                            d1++;
                    }
                    d1 += months[i];
                }
                for (int i = y2; i <= y1; i++)
                {
                    if (i % 400 == 0)
                    {
                        counter++;
                    }
                    else if (i % 100 != 0 && i % 4 == 0)
                    {
                        counter++;
                    }
                }
                d1 += (y1 - y2) * 365 + counter;
            }
            else
            {
                for (int i = 0; i < m2; i++)
                {
                    if (i == 1)
                    {
                        if (y2 % 400 == 0)
                            d2++;
                        else if (y2 % 100 != 0 && y2 % 4 == 0)
                            d2++;
                    }
                    d2 += months[i];
                }
                for (int i = 0; i < m1; i++)
                {
                    if (i == 1)
                    {
                        if (y1 % 400 == 0)
                            d1++;
                        else if (y1 % 100 != 0 && y1 % 4 == 0)
                            d1++;
                    }
                    d1 += months[i];
                } 
            }
            days = Math.Abs(d2 - d1);
            days -= counter;
            years = days / 365;
            days = days % 365;
            WriteLine($"{years} years,  {days} days");
        }
    }
}
