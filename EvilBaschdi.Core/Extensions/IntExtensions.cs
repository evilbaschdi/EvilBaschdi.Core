namespace EvilBaschdi.Core.Extensions;

/// <summary>
///     Class to extend the functionality of type <see cref="int" />
/// </summary>
// ReSharper disable once UnusedType.Global
public static class IntExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string ToWords(this int number)
    {
        switch (number)
        {
            case 0:
                return "zero";
            case < 0:
                return $"minus {ToWords(Math.Abs(number))}".Trim();
        }

        var words = string.Empty;

        if (number / 1000000 > 0)
        {
            words += $"{ToWords(number / 1000000)} million ";
            number %= 1000000;
        }

        if (number / 1000 > 0)
        {
            words += $"{ToWords(number / 1000)} thousand ";
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            words += $"{ToWords(number / 100)} hundred ";
            number %= 100;
        }

        if (number <= 0)
        {
            return words;
        }

        var unitsMap = new[]
                       {
                           "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen",
                           "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
                       };
        var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        if (number < 20)
        {
            words += unitsMap[number];
        }
        else
        {
            words += tensMap[number / 10];
            if (number % 10 > 0)
            {
                words += $"-{unitsMap[number % 10]}";
            }
        }

        return words.Trim();
    }
}