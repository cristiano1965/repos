using System;
using System.Collections.Generic; // per usare List<T> cioè una collezione generica che può memorizzare una lista ordinata di qualiasi tipo
using System.Text;
using static System.Console;

namespace Packt.Shared
{ 
    // attenzione: classe parziale; il resto si trova dentro PersonAutogen.cs di PacktLibrary
    public partial class Person : Object //eredita da System.Object ; si puà anche non specificare perchè qualsiasi classe eredita sempre da questa 
    {
        // fields
        public string Name;
        public DateTime DateOfBirth;

        public WondersOfTheAncientWorld FavoriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;

        public List<Person> Children = new List<Person>(); //il campo children è una lista di instanze di Person 

        //constants
        public const string Species = "Homo Sapiens";

        //read-only fields
        public readonly string HomePLanet = "Earth";
        public readonly DateTime Istanziato;

        //costruttori: hanno lo stesso nome della classe
        public Person()
        {
            // impostiamo i valori di default dei campi, inclusi anche i read-only
            Name = "Unknown";
            Istanziato = DateTime.Now;
        }

        // secondo costruttore per inizializzare il nome e pianeta di nascita
        public Person(string initialName, string homePlanet)
        {
            
            Name = initialName;
            HomePLanet = homePlanet;
            Istanziato = DateTime.Now;
        }

        //metodi-------------------------------inizio----------
        public void WriteToConsole()
        {
            WriteLine($"{Name} é nato di {DateOfBirth:dddd} ");
        }

        public string GetOrigin()
        {
            return $"{Name} é nato su {HomePLanet} ";
        }

        public string SayHello() {
            return $"{Name} dice 'Ciao!'";
        }

        public string SayHello(string nome_in)
        {
            return $"{Name} dice 'Ciao {nome_in}!'";
        }

        public string ParametriOpzionali(
            string command = "Run!",
            double number = 0.0,
            bool active = true
            ) {

            return string.Format(
                format: "il comando è {0}, il numero è {1} e active è {2}",
                arg0: command,
                arg1: number,
                arg2: active
                );
        }

        public void PassareParametri(int x, ref int y, out int z) { //il parametro "ref" vuol dire che si riceve il riferimento alla variabile che viene passata
            // i parametri out non possono avere un valore di default e DEVONO essere inizializzati dentro il metodo
            z = 99;

            // valorizza i parametri incrementandoli
            x++;
            y++;
            z++;
        
        }
        /* metodi ------------------ fine-------------------*/

        /* costruttori*/
        // costruttore che ritorna una tupla, cioè un record con una serie di valori (in questo caso una stringa ed un int)
        // qui i valori ritornati non hanno un nome e potranno essere referenziati con "Item1" e "Item2"
        public (string, int) GetFruit() {
            return ("Apples", 5);
        }

        // costruttore che ritorna una tupla
        // qui i valori ritornati hanno un nome specifico, cioè "Name" e "Number"
        public (string Name, int Number) GetNamedFruit()
        {
            return ("Apples",5);
        }

        //decostruttori: il nome del metodo deve essere per forza "Deconstruct"
        public void Deconstruct(out string nome, out DateTime data_nascita) {

            nome = Name;
            data_nascita = DateOfBirth;
        }

        public void Deconstruct(out string nome, out DateTime data_nascita, out WondersOfTheAncientWorld meraviglie_vaforite)
        {

            nome = Name;
            data_nascita = DateOfBirth;
            meraviglie_vaforite = FavoriteAncientWonder;
        }


    }
}