using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faktury.Classes
{
    class NumberToWordConventer
    {
            static readonly string[] Months =
            {
                "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"
            };

            static readonly string[] Units =
            {
                "Zero", "Jeden", "Dwa", "Trzy", "Cztery", "Pięć", "Sześć",
                "Siedem", "Osiem", "Dziewięć", "Dziesięć", "Jedenaście",
                "Dwanaście", "Trzynaście", "Czternaście", "Piętnaście",
                "Szesnaście", "Siedemnaście", "Osiemnaście", "Dziewiętnaście"
            };

            static readonly string[] Tens =
            {
                "Dwadzieścia", "Trzydzieści", "Czterdzieści", "Pięćdziesiąt",
                "Sześćdziesiąt", "Siedemdziesiąt", "Osiemdziesiąt",
                "Dziewięćdziesiąt"
            };

            static readonly string[] Hundreds =
            {
                "", "Sto", "Dwieście", "Trzysta", "Czterysta", "Pięćset",
                "Sześćset", "Siedemset", "Osiemset", "Dziewięćset"
            };

            static readonly string[,] OtherUnits =
            {
                { "Jeden Tysiąc", "Tysiące", "Tysięcy"     },
                { "Milion", "Miliony", "Milionów"    },
                { "Miliard", "Miliardy", "Miliardów" }
            };

            static readonly string[] MoneyUpperUnits =
            {
                "Złoty", "Złote", "Złotych"
            };

            static readonly string[] MoneyLowerUnits =
            {
                "Grosz", "Grosze", "Groszy"
            };

        static string SmallValueToWords(int n)
        {
            if (n == 0)
            {
                return String.Empty;
            }
            
            StringBuilder valueInWords = new StringBuilder();

            // Konwertuj setki.
            int temp = n / 100;
            if (temp > 0)
            {
                valueInWords.Append(Hundreds[temp]);
                n -= temp * 100;
            }

            // Konwertuj dziesiątki i jedności.
            if (n > 0)
            {
                if (valueInWords.Length > 0)
                    valueInWords.Append(" ");
                if (n < 20)
                {
                    valueInWords.Append(Units[n]);
                }
                else
                {
                    valueInWords.Append(Tens[(n / 10) - 2]);
                    int lastDigit = n % 10;
                    if (lastDigit > 0)
                    {
                        valueInWords.Append(" ");
                        valueInWords.Append(Units[lastDigit]);
                    }
                }
            }

            return valueInWords.ToString();
        }


        /// <summary>
        /// Tysiąc, tysiące lub tysięcy
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static int GetBigUnitIndex(long n)
        {
            if (n == 1) return 0;

            n = n % 100;
            int lastDigit = (int) n % 10;
            if ((n >= 10 && (n <= 20 || lastDigit == 0)) || (lastDigit > 4) || lastDigit <= 1)
            {
                return 2;
            }

            return 1;
        }

        static long ToWords(StringBuilder valueInWords, long n, int level)
        {
            int smallValue = 0;
            long divisor = (long) Math.Pow(1000, level + 1);
            if (divisor <= n)
            {
                n = ToWords(valueInWords, n, level + 1);
                smallValue = (int) (n / divisor);
                if (valueInWords.Length > 0)
                {
                    valueInWords.Append(" ");
                }
                if (smallValue > 1)
                {
                    
                    valueInWords.Append(SmallValueToWords(smallValue));
                    if (n != 1000) valueInWords.Append(" ");
                    else valueInWords.Append(" Zero ");
                }
                valueInWords.Append(OtherUnits[level, GetBigUnitIndex(smallValue)]);
            }
            return n - smallValue * divisor;
        }


        public static string ToWords(long value)
        {
            if (value == 0)
            {
                return Units[0];
            }

            StringBuilder valueInWords = new StringBuilder();

            int smallValue = (int)ToWords(valueInWords, value, 0);
            if (smallValue > 0)
            {
                if (valueInWords.Length > 0)
                {
                    valueInWords.Append(" ");
                }
                valueInWords.Append(SmallValueToWords(smallValue));
            }
            return valueInWords.ToString();
        }

        public static string ConvertValues(float value)
        {
            string StringVal = value.ToString();
            string[] Data = { StringVal };
            if (StringVal.Contains('.'))
            {
                Data = StringVal.Split('.');
            }
            else if (StringVal.Contains(','))
            {
                Data = StringVal.Split(',');
            }   

            StringVal = ConvertUpperMoney(long.Parse(Data[0]));
            if (Data.Length > 1)
            {
                if (Data[1].Length == 1)
                {
                    Data[1] += "0";
                }
                StringVal += " " + ConvertLowerMoney(int.Parse(Data[1]));
            }
            else
            {
                StringVal += " " + ConvertLowerMoney(0);
            }

            return StringVal;

        }

        public static string ConvertUpperMoney(long value)
        {
            string Result = "";

                Result = ToWords(value) + " " + MoneyUpperUnits[GetMoneyEndingIndex(Convert.ToInt32(value))];

            return Result;
        }

        public static string ConvertLowerMoney(int value)
        {
            string Result = "";

                Result = ToWords(value) + " " + MoneyLowerUnits[GetMoneyEndingIndex(value)];

            return Result;
        }

        public static int GetMoneyEndingIndex(int value)
        {
            int Dziesiatki = (value % 100);
            if (Dziesiatki > 10)
            {
                Dziesiatki = Dziesiatki - (Dziesiatki % 10);
                Dziesiatki = Dziesiatki / 10;
            }
            else Dziesiatki = 0;

            if (Dziesiatki != 1)
            {
                switch (value % 10)
                {
                    case 1:

                        if (Dziesiatki == 0 && value < 100)
                            return 0;
                        else return 2;    
                    case 2:
                        return 1;
                    case 3:
                        return 1;
                    case 4:
                        return 1;
                    default:
                        return 2;
                }
            }
            else return 2;

        }

        public static string ConvertNumberToMonth(string value)
        {
            try
            {
                return Months[Convert.ToInt32(value) -1];
            }
            catch
            {
                return "Exception!";
            }
        }

        public static int ConvertMonthToNumber(string value)
        {
            int Out = 0;
            if (!int.TryParse(value, out Out))
            {
                for (int i = 0; i < Months.Length; i++)
                {
                    if (Months[i].ToLower() == value.ToLower()) return i + 1;
                }
            }
            return Out;
        }
    }
}
