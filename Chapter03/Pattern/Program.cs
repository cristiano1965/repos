using static System.Console;

string path = @"C:\Users\cris6\source\repos\Chapter03";

Write("Press R for read-only or W fro writeable; ");
ConsoleKeyInfo key = ReadKey();
WriteLine();

Stream? s;

if (key.Key == ConsoleKey.R)
{
    s = File.Open(Path.Combine(path, "file.txt"), FileMode.OpenOrCreate, FileAccess.Read);
}
else {
    s = File.Open(Path.Combine(path, "file.txt"), FileMode.OpenOrCreate, FileAccess.Write);

}


string fs = (s.GetType() == typeof(FileStream) ? "filestream" : (s.GetType() == typeof(MemoryStream) ? "memorystream" : null) );

/*
 
string message;
 
switch (s) {

    case FileStream writeablefile when s.CanWrite:   //definisce la variabile writeablefile di tipo FileStream (che è un sotto-tipo di Stream e può essere un file stream o memory stream)e la confronta il risultato della open richiesta (in read o write) dentro "s"
        message = "Lo stream é un file scrivibile";
        break;
    case FileStream readOnlyFile:
        message = "Lo stream é un file di sola lettura";
        break;
    case MemoryStream ms:
        message = "Lo stream é un file in memoria";
        break;
    default: // sempre valutata per ultima, nonostante la posizione in cui viene definito
        message = "Lo stream é di qualche altro tipo";
        break;
    case null:
        message = "Lo stream é nullo";
        break;
}
*/

// switch come sopra oppure questo, dove non è necessario inserire il "break" e "_" identifica il "default" e cioè che c'è dopo operatore lambda (=>) viene restituito in message
string message = s switch
{
    FileStream writeablefile when s.CanWrite
        => "Lo stream é un file scrivibile",
    FileStream readOnlyFile
        => "Lo stream é un file di sola lettura",
    MemoryStream ms
        => "Lo stream é un file in memoria",
    null
       => "Lo stream é nullo",
    _
       => "Lo stream é di qualche altro tipo"
};


WriteLine(message);
