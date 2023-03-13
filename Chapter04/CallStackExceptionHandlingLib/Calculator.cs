using static System.Console;

namespace Packt;

public class Calculator
{
    public static void Gamma() // public, così può essere chiamata dall'esterno
    {
        WriteLine("Sono dentro GAMMA !");
        Delta();
    }

    private static void Delta() // può essere richiamata solo da questa classe 
    {
        WriteLine("Sono dentro DELTA !");
        File.OpenText("bad file path");
    }
}
