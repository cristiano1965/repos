using static System.Console;

string[] names = new[] { "Michael", "Pam", "Jim", "Dwight", "Angela", "Kevin", "Toby", "Creed" };
WriteLine("Writing queries");

/*
WriteLine("Deferred execution");

// usa LINQ per vedere which names end with "M" ?
var query1 = names.Where(name => name.EndsWith("m"));   // esecuzione differita fino al momento in cui si chiede di estrarre i dati da query1

// usa LINQ query comprehensive syntax per fare la stessa cosa di cui sopra
var query2 = from name in names where name.EndsWith("m") select name; // esecuzione differita fino al momento in cui si chiede di estrarre i dati da query2

// Risposta ritornata come array di stringhe contenente Pam e Jim
string[] result1 = query1.ToArray(); //ora estrae veramente i dati

List<string> result2 = query2.ToList(); //ora estrae veramente i dati

foreach(string name in query1) // ora estrae i dati appena enumeriamo i risultati
{
    WriteLine(name); // estrae Pam
    names[2] = "Jimmy"; // change Jim to Jimmy
    // e quindi al secondo giro Jimmy non termina più con la "m"
}
*/
// in questo esempio LINQ passiamo un "metodo delegato" alla Where, dove ogni elemento viene passato alla funzione che restituisce un bool e se vero, viene reso disponibile in item 
var query = names.Where(new Func<string, bool>(NameLongerThanFour));
var query1 = names.Where(NameLongerThanFour); // stesso risultato perchè il compilatore può instanziare direttamente il metodo al posto nostro 
var query2 = names.Where(n => n.Length > 4)      // stessa cosa usando l'operatore lambda: n = nome del parametro di input, n.lenght > 4 = espressione
                  .OrderBy(n => n.Length)        // ordinati per lunghezza nome
                  .ThenByDescending(n => n);     //  a parità di lunghezza per nome descend;  

foreach (string item in query) // ora estrae i dati appena enumeriamo i risultati
{
    WriteLine(item); 
}
WriteLine();
foreach (string item in query1) // ora estrae i dati appena enumeriamo i risultati
{
    WriteLine(item);
}
WriteLine();
WriteLine("Questi sono ordinati per lunghezza nome / Descend nome");
foreach (string item in query2) // ora estrae i dati appena enumeriamo i risultati
{
    WriteLine(item);
}


/// /
WriteLine();
WriteLine("Filtering by type");

List<Exception> exceptions = new()
{
    new ArgumentException(),
    new SystemException(),
    new IndexOutOfRangeException(),
    new InvalidOperationException(),
    new NullReferenceException(),
    new InvalidCastException(),
    new OverflowException(),
    new DivideByZeroException(),
    new ApplicationException()
};

// rimuoviamo le eccezioni che non sono aritmetiche scrivendo solo quelle aritmetiche
IEnumerable<ArithmeticException> queryEcc = exceptions.OfType<ArithmeticException>();

foreach (ArithmeticException excp in queryEcc)
{
    WriteLine(excp);
}



static bool NameLongerThanFour(string name)
{
    return name.Length > 4;
}