using System.Linq;

namespace Faktury.Domain.Classes
{
    public class NipValidator
    {
        private static readonly int[] Factors = {6, 5, 7, 2, 3, 4, 5, 6, 7};

        public static bool IsNipValid(string x)
        {
            if (string.IsNullOrWhiteSpace(x))
            {
                return false;
            }
            var digits = x.Where(char.IsDigit).Select(c => (int)char.GetNumericValue(c)).ToArray();
            if (digits.Length != Factors.Length + 1)
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < Factors.Length; i++)
            {
                sum += digits[i] * Factors[i];
            }

            return digits[9] == sum % 11;

        }
    }
}