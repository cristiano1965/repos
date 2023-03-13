using Microsoft.AspNetCore.Mvc; //IActionResult
using Microsoft.AspNetCore.OData.Query; //[EnableQuery]
using Microsoft.AspNetCore.OData.Routing.Controllers; // ODataController
using Packt.Shared; //northwindcontext
using static System.Console;

namespace Northwind.OData.Controllers;

public class ProductsController : ODataController
{
    private readonly NorthwindContext db;

    public ProductsController(NorthwindContext db) 
    {
        this.db = db;
    }

    

    [EnableQuery]
    public IActionResult Get(string version="1")
    {
        WriteLine($"ProductsController version{version}.");
        var prodotti = db.Products;
        return Ok(db.Products);
    }
    [EnableQuery]
    public IActionResult Get(int key, string version = "1")
    {
        WriteLine($"ProductsController version{version}.");
        Product? p =  db.Products.Find(key);

        if (p is null)
        {
            return NotFound($"Product with id {key} not found.");
        }
        else 
        {
            if (version == "2") 
            {
                p.ProductName += " version 2.0";
            }
            return Ok(p);
        }
    }

    public IActionResult Post([FromBody] Product p) 
    {
        db.Products.Add(p);
        db.SaveChanges();
        return Created(p);
    }
}
