using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization; //aggiungiamo il decoratore XmlAttribute per personalizzare il nome dell'elemento Xml e risparmiare spazio su disco

namespace Packt.Shared;

public class Person
{
    public Person() { }
    public Person(decimal initialSalary)
    {
         Salary = initialSalary;

    }
   
    protected decimal Salary { get; set; }
    [XmlAttribute("fname")]
    public string? FirstName { get; set; }
    [XmlAttribute("lname")]
    public string? LastName { get; set; }
    [XmlAttribute("dob")]
    public DateTime DateOfBirth { get; set; }
    public HashSet<Person>? Children { get; set; }


}
