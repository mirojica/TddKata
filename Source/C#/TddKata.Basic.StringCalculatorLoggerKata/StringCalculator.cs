using System;
using System.Collections.Generic;
using System.Linq;

namespace TddKata.Basic.StringCalculatorWithColaboratorsKata
{
    public class StringCalculator
    {
        public int Add(string stringNumbers)
        {
            if (string.IsNullOrEmpty(stringNumbers))
            {
                return 0;
            }

            var delimiters = GetDefaultDelimiters();

            if (ContainsCustomeDelimitersIn(stringNumbers))
            {
                delimiters = ExtractCustomeDelimitersFrom(stringNumbers);

                stringNumbers = RemoveDelimiterPartFrom(stringNumbers);
            }
            var numbers = ExtractNumbersLowerOrEqualsThanThousandFrom(stringNumbers, delimiters);
            var negativeNumbers = ExtractNegativeNumbersFrom(numbers);

            if (negativeNumbers.Any())
            {
                throw new Exception($"negatives not allowed: {string.Join(", ", negativeNumbers)}");
            }

            return numbers.Sum();
        }

        private static List<int> ExtractNegativeNumbersFrom(List<int> numbers)
        {
            return numbers.Where(number => number < 0).ToList();
        }

        private static List<int> ExtractNumbersLowerOrEqualsThanThousandFrom(string stringNumbers, string[] delimiters)
        {
            return stringNumbers.Split(delimiters, StringSplitOptions.None)
                                        .Select(int.Parse).Where(number => number <= 1000).ToList();
        }

        private static string[] GetDefaultDelimiters()
        {
            return new[] { ",", "\n" };
        }

        private static string RemoveDelimiterPartFrom(string stringNumbers)
        {
            return stringNumbers.Remove(0, stringNumbers.IndexOf("\n", StringComparison.Ordinal) + 1);
        }

        private static string[] ExtractCustomeDelimitersFrom(string stringNumbers)
        {
            return stringNumbers.StartsWith("//[") ?
                stringNumbers.Split('[', ']').Skip(1).ToArray() : new[] { stringNumbers.Split('\n').First().Last().ToString() };
        }

        private static bool ContainsCustomeDelimitersIn(string stringNumbers)
        {
            return stringNumbers.StartsWith("//");
        }
    }
}