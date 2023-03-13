using System;
using System.Collections.Generic;
using System.Text;

namespace Packt.Shared
{
    public partial class Person
    {
        // propietà specificatamente definite solo in questa classe parziale, ma poi disponibili nell'oggetto
        public string Origin //definiamo una proprietà usando sintassi c# da versioni 1-5
        {
            get {
                return $"{Name} è nato su {HomePLanet}";
            }
        }

        // //definiamo una proprietà usando sintassi c# 6+ con il Lambda
        public string Greeting => $"{Name} dice 'Ciao!'";
        public int Age => System.DateTime.Today.Year - DateOfBirth.Year;

        public string FavoriteIceCream { get; set; } //sintassi automatica di una proprietà che ha un getter ed un setter

        private string favPriColor; /// attenzione: è privata, quindi visibile solo dai metodi della classe

        public string FavoritePrimaryColor // questa è invece la prorpietà visibile dall'esterno che ritorna il contenuto della variabile privata
        {
            get 
            {
                return favPriColor; 
            }
            set
            {
                switch (value.ToLower())
                {
                    case "red":
                    case "green":
                    case "blue":
                        favPriColor = value;   //imposta la variabile privata con un valore che arriverà (value)
                        break;
                    default:
                        favPriColor = $"{value} is not a primary color." + "Choose from: red, green, blue.";
                        break;
                        /* oppure 
                        throw new System.ArgumentException(
                            $"{value} is not a primary color." + "Choose from: red, green, blue."
                            );
                        */

                }
            }
        }

        //definisce un indexer per i Children di una persona
        public Person this[int index]
        {
            get 
            {
                return Children[index]; // passa il valore all'indicizzatore List<T>
            }
            set
            {
                Children[index] = value; // riceve il valore "value" con cui impostare l'indicizzatore List<T>
            }
        }
    }
}
