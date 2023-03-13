using static System.Console;
using System.Text.RegularExpressions;

bool ripeti = true;

while (ripeti)
{
    Write("Dimmi la tua età: ");
    string? input = ReadLine();

    /*
     * //@=necessario per disabilitare escape, visto che usiamo la "\" 
     * \d = una cifra numerica
     * ^ = inizio sequenza di escape
     * $ = fine sequenza di escape
     * + = una a più cifre
     */

    Regex ageChecker = new(@"^\d+$"); 

    if (ageChecker.IsMatch(input))
    {
        WriteLine("Grazie !");
        ripeti = false;
    }
    else
    {
        WriteLine($"{input} non è un'età valida.");
    }
}