using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; //DbContext, DbSet<T>
using static System.Console;


namespace Packt.Shared;

// questo gestirà la connessione al database
public class Northwind : DbContext
{
    // queste prorpietà si mappano alle tabelle del database
    public DbSet<Category>? Categories { get; set; }    //tabella categorie
    public DbSet<Product>?  Products { get; set; }  //tabella prodotti

    public DbSet<Customer>? Customers { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (ProjectConstants.DatabaseProvider == "SQLServer") 
        {
            string connection = "Data Source=.;" +
                "Initial Catalog=Northwind;" +
                "Integrated Security = true;" +
                "MultipleActiveResultSets=true;" 
                ;

            optionsBuilder.UseSqlServer(connection);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasConversion<double>();
    }
}
