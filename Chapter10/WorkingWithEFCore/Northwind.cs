using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static System.Console;


namespace Packt.Shared;

// questo gestorà la connessione al database
public class Northwind : DbContext
{
    public DbSet<Category>? Categories { get; set; }    //tabella categorie
    public DbSet<Product>?  Products { get; set; }  //tabella prodotti
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (ProjectConstants.DatabaseProvider == "SQLServer") 
        {
            string connection = "Data Source=.;" +
                "Initial Catalog=Northwind;" +
                "Integrated Security = true;" +
                "MultipleActiveResultSets=true;" +
                "Encrypt = False;";

            optionsBuilder.UseSqlServer(connection);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //example of using Fluent API instead of attributes to limit the length of a category name to 15
        modelBuilder.Entity<Category>()
            .Property(category => category.CategoryName)
            .IsRequired()   // NOT NULL
            .HasMaxLength(15);

        //global filter to remove discontinued products
        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.Discontinued);
    }
}
