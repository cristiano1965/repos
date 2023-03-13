using static System.Console;

namespace Packt.Shared;

public class Person : object, IComparable<Person> //ereditiamo anche da interfaccia IComparable usando una classe per la quale abbiamo il sorgente, cioè Person, se non avevamo il sorgente di Person avremmo dovuto usare l'interfaccia IComparer 
{
    //fields
    public string? Name;  // ? consente il null
    public DateTime DateOfBirth;
    public List<Person> Children = new(); 

    // methods
    public void WriteToConsole()
    {
        WriteLine($"{Name} è nato di {DateOfBirth:dddd}."+"Da base");
    }

    // metodo statico per "moltiplicarsi": riceve in input le due persone che creano un figlio,
    // il figlio viene aggiunto ad entrambe le persone con il nome "Filgio di p1 e p2"
    public static Person Procreate(Person p1, Person p2)
    {
        Person baby = new()
        {
            Name = $"Figlio di {p1.Name} e {p2.Name}"
        };

        p1.Children.Add(baby);
        p2.Children.Add(baby);
        
        return baby;

    }

    // metodo per dire, dato un oggetto persona, qual'è la persona con cui ha procreato
    // chiama il metodo statico passando l'instanza della persona procreante (this) ed il partner
    public Person ProcreateWith(Person partner)
    {
        return Procreate(this, partner);

    }

    //operatore per "procreare" (usiamo * al posto del nome del metodo) in modo che possiamo dire: Person baby3 = persona1 * persona2 
    public static Person operator *(Person p1, Person p2)
    {
        return Person.Procreate(p1, p2);
    }

    // metodo con la sua funzione interna
    public static int Factorial(int number) 
    {
        if (number < 0) 
        {
            throw new ArgumentException($"{nameof(number)} deve essere >= 0");
        }
        return localFactorial(number);
       
        int localFactorial(int localnumber) //funzione locale
        {
            if (localnumber < 1) return 1;
            return localnumber * localFactorial(localnumber - 1);
        }
    }

    /********************************* delegati ****/
    //definisce il campo Shout (che potrà essere nullo), di tipologia EventHandler che rappresenta il metodo che gestirà un evento che non ha dati; questo metodo è già predefinito come "delegate" (delegato)
    public event EventHandler? Shout;

    //campo dati
    public int AngerLevel;

    //metodo
    // ogni volta che una persona è "Poked" il suo AngerLevel incrementa e quando arriva a 3 viene chiamato il metodo delegato Shout passandogli l'oggetto (this) e la classe EventArgs vuota
    // ma solo se c'è almeno un evento delegato che punta ad un metodo definito da qualche altra parte nel codice (il metodo Harry_Shout nel Program.cs) quindi Shout non deve essere nullo
    public void Poke()
    {
        AngerLevel++;

        if (AngerLevel >= 3)
        {
            // se qualcosa è in ascolto...
            if (Shout != null)
            {
                //... allora chiama il delegato
                Shout(this, EventArgs.Empty); //oppure Shout?.Invoke(this, EventArgs.Empty) evitando la if null prima di chiamare il metodo
            }

        }
    }

    // ordina le istanze di Person usando il Name
    public int CompareTo(Person? other)
    {
        if (Name is null) return 0;
        return Name.CompareTo(other?.Name);
    }

    //override methods
    public override string ToString() //notare la keyword override, che sostituisce il metodo della classe base; questo è possibile solo perchè il metodo ToString() nella classe base ha la keyword "virtual", altrimenti non potevamo fare override
    {
        return $"{Name} è un {base.ToString()}";  // notare il base. che dice di usare il metodo della classe base
    }

    public void TimeTravel(DateTime quando) 
    {
        if (quando <= DateOfBirth)
        {
            throw new PersonException("Se viaggi indietro nel tempo prima della tua data di nascita, allora l'universo esploderà");
        }
        else 
        {
            WriteLine($"Benvenuto nel {quando:yyyy}");   
        }
    }
}
