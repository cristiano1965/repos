using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // [Required], [StringLength]
using System.ComponentModel.DataAnnotations.Schema; // [Column]

namespace Packt.Shared;

public class Product
{
    public int ProductId { get; set; } // Primary Key

    [Required]
    [StringLength(40)]
    public string ProductName { get; set; } = null!; // non nullo 

    [Column("UnitPrice", TypeName ="money")]
    public decimal Cost { get; set; } //cambiamo il nome della colonna (sul DB si chiama "UnitPrice", la mappiamo con il nome interno "Cost"
                                      //
    [Column("UnitsInStock")]
    public short? Stock { get; set; }

    public bool Discontinued { get; set; }

    //these two define the foreign key relationship to the Categories table
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!; //
}
