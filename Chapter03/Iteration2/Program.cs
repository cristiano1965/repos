using System.Collections;
using static System.Console;

string[] names = { "Adam", "Ciccio", "Corpulento"};

WriteLine("Esempio1");
foreach (string name in names) {

    WriteLine($"{name} ha {name.Length} caratteri");
}

WriteLine("Esempio2");

IEnumerator elemento = names.GetEnumerator();

while (elemento.MoveNext()) { //finchè c'è un prossimo elemento ritorna true, altrimenti false
    string name = (string)elemento.Current; //l'elemento corrente è readnly
    WriteLine($"{name} ha {name.Length} caratteri");
}
