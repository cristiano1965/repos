using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packt.Shared;

public class PersonComparer : IComparer<Person>
{
    // qui ordina le persone in base alla lunghezza del nome ed a parità di lunghezza per Nome
    public int Compare(Person? x, Person? y)
    {
        if (x is null || y is null) return 0;

        // compara la lunghezza dei nomi
        int result = x.Name.Length.CompareTo(y.Name.Length);

        // if they are equal....
        if (result == 0)
        {
            //... then compare by the Names
            return x.Name.CompareTo(y.Name);
        }
        else { //se maggiore (1) o minore (-1) lunghezza
        
            // .... otherwise compare by the lenghts
            return result;
        }
    }
}
