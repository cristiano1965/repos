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

    public int? SupplierId { get; set; }
    public int? CategoryId { get; set; }
    [StringLength(20)]
    public string? QuantityPerUnit { get; set; } 

    [Column(TypeName ="money")] //required for sqlserver provider
    public decimal? UnitPrice { get; set; } 
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    

    public bool Discontinued { get; set; }

    
}
