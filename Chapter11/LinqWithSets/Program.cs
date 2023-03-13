using static System.Console;

string[] gruppo1 = new[] { "Rachel","Gareth","Jonathan","George"};
string[] gruppo2 = new[] { "Jack", "Stephen", "Daniel", "Jack","Jared" }; //qui Jack c'è due volte
string[] gruppo3 = new[] { "Declan", "Jack", "Jack", "Jasmine", "Conor" }; //anche qui Jack c'è due volte

Output(gruppo1, "Gruppo 1");
Output(gruppo2, "Gruppo 2");
Output(gruppo3, "Gruppo 3");

Output(gruppo2.Distinct(), "Gruppo 2 Distinct() - cioè senza duplicati");
Output(gruppo2.DistinctBy(n => n.Substring(0,2)), "Gruppo 2 Distinct() - cioè senza duplicati sui primi due caratteri del nome");
Output(gruppo2.Union(gruppo3), "Gruppo2 Union(Gruppo3) - Unisce i due insiemi indicando solo una volta i duplicati e comuni ad entrambi (Jack compare solo una volta nonostante sia doppio su entrambi i gruppi !");
Output(gruppo2.Concat(gruppo3), "Gruppo2 Concat(gruppo3)-QUi ci sono tutti di tutti e due i gruppi");
Output(gruppo2.Intersect(gruppo3), "Gruppo2 Intersect(gruppo3)-Intersezione dei due gruppi (solo quelli che sono comuni ai due insiemi)");
Output(gruppo2.Except(gruppo3), "Gruppo2 Except(gruppo3)-Quelli del gruppo2 non presenti nel gruppo3");
Output(gruppo1.Zip(gruppo2, (c1, c2) => $"{c1} con {c2}"), "Gruppo1.Zip(gruppo2) ogni elemento del gruppo1 con il corrispettivo del gruppo2 - I mancanti sono esclusi (Jared)");


static void Output(IEnumerable<string> gruppo, string description = "") //lista di stringhe e descrizione opzionale
{
    if (!string.IsNullOrEmpty(description))
    {
        WriteLine(description);
    }
    Write(" ");
    WriteLine(string.Join(", ", gruppo.ToArray())); //converte la lista di stringhe in un array utilizzando il separatore a parametro1
    WriteLine();
}
