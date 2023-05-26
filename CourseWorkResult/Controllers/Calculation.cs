using CourseWorkResult.Models;
using System;

namespace CourseWorkResult.Controllers
{
    static class Calculation
    {
        public static CalculationResult Calculate(CalculationData data)
        {
            decimal Square, LeftoverSquare;
            int Amount = 0, CountOfPackages, ResultPrice, Leftover = 0;
            int length = (int)Math.Round(data.LengthRoom * 1000 - 2 * data.Indent, MidpointRounding.AwayFromZero);
            int width = (int)Math.Round(data.WidthRoom * 1000 - 2 * data.Indent, MidpointRounding.AwayFromZero);
            int i = 0, j = 0, temp = 0;

            while (i < width)
            {
                while (j < length)
                {
                    if (j == 0 && temp >= data.MinLength && data.MinLength != 0)
                    {
                        j += temp;
                        temp = 0;
                        continue;
                    }

                    if (j + data.Laminate.Length > length)
                    {
                        int last = length - j;

                        if (last >= data.MinLength)
                        {
                            j += last;
                            temp = data.Laminate.Length - last;
                        }
                        else
                        {
                            temp = length - j;
                            j += temp;
                            temp = length - data.MinLength;
                        }

                        if (temp < data.MinLength)
                        {
                            Leftover++;
                        }
                    }
                    else
                    {
                        j += data.Laminate.Length;
                    }

                    Amount++;
                }

                i += data.Laminate.Width;
                j = 0;
            }

            Square = Amount * data.Laminate.Length * data.Laminate.Width / 1000000.0m;
            LeftoverSquare = Leftover * data.Laminate.Length * data.Laminate.Width / 1000000.0m;
            CountOfPackages = (int)Math.Ceiling(Amount * 1.0d / data.Laminate.Amount);
            ResultPrice = data.Laminate.Price * CountOfPackages;
            return new CalculationResult(Square, Amount, CountOfPackages, ResultPrice, Leftover, LeftoverSquare);
        }
    }
}
