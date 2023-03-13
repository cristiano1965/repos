using static System.Console;
using System.Globalization; // CultureInfo

Console.OutputEncoding = System.Text.Encoding.UTF8;

CultureInfo globalizzazione = CultureInfo.CurrentCulture;
CultureInfo localizzazione = CultureInfo.CurrentUICulture;

WriteLine($"The current glogalization culture is {globalizzazione.Name}: {globalizzazione.DisplayName}");
WriteLine($"The current localization culture is {localizzazione.Name}: {localizzazione.DisplayName}");

WriteLine();

WriteLine("en-US: English (United States)");
WriteLine("da-DK: Danish (Denmark)");
WriteLine("fr-CA: French (Canada)");


Write("Enter an ISO culture code: ");
string? newCulture = ReadLine();

if (!string.IsNullOrEmpty(newCulture))
{
    CultureInfo nuovaCi = new(newCulture);
    
    //change the current cultures;
    CultureInfo.CurrentCulture = nuovaCi;   
    CultureInfo.CurrentUICulture = nuovaCi; 


}

WriteLine();

Write("Enter your name: ");
string? name = ReadLine();

Write("Enter your date of birth: ");
string? dob = ReadLine();

Write("Enter your salary: ");
string? salary = ReadLine();

DateTime date = DateTime.Parse(dob);
int minutes = (int)DateTime.Today.Subtract(date).TotalMinutes;
decimal earns = decimal.Parse(salary);

WriteLine($"{name} è dato di {date:dddd} e sono trascorsi {minutes:N0} minuti dalla sua nascita e guadagna {earns:C}");



