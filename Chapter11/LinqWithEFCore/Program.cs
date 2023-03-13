using Packt.Shared;
using static System.Console;
using Microsoft.EntityFrameworkCore; //DbSet<T>
using System.Xml.Linq; // per essere usato da OutputProductsAsXml, cioè output di LINQ in XML

//FilterAndSort();
//JoinCategoriesAndProducts();
//GroupJoinCategoriesAndProducts();
//AggregateProducts();
//OutputProductsAsXml();
//OutputProductsAsXml(2);
//ProcessSettings();
GroupCountries();

static void FilterAndSort()
{
    using (Northwind db = new())
    {
        DbSet<Product> allProducts = db.Products;

        IQueryable<Product> filteredProducts =
            allProducts.Where(p => p.UnitPrice < 10M);

        IOrderedQueryable<Product> sortedAndFilteredProducts =
            filteredProducts.OrderByDescending(p => p.UnitPrice);

        var projectedProducts = sortedAndFilteredProducts
            .Select(p => new //tipo anonimo
            {
                p.ProductId,
                p.ProductName,
                p.UnitPrice
            });

        WriteLine("Products that cost less that $10:");
        foreach (var p in projectedProducts)
        {
            WriteLine("{0}: {1} costa {2:$#,##0.00}", p.ProductId, p.ProductName, p.UnitPrice);
        }

        WriteLine();
    }
}

static void JoinCategoriesAndProducts()
{
    using (Northwind db = new())
    {
        // join every product to its category and return 77 matches (questa genera una inner join tra Products e Categories)
        var queryJoin = db.Products.Join(
            inner: db.Categories,
            outerKeySelector: product => product.CategoryId,
            innerKeySelector: category => category.CategoryId,
            resultSelector: (p, c) =>
                new { c.CategoryName, p.ProductName, p.ProductId })
            .OrderBy(cp => cp.CategoryName);

        string strQuery =queryJoin.ToQueryString();

        foreach (var item in queryJoin)
        {
            WriteLine($"{item.ProductId}: {item.ProductName} è nella categoria {item.CategoryName}");
        }
    }

    
}

static void GroupJoinCategoriesAndProducts()
{
    using (Northwind db = new())
    {
        // group all products by theit category to return 8 matches
        // in questo caso convertiamo il ritorno, che normalmente è un IQueryable<T>, in un Enumerable<T> usando AsEnumerable()
        // perchè poi dobbiamo leggere i prodotti di ogni categoria restituiti dalla sottoqueri 
        var queryGroup = db.Categories.AsEnumerable().GroupJoin(              
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, p) =>
                new {
                    c.CategoryName,
                    listaProdotti = p.OrderBy(p => p.ProductName)});

       //string strQuery? = queryGroup.ToQueryString();  /qui non riusciamo a tirare fuori la query string perchè anche essendo una group by richiediamo anche la lista di dettaglio dei prodotti (ordinata per nome)

        foreach (var category in queryGroup)
        {
            WriteLine($"{category.CategoryName} ha {category.listaProdotti.Count()} prodotti");

            foreach (var product in category.listaProdotti) 
            {
                WriteLine($" {product.ProductId}: {product.ProductName}");
            }
        }
    }


}

static void AggregateProducts()
{
    using (Northwind db = new())
    {
        WriteLine("{0, -25} {1, 10 }", "Products count:", db.Products.Count());
        WriteLine("{0, -25} {1, 10:$#,##0.00}", "Highest product price:", db.Products.Max(p => p.UnitPrice));
        WriteLine("{0, -25} {1, 10:N0}", "Sum of units in stock:", db.Products.Sum(p=>p.UnitsInStock));
        WriteLine("{0, -25} {1, 10:N0}", "Sum of units on order:", db.Products.Sum(p=>p.UnitsOnOrder));
        WriteLine("{0, -25} {1, 10:$#,##0.00}", "Average unit price:", db.Products.Average(p=>p.UnitPrice));
        WriteLine("{0, -25} {1, 10:$#,##0.00}", "Value of units in stock:", db.Products.Sum(p=>p.UnitsInStock * p.UnitPrice));
    }


}

// due diversi modi per generare un XML leggendo i dati da DB con LINQ
static void OutputProductsAsXml(int formato = 1)
{
    using (Northwind db = new())
    {
        Product[] prodottiArray = db.Products.ToArray();

        XElement xml = null;

        if (formato == 1) { 
             xml = new("Prodotti",
                from p in prodottiArray
                select new XElement("prodotto",
                            new XElement("dati",
                                new XElement("id", p.ProductId),
                                new XElement("price", p.UnitPrice),
                                new XElement("name", p.ProductName)
                            ),
                            new XElement("quantitativi",
                                new XElement("in_stock", p.UnitsInStock),
                                new XElement("in_ordine", p.UnitsOnOrder)
                            )
                        )
            );
        }
        else if (formato == 2)
        {
            xml = new("Prodotti",
               from p in prodottiArray
               select new XElement("prodotto",
                            new XAttribute("id", p.ProductId),
                            new XAttribute("price", p.UnitPrice ?? 0),
                         new XElement("name", p.ProductName)
                           ));
        }
        if (xml is not null)
            WriteLine(xml.ToString());

        /*
         ** formato 1: il prodotto ha due nodi: 1 con <dati> ed un altro con <quantitativi>
          <Prodotti>
              <prodotto>
                <dati>
                  <id>1</id>
                  <price>18</price>
                  <name>Chai</name>
                </dati>
                <quantitativi>
                  <in_stock>39</in_stock>
                  <in_ordine>0</in_ordine>
                </quantitativi>
              </prodotto>
              <prodotto>
                <dati>
                  <id>2</id>
                  <price>19</price>
                  <name>Chang</name>
                </dati>
                <quantitativi>
                  <in_stock>17</in_stock>
                  <in_ordine>40</in_ordine>
                </quantitativi>
              </prodotto>
            </Prodotti>

         ** formato2: il prodotto ha due elementi: il primo si chiama "prodotto" con sue attributi (id) e (price), il secondo si chiama "name"
          <Prodotti>
              <prodotto id="1" price="18">
                <name>Chai</name>
              </prodotto>
              <prodotto id="2" price="19">
                <name>Chang</name>
              </prodotto>
           </Prodotti>
         *
         */
    }

}

static void ProcessSettings()
{
    XDocument doc = XDocument.Load("settings.xml");

    var fileXml = doc.Descendants("appSettings")
        .Descendants("add")
        .Select(nodo => new
        {
            Chiave = nodo.Attribute("key")?.Value,
            Valore = nodo.Attribute("value")?.Value,
        }
        ).ToArray();

    foreach (var item in fileXml)
    {
        WriteLine($" {item.Chiave}: {item.Valore}");
    }
}

static void GroupCountries()
{
    using (Northwind db = new())
    {
        // group all products by theit category to return 8 matches
        // in questo caso convertiamo il ritorno, che normalmente è un IQueryable<T>, in un Enumerable<T> usando AsEnumerable()
        // perchè poi dobbiamo leggere i prodotti di ogni categoria restituiti dalla sottoqueri 
        String[]? listaCountries = db.Customers?.GroupBy(c => c.Country).Select(g =>  g.Key ).ToArray();

       

        //string strQuery? = queryGroup.ToQueryString();  /qui non riusciamo a tirare fuori la query string perchè anche essendo una group by richiediamo anche la lista di dettaglio dei prodotti (ordinata per nome)

        foreach (var country in listaCountries)
        {
            WriteLine(country);

            
        }
    }


}
