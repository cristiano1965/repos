using System.Globalization;
using static System.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

RunCardinalToOrdinal();

/// <summary>
/// pass a 32-bit integer and it will be converted into its ordinal equivalent. 
/// </summary>
/// <param name="number">Number is a cardinal value e.g. 1, 2, 3 and so on.</param>
/// <returns>Number as ordinak value e.g 1st, 2nd, 3rd, and so on.</returns>
static string CardinalToOrdinal(int number) {

    switch (number) {

        case 11: // casi speciali da 11 a 13
        case 12:
        case 13:
            return $"{number}th";
        default:
            int lastDigit = number % 10;

            string suffix = lastDigit switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
            return $"{number}{suffix}";
    }
} 

static void RunCardinalToOrdinal() {
    for (int number = 1; number <= 40; number++) {
    
        Write($"{CardinalToOrdinal(number)} ");
    }
    WriteLine();
}
