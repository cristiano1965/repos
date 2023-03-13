//using Microsoft.AspNetCore.Mvc; // [BindProperty], IactionResult
using Microsoft.AspNetCore.Mvc.RazorPages; //PageModel
using Packt.Shared; // NorthwindContext

namespace PacktFeatures.Pages;

public class EmployeesPageModel : PageModel
{
    private NorthwindContext db;

    public Employee[] Impiegati { get; set; } = null!; //array di Impiegati non vuoto

    public void OnGet()
    {
        ViewData["Title"] = "Cristiano B2B - Impiegati";

        Impiegati = db.Employees
            .OrderBy(i => i.LastName).ThenBy(i => i.FirstName).ToArray();
    }

    public EmployeesPageModel(NorthwindContext injectedContext) //Costruttore che riceve il context e lo setta nella variabile privata dell'oggetto istanziato
    {
        db = injectedContext;
    }

    
}
