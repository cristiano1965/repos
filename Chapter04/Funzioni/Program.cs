using System.Globalization;
using static System.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

//Tabellina(5);

/*
// Display the name of the current culture.
Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.Name);
// Display the name of the current UI culture.
Console.WriteLine("CurrentUICulture is {0}.", CultureInfo.CurrentUICulture.Name);

RegionInfo myRI1 = new RegionInfo("IT");
Console.WriteLine("   Name:                         {0}", myRI1.Name);
Console.WriteLine("   DisplayName:                  {0}", myRI1.DisplayName);
Console.WriteLine("   EnglishName:                  {0}", myRI1.EnglishName);
Console.WriteLine("   IsMetric:                     {0}", myRI1.IsMetric);
Console.WriteLine("   ThreeLetterISORegionName:     {0}", myRI1.ThreeLetterISORegionName);
Console.WriteLine("   ThreeLetterWindowsRegionName: {0}", myRI1.ThreeLetterWindowsRegionName);
Console.WriteLine("   TwoLetterISORegionName:       {0}", myRI1.TwoLetterISORegionName);
Console.WriteLine("   CurrencySymbol:               {0}", myRI1.CurrencySymbol);
Console.WriteLine("   ISOCurrencySymbol:            {0}", myRI1.ISOCurrencySymbol);
*/

decimal tassaDaPagare = CalcolaTasse(149, "FR"); // oppure CalcolaTasse(amount: 149, twoLetterRegionCode: "FR")
WriteLine($"Devi pagare {tassaDaPagare:C} di tasse.");

static void Tabellina(byte number) {

    WriteLine($"Questa è la tabellina del {number}:");

    for (int i = 1; i <= 12; i++) {

        WriteLine($"{i} x {number} = {i * number}");
    }

    WriteLine();

}

static decimal CalcolaTasse(decimal amount, string twoLetterRegionCode)
{
    decimal rate = 0.0M;

    switch (twoLetterRegionCode) {

        case "CH":  // svizzera
            rate = 0.08M;
            break;
        case "DK":  // danimarca
        case "NO":  // norvegia
            rate = 0.25M;
            break;
        case "GB":  // inghilterra 
        case "FR":  // francia
            rate = 0.2M;
            break;
        case "HU":  // ungheria 
        case "AK":  // alaska
            rate = 0.0M;
            break;
        default:
            rate = 0.0M;
            break;

    }

    return amount * rate;
}
