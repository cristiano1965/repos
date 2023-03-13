using System.Globalization;
using static System.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

RunFactorial();

static void RunFactorial() {
    for (int i = 1; i <= 14; i++) {
        try
        {
            WriteLine($"{i}! = {Factorial(i):N0}");
        }
        catch (System.OverflowException) {
            WriteLine($"{i} è troppo grande per un intero a 32 bit");
        }
    }
}

static int Factorial(int number) {

    if (number < 1)
    {
        return 0;
    }
    else if (number == 1)
    {

        return 1;
    }

    else { 
        checked // per overflow
        { 
            return number * Factorial(number - 1);
        }
    }
}