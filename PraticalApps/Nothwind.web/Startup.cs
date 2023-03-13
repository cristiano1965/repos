namespace Northwind.web;
using Packt.Shared; // aggiunge Northwind extension method 
using static System.Console;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    { 
        services.AddRazorPages();   // per gestire le pagine  Dinamiche Razor
        services.AddNorthwindContext();  // registriamo il servizio EF Core per database Northwind
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment()) 
        {
            app.UseHsts(); //in PRODUZIONE usa HTTP Strict Transport Security (HSTS) che abilitato la forzatura di tutta la comunicazione con il server su HTTPS ed impedisce al visitatore di usare certificati non validi oppure untrusted
        }

        app.UseRouting(); // start endpoint routing

        app.Use(async (HttpContext context, Func<Task> next) =>
        {
            RouteEndpoint? rep = context.GetEndpoint() as RouteEndpoint;
            if (rep is not null)
            {
                WriteLine($"Endpoint name: {rep.DisplayName}");
                WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
            }
            if (context.Request.Path == "/bonjour")
            {
                // nel caso di match con un URL path, questo diventa un delegato che termina e ritorna, così non viene chiamato il prossimo delegato
                await context.Response.WriteAsync("Buongionrno Mondo!");
                return;
            }

            // potremmo modificare la richiesta prima di chiamare il prossimo delegato
            await next();
            // potremmo modificare il response dopo aver chiamato il prossimo delegato
        }
        );


        app.UseHttpsRedirection(); //redirect http to https 

        app.UseDefaultFiles(); // abilita la ricezione di file statici come index.html, default.html and so on
        app.UseStaticFiles();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages(); // abilita la ricezione di pagine dinamiche Razor (con estensione .cshtml) nel folder Pages

            endpoints.MapGet("/hello", () => "Ciao mondo."); //https://localhost:5001/hello ; se invece chiediamo solo la root allora ritorna il file statico index.html
        });

    }
}
