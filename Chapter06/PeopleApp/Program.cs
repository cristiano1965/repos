using Packt.Shared;
using static System.Console;

Person harry = new() { Name = "Harry" };
Person mary = new() { Name = "Mary" };
Person jill = new() { Name = "Jill" };

// chiama il metodo di istanza ()qui usiamo l'oggetto per chiamare il metodo)
Person baby1 = mary.ProcreateWith(harry);
baby1.Name = "Gary";

// chiama il metodo statico usando il nome della classe 
Person baby2 = Person.Procreate(harry, jill);

//chiama il procreate usando l'operatore "*",  che chiama Procreate(harry, mary)
Person baby3 = harry * mary;

WriteLine($"{harry.Name} ha {harry.Children.Count} figli.");
WriteLine($"{mary.Name} ha {mary.Children.Count} figli.");
WriteLine($"{jill.Name} ha {jill.Children.Count} figli.");
WriteLine($"Il primo figlio di {harry.Name} si chiama {harry.Children[0].Name}");
WriteLine($"Il secondo figlio di {harry.Name} si chiama {harry.Children[1].Name}");
WriteLine($"Il terzo figlio di {harry.Name} si chiama {harry.Children[2].Name}");

WriteLine($"Il fattoriale di 5! è {Person.Factorial(5)}");

//assegna il metodo al campo delegato (la convenzione Microsoft per i nomi dei metodi che gestiscono gli eventi è ObjectName_EventName, quindi Harry_Shout)
// notare il "+=" per assegnare altri futuri metodi delegati allo stesso campo delegato 
harry.Shout += Harry_Shout; 
harry.Poke(); // AngerLevel=1, non succede nulla
harry.Poke(); // AngerLevel=2, non succede nulla
harry.Poke(); // AngerLevel=3, visualizza
harry.Poke(); // AngerLevel=1, visualizza

//generic lookup collection
Dictionary<int, string> dizionario = new();
dizionario.Add(1, "Alpha");
dizionario.Add(2, "Beta");
dizionario.Add(3, "Gamma");
// dizionario.Add(harry, "Delta"); questo non lo compila perhè harry è un oggetto e non un int
dizionario.Add(4, "Delta");
int key = 3;
WriteLine($"La chiave {key} ha valore {dizionario[key]}");

Person[] people =
{
    new() {Name = "Simon"},
    new() {Name = "Jenny"},
    new() {Name = "Adamon2"},
    new() {Name = "Richard"}

};

WriteLine("Lista iniziale di 'people':");
foreach (Person p in people) 
{
    WriteLine($"  {p.Name}");
}

WriteLine("Usa una implementazione dell'interfaccia IComparable, mediante un metodo (CompateTo) definito all'interno della classe Person, per effettuare il sort per Nome:");
Array.Sort(people);
foreach (Person p in people)
{
    WriteLine($"  {p.Name}");
}

WriteLine("Usa una implementazione dell'interfaccia IComparer, mediante il metodo (PersonComparer) per effettuare il sort per lunghezza stringa/Nome:");
Array.Sort(people, new PersonComparer());
foreach (Person p in people)
{
    WriteLine($"  {p.Name}");
}


// delegato: deve avere la stessa firma del delegante, cioè EventHandler(object? sender, EventArgs e)
static void Harry_Shout(object? sender, EventArgs e)
{
    if (sender is null) return; //questo conterrà il riferimento all'oggetto che ha chiamato il metodo
    Person p = (Person)sender;
    WriteLine($"{p.Name} è arrabbiato a livello {p.AngerLevel}");
}

// esempio di struct formata da due interi
// la struct altro non è che un oggetto che ha una sua struttura (come i campi che formano un record di tabella di database)
// e può essere comodo per passarlo ad una funzione, anzichè passare la lista dei velori di cui è composto
DisplacementVector dv1 = new(3, 5);
DisplacementVector dv2 = new(-2, 7);
DisplacementVector dv3 = dv1 + dv2; //usa il primo costruttore per sommare i due X tra di loro ed i due Y tra di solo
WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X}, {dv2.Y}) = ({dv3.Y}, {dv3.Y})");

dv1 = dv1 + 5;  //usa il secondo costruttore per sommare un intero sia ad X che ad Y
WriteLine($"({dv1.X}, {dv1.Y})");

int thisCannotBeNull = 4;
//thisCannotBeNull = null; // errore di compilazione

int? thisCouldBeNull = null;
WriteLine(thisCouldBeNull);  //questo emette un blanks perchè il valore è nullo
WriteLine(thisCouldBeNull.GetValueOrDefault()); // questo emette 0, il valore di default di un int quando queesto è nullo

thisCouldBeNull = 7;
WriteLine(thisCouldBeNull); // 7
WriteLine(thisCouldBeNull.GetValueOrDefault()); // 7

Employee john = new()
{
    Name = "John Jones",
    DateOfBirth = new(year: 1990, month: 7, day: 28)
};

john.WriteToConsole();

john.EmployeeCode = "JJ001";
john.HireDate = new(year: 2014, month: 11, day: 23);
WriteLine($"{john.Name} con codice {john.EmployeeCode} è stato assunto il {john.HireDate:dd/MM/yyyy}");

WriteLine(john.ToString());

Employee aliceEmployee = new()
{ 
    Name = "Alice", EmployeeCode = "AA123"
};

Person alicePerson = aliceEmployee;

Employee explicitAlice = (Employee)alicePerson;

aliceEmployee.WriteToConsole();
alicePerson.WriteToConsole();
WriteLine(aliceEmployee.ToString());
WriteLine(alicePerson.ToString());

try {

    john.TimeTravel(new(1999, 12, 31));
    john.TimeTravel(new(1950, 12, 25));

}
catch(PersonException ex)
{
    WriteLine(ex.Message);
}

string[] email = { "pamela@test.com", "ian&test.com" };
foreach(string posta in email)
{
    WriteLine($"{posta} è un indirizzo email valido: {StringExtensions.IsValidEmail(posta)}");

}


Rectangle r = new(height: 3.5, width: 4.5);
WriteLine($"Rettangolo H:{r.Height}, W:{r.Width}, A:{r.Area}");
Square q = new(5);
WriteLine($"Quadrato H:{q.Height}, W:{q.Width}, A:{q.Area}");
Circle c = new(radius: 2.5);
WriteLine($"Cerchio H:{c.Height}, W:{c.Width}, A:{c.Area}");

