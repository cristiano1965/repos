using Microsoft.AspNetCore.OData; // Add Odata Extension method
using Microsoft.OData.Edm; // IEdm Model
using Microsoft.OData.ModelBuilder; //ODataConventionModelBuilder
using Packt.Shared; // Northwind context and entity models

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5004/");

// Add services to the container.

builder.Services.AddNorthwindContext(); //registra il Northwind database context

builder.Services.AddControllers()
    .AddOData(options => options
    // register OData models using multiple estensions
    .AddRouteComponents(routePrefix: "catalog", model: GetEdmModelForCatalog())
    .AddRouteComponents(routePrefix: "orderSystem", model: GetEdmModelForOrderSystem())
    .AddRouteComponents(routePrefix: "v{version}", model: GetEdmModelForCatalog())
    // enable query options
    .Select() // enable $Select per la scelta dei campi (proiezione)
    .Expand() // enable $Expand per navigare nelle entità correlate
    .Filter() // abilita $Filter
    .OrderBy() // abilita OrderBy
    .SetMaxTop(100) // abilita $Top
    .Count() // Abilita $Count
    );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// definiamo questo metodo per definire e ritornare un modello OData per il database Northwind che esporrà solo le 3 tabelle che ci interessano
IEdmModel GetEdmModelForCatalog() 
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Category>("Categories");
    builder.EntitySet<Product>("Products");
    builder.EntitySet<Supplier>("Suppliers");

    return builder.GetEdmModel();

}

// definiamo questo secondo metodo per definire e ritornare un modello OData per gli ordini clienti che dal database northwind esporrà solo le 5 tabelle che ci interessano (notare come la tabella Product è stata definita anche qui )
IEdmModel GetEdmModelForOrderSystem()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Customer>("Customers");
    builder.EntitySet<Order>("Orders");
    builder.EntitySet<Employee>("Employees");
    builder.EntitySet<Product>("Products");
    builder.EntitySet<Shipper>("Shippers");

    return builder.GetEdmModel();

}