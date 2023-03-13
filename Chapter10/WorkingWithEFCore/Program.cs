using Packt.Shared;
using static System.Console;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; //ILoggerProvider, Ilogger, LogLevel
using Microsoft.EntityFrameworkCore.Storage; //IDbContextTransaction


WriteLine($"Using {ProjectConstants.DatabaseProvider} database provider");

//QueryingCategories();
//FilteredIncludes();
//QueryingProducts();
//QueryingWithLike();

//add 

if (AddProduct(6, "Bob's burgers", 500M))
    WriteLine("prodotto aggiunto");
if (AddProduct(6, "Bob's veggies", 550M))
    WriteLine("prodotto aggiunto");

//modify
int aggiornati = IncreaseProductPrice("Bob", 20M);
    WriteLine($"{aggiornati} prodotti aggiornati");

//delete

WriteLine($"{DeleteProducts("Bob")} prodotti cancellati");



ListProducts();


static void QueryingCategories()
{
    using (Northwind db = new())
    {
        WriteLine("Categories and now many products they have:");

        //a query to get all categories and count their relaterd products
        IQueryable<Category>? categories = db.Categories? .Include(c => c.Products);

        if (categories is null)
        {
            WriteLine("No categories found.");
            return;
        }

        //execute query and enumerate results
        foreach(Category c in categories)
        {
            WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
        }
    }
}

static void FilteredIncludes()
{
    using (Northwind db = new())
    {
        Write("Enter a minimum for units in stock: ");

        string unitsInStock = ReadLine() ?? "10"; //se vuoto default = 10
        int stock = int.Parse(unitsInStock);

        IQueryable<Category>? categories = db.Categories?.Include(c => c.Products.Where(p => p.Stock >= stock));


        if (categories is null)
        {
            WriteLine("No categories found.");
            return;
        }
        WriteLine($"ToQueryString: {categories.ToQueryString()}");
        //execute query and enumerate results
        foreach (Category c in categories)
        {
            WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum stock of {stock} units in stock.");
            foreach (Product p in c.Products) 
            {
                WriteLine($"  {p.ProductName} has {p.Stock} units in stock,");
            }
        }
    }
}

static void QueryingProducts()
{
    using (Northwind db = new())
    {
        ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(new ConsoleLoggerProvider());

        WriteLine("Products that cost more than a price, highest at top");
        string? input;
        decimal price;

        do
        {
            Write("Enter a a product price: ");
            input = ReadLine();
        } while (!decimal.TryParse(input, out price));

        IQueryable<Product>? products = db.Products?
            .TagWith("Products filtered by price > "+price+" and sorted by reverse cost") //aggiunge un tag descrittivo deciso da noi nel logger
            .Where(p => p.Cost > price)
            .OrderByDescending(p => p.Cost);
        ;


        if (products is null)
        {
            WriteLine("No products found.");
            return;
        }
        //WriteLine($"ToQueryString: {products.ToQueryString()}");
        foreach (Product p in products)
        {
            WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} in stock", p.ProductId, p.ProductName, p.Cost, p.Stock);
        }
    }
}

static void QueryingWithLike()
{
    using (Northwind db = new())
    {
        ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(new ConsoleLoggerProvider());

        Write("Enter part of product name: ");
        string? input = ReadLine();
        

        IQueryable<Product>? products = db.Products?
            .TagWith("Products filtered by name like '" + input + "'") //aggiunge un tag descrittivo deciso da noi nel logger
            .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));
        ;


        if (products is null)
        {
            WriteLine("No products found.");
            return;
        }
        //WriteLine($"ToQueryString: {products.ToQueryString()}");
        foreach (Product p in products)
        {
            WriteLine("{0} has {1} units in stock. Discontinued ? {2}",  p.ProductName, p.Stock, p.Discontinued);
        }
    }
}

static bool AddProduct(int categoryid, string productName, decimal? price)
{
    using (Northwind db = new()) 
    {
        Product p = new()
        {
            CategoryId = categoryid,
            ProductName = productName,
            Cost = (decimal)price

        };

        // mark product as added in change tracking
        db.Products.Add(p);

        //save tracked change to database
        int affected = db.SaveChanges();
        return affected ==  1;
    }
}

static void ListProducts()
{

    using (Northwind db = new())
    {
        WriteLine("{0, -3} {1, -35} {2,8} {3,5} {4}",
            "Id", "Product Name", "Cost", "stock", "Disc");
        foreach(Product p in db.Products
            .OrderByDescending(p => p.ProductId)) 
        {
            WriteLine("{0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
                p.ProductId, p.ProductName, p.Cost, p.Stock, p.Discontinued);
            
        }
    }
}

static int IncreaseProductPrice( string productNameStartsWith, decimal amount)
{
    using (Northwind db = new())
    {
        // get all products whose name starts with a specific string
        IQueryable<Product>? updateProducts = db.Products?.Where(
            p => p.ProductName.StartsWith(productNameStartsWith)
            );
        if (updateProducts is not null) 
        {
            foreach(Product p in updateProducts)
                p.Cost += amount;
        }
        

        //save tracked change to database
        int affected = db.SaveChanges();
        return affected;
    }
}

static int DeleteProducts(string productNameStartsWith)
{
    using (Northwind db = new())
    {
        using (IDbContextTransaction transaction = db.Database.BeginTransaction()) 
        {
            WriteLine($"Transaction isolation level: {transaction.GetDbTransaction().IsolationLevel}"); //se ReadCommitted: when editing, it applies read lock(s) to block other users from reading the record(s) until transaction ends 
            // get all products whose name starts with a specific string
            IQueryable<Product>? deleteProducts = db.Products?.Where(
                p => p.ProductName.StartsWith(productNameStartsWith)
                );

            if (deleteProducts is not null)
            {
                db.Products.RemoveRange(deleteProducts);
            }


            //save tracked change to database
            int affected = db.SaveChanges();
            transaction.Commit();
            return affected;

        }
    }   
}