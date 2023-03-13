/*
object height = 1.88; // memorizza un double nell'oggetto
object name = "Amir"; // memorizza una stringa nell'oggetto
Console.WriteLine($"{name} is {height} metres tall");

// int lenght1 = name.Length; // da errore di compilazione
int length2 = ((string)name).Length; // dice al compilatore che si tratta di una stringa
Console.WriteLine($"{name} has {length2} characters");
*/

/*
//memorizzare una stringa in un oggetto dynamic
// la stringa ha una proprietà chiamata Length
dynamic something = "Ahmed";

// int non ha la prorietà Length
 something = 12;

// un array di qualsiasi tipo ha la proprietà Length
something = new [] {3, 5, 7};

// questo si compila ma emetterà una eccezione di run-time se successivamente ci memorizziamo un tipo di dati che non ha la proprietà Length
Console.WriteLine($"Length is {something.Length}");
*/
using System.Xml;
/*
int population = 66_000_000; // 66 milioni in UK
double weight = 1.88; // in Kg
decimal price = 4.99M; // in sterline
string fruit = "Apples"; // le stringhe usano i doppi apici
char letter = 'Z'; // i singoli caratteri usano apici singoli
bool happy = true; // i booleani hanno il valore true oppure false
*/

// equivale a quanto sotto in base a ciò che viene impostato dopo =
var population = 66_000_000; // 66 milioni in UK
var weight = 1.88; // in Kg
var price = 4.99M; // in sterline
var fruit = "Apples"; // le stringhe usano i doppi apici
var letter = 'Z'; // i singoli caratteri usano apici singoli
var happy = true; // i booleani hanno il valore true oppure false

//buon uso di var perchè evita di ripetere il tipo, come mostrato nella più descrittiva seconda riga
var xml1 = new XmlDocument();
XmlDocument xml2 = new XmlDocument();

// cattivo uso di var perchè non possiamo dichiarare il tipo
// in tal caso dovremmo usare una dichiarazione specifica come mostrato nella seconda istruzione
var file1 = File.CreateText("something1.txt");
StreamWriter file2= File.CreateText("something2.txt");

string[] names; // inizializza l'array di stringhe di qualsiasi dimensione

// alloca memoria per quatrto stringe da inserire nell'array
names = new string[4];
string[] names2 = new[] { "pippo", "pluto", "paperino" };

// memorizza i valori alle posizioni dell'indice
names[0] = "Kate";
names[1] = "Jack";
names[2] = "Rebecca";
names[3] = "Tom";

//cicla tra i nomi
for (int i = 0; i < names.Length; i++) {
    // scrive il nome alla posizione dell'indice
    Console.WriteLine($"[{i}]= {names[i]}");
}
Console.WriteLine("\n");
//cicla tra i nomi
for (int i = 0; i < names2.Length; i++)
{
    // scrive il nome alla posizione dell'indice
    Console.WriteLine($"[{i}]= {names2[i]}");
}


