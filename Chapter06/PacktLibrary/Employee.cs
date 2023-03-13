using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace Packt.Shared;

public class Employee : Person
{
    public string? EmployeeCode { get; set; }
    public DateTime HireDate { get; set; }

    // stiamo sostituiendo il metodo WriteToConsole già definito nella classe base e se vogliamo evitare il warning del compilatore, che dice che stiamo nascondendo il metodo base, aggiungiamo
    // la keyword new al metodo, in tal modo diciamo al compilatore che deliberatamente lo stiamo sostituendo a quello della classe base
    public new  void WriteToConsole() 
    {
        WriteLine($"{Name} è nato il {DateOfBirth: dd/MM/yyyy} ed è stato assunto il {HireDate: dd/MM/yyyy}"+" da employee");
    }

    //override methods
    public override string ToString() //notare la keyword override, che sostituisce il metodo della classe base; questo è possibile solo perchè il metodo ToString() nella classe base ha la keyword "virtual", altrimenti non potevamo fare override
    {
        return $"il codice di {Name} è {EmployeeCode}";  
    }
}
