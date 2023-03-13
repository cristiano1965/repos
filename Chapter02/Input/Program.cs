using static System.Console;

Write("Digita il tuo nome e premi ENTER: ");
string? nome = ReadLine();      //il ? vicino al tipo indica che la variabile potrebbe contenere un valore nullo

Write("Digita la tua età e premi ENTER: ");
string? age = ReadLine();

WriteLine($"Ciao {nome}, appari bene per la tua età di {age} anni !");

Write("Press any key combination: ");
ConsoleKeyInfo key = ReadKey();
WriteLine();
WriteLine("Key: {0}, Char: {1}, Modifiers: {2}",
    arg0: key.Key,
    arg1: key.KeyChar,
    arg2: key.Modifiers);
