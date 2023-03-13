using static System.Console;

string name = "Samantha Jones";

// Using substring
int lunghezzaNome = name.IndexOf(" ");
int lunghezzaCognome = name.Length - lunghezzaNome - 1;

string nome = name.Substring(0, lunghezzaNome);
string cognome = name.Substring(lunghezzaNome + 1, lunghezzaCognome);

WriteLine($"Cognome: {cognome}, nome: {nome}");

// usando gli spans: lo span è un sottoinsieme dell'array, come se fonne una finestra che vede un pezzo consecutivo dell'array
ReadOnlySpan<char> nameAsSpan = name.AsSpan();
ReadOnlySpan<char> nomeSpan = nameAsSpan[0..lunghezzaNome];
ReadOnlySpan<char> cognomeSpan = nameAsSpan[(lunghezzaNome+1)..]; //oppure [^lunghezzaCognome..^0]
WriteLine($"Cognome: {cognomeSpan}, nome: {nomeSpan}");




