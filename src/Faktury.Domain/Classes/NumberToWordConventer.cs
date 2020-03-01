using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Faktury.Domain.Classes
{
    public class NumberToWordConventer
    {
        private static readonly string[] Months =
            {
                "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"
            };

        private static readonly string[] Units =
            {
                "Zero", "Jeden", "Dwa", "Trzy", "Cztery", "Pięć", "Sześć",
                "Siedem", "Osiem", "Dziewięć", "Dziesięć", "Jedenaście",
                "Dwanaście", "Trzynaście", "Czternaście", "Piętnaście",
                "Szesnaście", "Siedemnaście", "Osiemnaście", "Dziewiętnaście"
            };

        private static readonly string[] Tens =
            {
                "Dwadzieścia", "Trzydzieści", "Czterdzieści", "Pięćdziesiąt",
                "Sześćdziesiąt", "Siedemdziesiąt", "Osiemdziesiąt",
                "Dziewięćdziesiąt"
            };

        private static readonly string[] Hundreds =
            {
                "", "Sto", "Dwieście", "Trzysta", "Czterysta", "Pięćset",
                "Sześćset", "Siedemset", "Osiemset", "Dziewięćset"
            };

        private static readonly string[,] OtherUnits =
            {
                { "Jeden Tysiąc", "Tysiące", "Tysięcy"     },
                { "Milion", "Miliony", "Milionów"    },
                { "Miliard", "Miliardy", "Miliardów" }
            };

        private static readonly string[] MoneyUpperUnits =
            {
                "Złoty", "Złote", "Złotych"
            };

        private static readonly string[] MoneyLowerUnits =
            {
                "Grosz", "Grosze", "Groszy"
            };

        private static string SmallValueToWords(int n)
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
        private static int GetBigUnitIndex(long n)
        {
            if (n == 1) return 0;

            n %= 100;
            int lastDigit = (int) n % 10;
            if ((n >= 10 && (n <= 20 || lastDigit == 0)) || (lastDigit > 4) || lastDigit <= 1)
            {
                return 2;
            }

            return 1;
        }

        private static long ToWords(StringBuilder valueInWords, long n, int level)
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


        private static string ToWords(long value)
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

        public static string ConvertValues(decimal value)
        {
            string stringVal = value.ToString(CultureInfo.InvariantCulture);
            string[] data = { stringVal };
            if (stringVal.Contains('.'))
            {
                data = stringVal.Split('.');
            }
            else if (stringVal.Contains(','))
            {
                data = stringVal.Split(',');
            }   

            stringVal = ConvertUpperMoney(long.Parse(data[0]));
            if (data.Length > 1)
            {
                if (data[1].Length == 1)
                {
                    data[1] += "0";
                }
                stringVal += " " + ConvertLowerMoney(int.Parse(data[1]));
            }
            else
            {
                stringVal += " " + ConvertLowerMoney(0);
            }

            return stringVal;

        }

        private static string ConvertUpperMoney(long value)
        {
            var result = ToWords(value) + " " + MoneyUpperUnits[GetMoneyEndingIndex(Convert.ToInt32(value))];
            return result;
        }

        private static string ConvertLowerMoney(int value)
        {
            var result = ToWords(value) + " " + MoneyLowerUnits[GetMoneyEndingIndex(value)];
            return result;
        }

        private static int GetMoneyEndingIndex(int value)
        {
            int tenths = (value % 100);
            if (tenths > 10)
            {
                tenths -= (tenths % 10);
                tenths /= 10;
            }
            else tenths = 0;

            if (tenths != 1)
            {
                switch (value % 10)
                {
                    case 1:

                        if (tenths == 0 && value < 100)
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

            return 2;

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
            if (!int.TryParse(value, out var Out))
            {
                for (int i = 0; i < Months.Length; i++)
                {
                    if (String.Equals(Months[i], value, StringComparison.CurrentCultureIgnoreCase)) return i + 1;
                }
            }
            return Out;
        }
    }
}
