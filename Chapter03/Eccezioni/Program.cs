using static System.Console;

Write("Quanti anni hai ? ");

string? input = ReadLine();

try
{   
    int eta = int.Parse(input);
    WriteLine($"Hai {eta} anni");

}
catch (OverflowException)
{
    WriteLine("L'età inserita è troppo grande o troppo piccola");
}
catch (FormatException) //generata anche quando input è null !!!
{
    WriteLine("L'età inserita non è nel formato valido");
}
catch (Exception ex)
{
    WriteLine($"{ex.GetType()} dice: {ex.Message}");
}

Write("Inserisci un importo con un decimale ? ");

string? input2 = ReadLine();

try
{
    decimal importo = decimal.Parse(input2);
    if (input2.Contains("."))
        WriteLine("inserire l'importo con la ',' per separare i decimali");
    else
        WriteLine($"Hai inserito {importo}");

}

catch (FormatException) when (input2.Contains("$") || input2.Contains("€"))
{
    WriteLine("inserire l'importo senza il simobolo della valuta");
}


catch (FormatException) //generata anche quando input è null !!!
{
    WriteLine("L'importo deve contenere solo numeri");
}
catch (Exception ex)
{
    WriteLine($"{ex.GetType()} dice: {ex.Message}");
}

