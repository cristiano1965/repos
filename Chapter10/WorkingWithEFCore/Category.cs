using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema; // [Column]

namespace Packt.Shared;

public class Category
{
    //these properties map to columns in the database
    public int CategoryId { get; set; }
    public string? CategoryName  { get; set; }

    [Column(TypeName = "ntext")]
    public string? Description { get; set; }

    //defines a navigation property for related rows
    public virtual ICollection<Product> Products { get; set; }

    public Category()
    {
        //to enable developers to add products to a Category we must
        // initialize the navogation property to an empy collection
        Products = new HashSet<Product>();  //hashset è una lista non ordinata di elementi univoci. It is generally used when we want to prevent duplicate elements from being placed in the collection. The performance of the HashSet is much better in comparison to the list. 
    }
}
