using NumeriPrimiLib;
using static System.Console;


Boolean prosegui = true;
int numero = 0;

while (prosegui)
{
    WriteLine("Calcolo dei numeri primi");
    Write("Inserisci un numero da 1 a 1000: ");

    string? input = ReadLine();

    try
    {
        numero = int.Parse(input);
        if (numero > 0 && numero <= 1000)
            prosegui = false;
        else
            WriteLine("Numero non valido !!");
    }
    catch(System.FormatException)
    {
        WriteLine("Numero non valido !!");
    }
    
}

Primi primiObj = new();


WriteLine($"I numeri primi di {numero} sono: {primiObj.GeneraPrimi(numero)}");


