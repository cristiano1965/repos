
using System.Xml.Serialization; // XmlSerializer
using Packt.Shared; // Person
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using NewJson = System.Text.Json.JsonSerializer;  //rinomina l'import per evitare conflitti con Newtonsoft.json

Console.OutputEncoding = System.Text.Encoding.UTF8;

SerializzaJson();


static List<Person> InizializzaPersone()
{
    // create an ojbect graph; attenzione: solo i campi PUBLIC verranno serializzati 
    List<Person> people = new()
    {
        new(30000M)
        {
            FirstName = "Alice",
            LastName = "Smith",
            DateOfBirth = new(1974, 3, 14)
        },
        new(40000M)
        {
            FirstName = "Bob",
            LastName = "Jones",
            DateOfBirth = new(1969, 11, 21),

        },
        new(20000M)
        {
            FirstName = "Charlie",
            LastName = "Cox",
            DateOfBirth = new(1969, 11, 21),
            Children = new()
            {
                new(0m)
                {
                    FirstName = "Sally",
                    LastName = "Cox",
                    DateOfBirth = new(2000, 7, 12),
                }
            }
        },
    };

    return people;
}

static  void SerializzaJson()
{

    List<Person> people = InizializzaPersone();

    // create a file to write to
    string file = Combine(CurrentDirectory, "people.json");

    
    //create a new object the will format as Json
    //Newtonsoft.Json.JsonSerializer jss = new();

    // serialize the object into a string
    //jss.Serialize(jsonStream, people);
    string jsonString = NewJson.Serialize(people);
    File.WriteAllText(file, jsonString);

    
    WriteLine();
    WriteLine($"Written {new FileInfo(file).Length} bytes of JSON to {file}");

    // display the serialized object graph
    //WriteLine(File.ReadAllText(file));

    
    using (FileStream jsonLoad = File.Open(file, FileMode.Open))
    {
        //deserialize and cast the object graph into a List of Person
        List<Person>? loadedPeople =  NewJson.Deserialize(jsonLoad, typeof(List<Person>)) as List<Person> ;

        if (loadedPeople is not null)
        {
            foreach (Person p in loadedPeople)
            {
                WriteLine($"{p.LastName} ha {p.Children?.Count ?? 0} figli.");
            }
        }
    }
    
}

static void SerializzaXml()
{
    List<Person> people = InizializzaPersone();

    // create an object that will format a List of Perons as XML
    XmlSerializer xs = new(people.GetType());

    // create a file to write to
    string file = Combine(CurrentDirectory, "people.xml");

    using (FileStream stream = File.Create(file))
    {
        // serialize the object to the stream
        xs.Serialize(stream, people);
    }

    WriteLine($"Written {new FileInfo(file).Length} bytes of XML to {file}");

    // display the serialized object graph
    WriteLine(File.ReadAllText(file));

    using (FileStream xmlLoad = File.Open(file, FileMode.Open))
    {
        //deserialize and cast the object graph into a List of Person
        List<Person>? loadedPeople = xs.Deserialize(xmlLoad) as List<Person>;

        if (loadedPeople is not null)
        {
            foreach (Person p in loadedPeople) 
            {
                WriteLine($"{p.LastName} ha {p.Children?.Count ?? 0} figli.");
            }
        }
    }
}   