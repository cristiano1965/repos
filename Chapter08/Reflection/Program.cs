using static System.Console;
using System.Reflection; // Assembly

WriteLine("Assembly metadata:");

Assembly assembly = Assembly.GetEntryAssembly();

if (assembly is null) 
{
    WriteLine("Failed to get entry assembly");
    return;
}

WriteLine($"   Full name: {assembly.FullName}");
WriteLine($"   Lacation: {assembly.Location}");

IEnumerable<Attribute> attributi = assembly.GetCustomAttributes();

WriteLine($"   Assembly-level attributes: ");
foreach (Attribute a in attributi)
{
    WriteLine($"   {a.GetType()}");

}

