using static System.Console;

Write("quante uova ci sono ? ");

string? input = ReadLine();

if (int.TryParse(input, out int valore))
{
    Write($"ci sono {valore} uova");
}
else {
    Write("Non sono stato in grado di convertire l'input");
}
    
    
