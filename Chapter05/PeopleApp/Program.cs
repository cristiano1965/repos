using Packt.Shared;
using static System.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Person bob = new();

bob.Name = "Bob Smith";
bob.DateOfBirth = new DateTime(1965,12, 22);
bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatuaDiZeusAOlimpia;
bob.BucketList = WondersOfTheAncientWorld.GiardiniPensiliDiBabilonia | WondersOfTheAncientWorld.MausoleoDiAlicarnasso; //in alternativa, ma meno leggibile -> bob.BucketList = (WondersOfTheAncientWorld)18; cioè l'ememento con valore 2 + elemento con valore 16
bob.Children.Add(new() { Name = "Alfred"});
bob.Children.Add(new() { Name = "Ciccio" });

WriteLine($"{bob.Name} was born on {bob.DateOfBirth:dddd, d MMMM yyyy}"); // mercoledì, 22 dicembre 1965
WriteLine($"La meraviglia favorita di {bob.Name} è '{bob.FavoriteAncientWonder}' e la sua posizione è {(int)bob.FavoriteAncientWonder}");
WriteLine($"La dei desideri di {bob.Name} è {bob.BucketList}");
WriteLine($"{bob.Name} é un {Person.Species} ");    //stampa la costante usando la classe e non l'oggetto
WriteLine($"{bob.Name} é nato su {bob.HomePLanet} ");    //campo read-only
Write($"{bob.Name} ha {bob.Children.Count} figli: ");
for (int i = 0; i < bob.Children.Count; i++) {
    Write($"{bob.Children[i].Name}, ");
}

WriteLine();

BankAccount.InterestRate = 0.12M; // valore condiviso da qualsiasi istanza di BankAccount

BankAccount jonesAccount = new();

jonesAccount.AccountName = "Mrs. Jones";
jonesAccount.Balance = 2400;
WriteLine($"{jonesAccount.AccountName} ha guadagnato {jonesAccount.Balance * BankAccount.InterestRate:C} di interessi");

BankAccount gerrierAccount = new();
gerrierAccount.AccountName = "Mr. Gerrier";
gerrierAccount.Balance = 98;
WriteLine($"{gerrierAccount.AccountName} ha guadagnato {gerrierAccount.Balance * BankAccount.InterestRate:C} di interessi");

Person personaVuota = new();
WriteLine($"{personaVuota.Name} nato su {personaVuota.HomePLanet} é stato creato il {personaVuota.Istanziato:hh:mm:ss} di {personaVuota.Istanziato:dddd}");

Person gunny = new("Gunny", "Marte");
WriteLine($"{gunny.Name} nato su {gunny.HomePLanet} é stato creato il {gunny.Istanziato:hh:mm:ss} di {gunny.Istanziato:dddd}");

bob.WriteToConsole();
WriteLine(bob.GetOrigin());

(string, int) frutta = bob.GetFruit();
WriteLine($"I frutti di {bob.Name} sono: {frutta.Item1}, {frutta.Item2}");
var fruttaNominata = bob.GetNamedFruit();
WriteLine($"I frutti di {bob.Name} sono: {fruttaNominata.Name} e {fruttaNominata.Number}");

//tipologia campi della tupla determinata dai valori di inizializzazione
var thing1 = ("Neville", 4);
WriteLine($"{thing1.Item1} ha {thing1.Item2} figli");

//tipologia e nomi dei campi della tupla determinata da nomi dei campi passati alla tupla
var thing2 = (bob.Name, bob.Children.Count);
WriteLine($"{thing2.Name} ha {thing2.Count} figli");

//ritorno valoro di una tupla in una variabile con due campi
(string IlNome, int IlNumero) fruttaBob = bob.GetNamedFruit();
WriteLine($"frutta di {bob.Name}: {fruttaBob.IlNome} e {fruttaBob.IlNumero}");

//ritorno valoro di una tupla in due variabili separate
(string NomeFrutto, int NumeroFrutto)  = bob.GetNamedFruit();
WriteLine($"frutta di {bob.Name}: {NomeFrutto} e {NumeroFrutto}");

//decostruire bob
var (name1, nascita1) = bob;    //qui usa in automatico il decostruttore con le due variabili
WriteLine($"{bob.Name} decostruito (nome e data nascita): {name1}, {nascita1}");
var (name2, nascita2, favoriti2) = bob;    //qui usa in automatico il decostruttore con le tre variabili
WriteLine($"{bob.Name} decostruito (nome, data nascita, meraviglia favorita): {name2}, {nascita2}, {favoriti2}");

WriteLine(bob.SayHello());
WriteLine(bob.SayHello("Cristiano"));

// passaggio parametri opzionale
WriteLine(bob.ParametriOpzionali());
WriteLine(bob.ParametriOpzionali("Salta!",98.5)); //qui passo posizionalmente i primi 2 (perchpè non uso il nome del parametro a cui passare il valore)
WriteLine(bob.ParametriOpzionali(active:false, command:"Nascondi!")); //qui passo terzo ed il primo usando il nome del parametro, il secondo lo lascio vuoto 

int a = 10, b = 20, c = 30;
WriteLine($"Prima: a={a}, b={b}, c={c}");
bob.PassareParametri(a, ref b, out c);  //a resta 10 (nel metodo viene incrementata, ma il suo valore viene poi perso) b diventa 21 perchè passato come riferimento e c viene completamente valorizzato dal metodo (per cui si poteva anche non passare)
WriteLine($"Dopo: a={a}, b={b}, c={c}");

// qui usiamo alcune proprietà definite nel partial in PersonAutogen.cs e poi le stampiamo
Person sam = new()
    {
        Name= "Sam",
        DateOfBirth = new(1972, 1, 27)
    };
WriteLine(sam.Origin);
WriteLine(sam.Greeting);
WriteLine(sam.Age);

sam.FavoriteIceCream = "Cioccolato";
WriteLine($"Il gusto di gelato preferito da {sam.Name} è {sam.FavoriteIceCream}");
sam.FavoritePrimaryColor = "Red";
WriteLine($"Il colore primario preferito da {sam.Name} è {sam.FavoritePrimaryColor}");

sam.Children.Add(new() { Name = "Charlie" });
sam.Children.Add(new() { Name = "Ella" });
WriteLine($"Il primo figlio dai Sam è {sam.Children[0].Name}");  //oppure sam[0].Name
WriteLine($"Il secondo figlio dai Sam è {sam.Children[1].Name}"); // oppure sam[1].Name

// definisce un array di oggetti delle diverse tipologie (impostando il campo specifico per ogni tipologia)
object[] passeggeri = { 
    new FirstClassPassenger { AirMiles = 1_419},
    new FirstClassPassenger { AirMiles = 16_562},

    new BusinessClassPassenger(),

    new CoachPassenger { CarryOnKG = 25.7},
    new CoachPassenger { CarryOnKG = 0},
    new CoachPassenger { CarryOnKG = 12},

};

// ora calcola il costo del volo in base alla tipologia del passeggero ed usando il when per discriminarlo in base ad AirMiles (per i prima classe) ed in base ai KG (solo per quelli Coach)
// e visualizza i dettagli 
foreach (object passeggero in passeggeri) {

    /* sintassi C#8
    decimal flightCost = passeggero switch
    {
        FirstClassPassenger p when p.AirMiles > 35000 => 1500M,
        FirstClassPassenger p when p.AirMiles > 15000 => 1750M,
        FirstClassPassenger _                         => 2000M,
        BusinessClassPassenger _                      => 1000M,
        CoachPassenger p when p.CarryOnKG < 10.0      =>  500M,
        CoachPassenger _                              =>  650M
        _                                             =>  800M
    };
    */

    // sintassi C# da 9+
    decimal flightCost = passeggero switch
    {
        FirstClassPassenger p => p.AirMiles switch
        {
            > 35000 => 1500M,
            > 15000 => 1750M,
            _ => 2000M
        },
        BusinessClassPassenger _ => 1000M,
        CoachPassenger p when p.CarryOnKG < 10.0 => 500M,
        CoachPassenger _                         => 650M,
        _                                        => 800M
        
    };
    WriteLine($"Costo volo {flightCost:C} per passeggero {passeggero}");
}
ImmutablePerson jeff = new(){ FirstName="Jeff", LastName="Winger"};

// jeff.FirstName = "Geoff";   // questo da errore in compilazione perchè la prorpietà set del campo è "init" e puà essere inizializzata SOLO con la new() la prima volta

ImmutableVehicle car = new()    // questo è un record, per cui dopo inizializzazione i valori non possono essere più cambiati
{
    Brand = "Mazda MX-5 RF",
    Color = "Soul Red Crystal Metallic",
    Wheels = 4

};

ImmutableVehicle repaintedCar = car //questo copia da car tutti gli attributi, ma imposta un nuovo colore
    with { Color = "Polymetal Grey Metallic" };

WriteLine($"Il colore originale del veicolo era {car.Color}");
WriteLine($"Il nuovo colore del veicolo è {repaintedCar.Color}");

ImmutableAnimal oscar = new("Oscar", "Labrador");
var (who, what) = oscar; //(chiama il deconstruct del record)
WriteLine($"{who} è un {what}");



