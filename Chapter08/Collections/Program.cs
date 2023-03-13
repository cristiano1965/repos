using static System.Console;

//WorkingWithLists();
WorkingWithDictionaries();

static void Output(string title, IEnumerable<string> collection) 
{ 
    WriteLine(title);

    foreach(string item in collection) 
    {
        WriteLine($"   {item}");
    }
}

static void WorkingWithLists() 
{
    // sintassi semplice per definire una lista ed inserirci tre elementi
    List<string> cities = new();
    cities.Add("London");
    cities.Add("Paris");
    cities.Add("Milan");

    // sinstassi alternativa che viene però convertita dal compilatore nel tre add() di cui sopra
    /*
    List<string> cities = new()
    { "London", "Paris", "Milan"}
    ;
    */

    /* altra alternativa che passa un array di valori stringa ad metodo AddRange
     *
    List<string> cities = new();
    cities.AddRange(new[] { "London", "Paris", "Milan"});
     */

    Output("initial List", cities);

    WriteLine($"The first city is {cities[0]}");
    WriteLine($"The lasty city is {cities[cities.Count -1]}");

    cities.Insert(0, "Sydney");

    Output("Dopo aver inserito Sidney alla posizione 0", cities);

    cities.RemoveAt(1);
    cities.Remove("Milan");

    Output("Dopo aver rimosso due città", cities);

}

static void WorkingWithDictionaries() 
{
    Dictionary<string, string> keywords = new();

    //add using named parameter
    keywords.Add(key: "int", value: "tipo di dato intero a 32-bit");

    //add using positional parameters
    keywords.Add("long", "tipo di dato intero a 64-bit");
    keywords.Add("float", "tipo di dato decimale a singola precisione");

    /* metodo alternativo convertito dal compilatore in chiamate al metodo Add()
    Dictionary<string, string> keywords = new()
    {
        { "int", "tipo di dato intero a 32-bit" },
        { "long", "tipo di dato intero a 64-bit" },
        { "float", "tipo di dato decimale a singola precisione" }

    }
    ;
    */

    /* altro metodo alternativo convertito dal compilatore in chiamate al metodo Add()
    Dictionary<string, string> keywords = new()
    {
        ["int"] = "tipo di dato intero a 32-bit" ,
        ["long"] = "tipo di dato intero a 64-bit" ,
        ["float"] = "tipo di dato decimale a singola precisione"

    }
    ;
    */

    Output("Chiavi del dizionario", keywords.Keys);
    Output("Valori del dizionario", keywords.Values);

    WriteLine("Chiavi e definizioni");
    foreach (KeyValuePair<string, string> elemento in keywords) 
    {
        WriteLine($"  {elemento.Key}, {elemento.Value}");
    }

    // ricerca valore usando una chiave
    string key = "long";
    WriteLine($"Il valore della chiave '{key}' è '{keywords[key]}'");






}
