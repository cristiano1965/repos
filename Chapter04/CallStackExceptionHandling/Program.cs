using Packt;

using static System.Console;

WriteLine("Sono nel Main");
Alpha();

static void Alpha() {
    WriteLine("Sono in Alpha");
    Beta();
}

static void Beta()
{
    WriteLine("Sono in Beta");
    try
    {
        Calculator.Gamma();
    }
    catch (Exception ex) {
        WriteLine($"Catturato questo errore: {ex.Message}");
        throw;
    }
}