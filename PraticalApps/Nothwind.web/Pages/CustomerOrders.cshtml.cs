using Microsoft.AspNetCore.Mvc.RazorPages; // PageModel
using Microsoft.EntityFrameworkCore; // Include extension method
using Packt.Shared; // Customer

namespace Northwind.Web.Pages;

public class CustomerOrdersModel : PageModel
{
    public Customer? Customer;

    private NorthwindContext db;

    public CustomerOrdersModel(NorthwindContext db)
    {
        this.db = db;
    }

    public void OnGet()
    {
        string id = HttpContext.Request.Query["id"];

        /*
         *  SELECT *
              FROM (
                  SELECT TOP(2) *
                  FROM [Northwind].[dbo].[Customers]AS [c]
                  WHERE [c].[CustomerId] = 'ALFKI'
              ) AS [t]
              LEFT JOIN [Northwind].[dbo].[Orders] AS [o] ON [t].[CustomerId] = [o].[CustomerId]
              ORDER BY [t].[CustomerId]
         */

        Customer = db.Customers.Include(c => c.Orders)
          .SingleOrDefault(c => c.CustomerId == id);
    }
}