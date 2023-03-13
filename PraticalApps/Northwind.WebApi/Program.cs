using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared; // AddNorthWindContext extension Method
using Northwind.WebApi.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI; //SubmitMethod
using Microsoft.AspNetCore.HttpLogging; //HttpLoggingFields

using static System.Console;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5002/"); //il webservice risponde su porta 5002


// Add services to the container.
builder.Services.AddNorthwindContext();
builder.Services.AddControllers(
    options =>
    {
        WriteLine("Default output formatters:");
        foreach (IOutputFormatter formatter in options.OutputFormatters)
        {
            OutputFormatter? mediaFormatter = formatter as OutputFormatter;
            if (mediaFormatter is null)
            {
                WriteLine($"   {formatter.GetType().Name}");
            }
            else
            {
                WriteLine("   {0}, Media Types: {1}",
                    mediaFormatter.GetType().Name,
                    string.Join(", ", mediaFormatter.SupportedMediaTypes)
                    );
            }
        }
    }
)
    .AddXmlDataContractSerializerFormatters()
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=> { c.SwaggerDoc("v1", new() { Title = "Northwind service API", Version = "V1" }); });

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096; // default is 32k
    options.ResponseBodyLogLimit = 4096; // default is 32k
});

builder.Services.AddCors(); // consentirà le chiamate a questo web service da altra origine (mvc è su 5001 ed il nostro web service risponde su 5002, quindi sono considerate due diverse origini !!!!)
var app = builder.Build();

app.UseHttpLogging();   // bisogna mettere su appsettings.json dentro Logging->LogLevel la seguente direttiva, altrimenti non logga nulla: "Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware": "Information"

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind service API Version 1");
        c.SupportedSubmitMethods(new[] { SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete});
    });
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(configurePolicy: options => {
    options.WithMethods("GET", "POST", "PUT", "DELETE");
    options.WithOrigins("https://5001"); // allow requests from the MVC client
});

app.MapControllers();

app.Run();
