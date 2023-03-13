using Northwind.web;

Host.CreateDefaultBuilder(args)
  .ConfigureWebHostDefaults(webBuilder =>
  {
      webBuilder.UseStartup<Startup>(); //specifichiamo che la classe di avvio e Startup in Startup.cs
  }).Build().Run();

Console.WriteLine("Questo viene eseguito dopo  che web server viene fermato!");

/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts(); //usa HTTP Strict Transport Security (HSTS) che abilitato la forzatura di tutta la comunicazione con il server su HTTPS ed impedisce al visitatore di usare certificati non validi oppure untrusted
}

app.UseHttpsRedirection(); //redirect http to https

app.MapGet("/", () => "Hello World!");

app.Run();

Console.WriteLine("Questo viene eseguito dopo che il web server è stato fermato");
*/