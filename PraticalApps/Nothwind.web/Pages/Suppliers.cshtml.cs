using Microsoft.AspNetCore.Mvc; // [BindProperty], IactionResult
using Microsoft.AspNetCore.Mvc.RazorPages; //PageModel
using Packt.Shared; // NorthwindContext

namespace Northwind.web.Pages;

public class SuppliersModel : PageModel
{
    private NorthwindContext db;

    public IEnumerable<Supplier>? Suppliers { get; set; }  //lista di fornitori in formato tabella di database
    public void OnGet()
    {
        ViewData["Title"] = "Cristiano B2B - Fornitori";

        Suppliers = db.Suppliers
            .OrderBy(c => c.Country).ThenBy(c => c.CompanyName);
    }

    public SuppliersModel(NorthwindContext injectedContext) //Costruttore che riceve il context e lo setta nella variabile privata dell'oggetto istanziato
    {
        db = injectedContext;
    }

    [BindProperty]      //connettiamo gli elementi HTML della pagina web alle proprietà della classe Supplier
    public Supplier? Supplier { get; set; }

    public IActionResult OnPost() // questo metodo risponde alla HTTP POST
    {
        if ((Supplier is not null) && ModelState.IsValid) //se non vuoto e le regole di validazione del modello (Required, StringLength, ecc.) sono ok.... 
        {
            db.Suppliers.Add(Supplier);
            db.SaveChanges();
            return RedirectToPage("/suppliers");
        }
        else
        {
            this.OnGet(); //ricarica la lista dei fornitori
            return Page(); // return to original page
        }
    }
}
