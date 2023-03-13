using Packt.Shared;
namespace Northwind.mvc.Models;

public class ODataProducts
{
    public Product[]? Value { get; set; } //il json ritorna un array di prodotti nella proprietà value

    
}
