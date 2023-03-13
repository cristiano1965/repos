using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packt.Shared;  // namespace in C#10

public class ImmutablePerson
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

}

public record ImmutableVehicle  // il record è come una classe ma con valori immutabili
{
    public int Wheels { get; init; }
    public string? Color { get; init; }
    public string? Brand { get; init; }

}


// anche il record, come la classe, puo avere il costruttore ed il decostruttore
// che possono anche non essere specificati, perchè basterebbe scrivere in alternativa
// public record ImmutableAnimal(string Name, string Species)
public record ImmutableAnimal
{
    public string Name { get; init; }
    public string Species { get; init; }

    // costruttore del record 
    public ImmutableAnimal(string name, string species)
    {
        Name = name;
        Species = species;

    }

    // decostruttore del record

    public void Deconstruct(out string name, out string species)
    {
        name = Name;
        species = Species;
    }
}
