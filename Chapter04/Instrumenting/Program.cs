using System.Globalization;
using static System.Console;
using System.Diagnostics;
using Microsoft.Extensions.Configuration; //installato con nuget sul progetto Microsoft.Extensions.Configuration e Microsoft.Extensions.Configuration.Binder e Microsoft.Extensions.Configuration.json e Microsoft.Extensions.Configuration.Fileextensions

/*-----------------------------------------
 * questi sono i livelli tracelevel
 *  NUMERO  Parola      Descrizione
 *    0       Off       Nessun Output
 *    1       Error     Output solo di errori
 *    2       Warning   Output solo di errori e warnings
 *    3       Info      Output solo di errori, warnings e informazioni
 *    4       Verbose   Output di tutto
 *-------------------------------------------------*/
Console.OutputEncoding = System.Text.Encoding.UTF8;

// write to a text file in the project folder 
Trace.Listeners.Add(new TextWriterTraceListener(
    File.CreateText(
        Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.Desktop), "log.txt")
    )
  )   
);

//text writer is buffered, so this option calls Flush() on all listeners after writing
Trace.AutoFlush = true;

Debug.WriteLine("Debug says: I'm watching!"); //se la soluzione è DEBUG questa viene scritta, se RELEASE non viene scritta
Trace.WriteLine("Trace says: I'm watching!"); // questa viene SEMPRE SCRITTA

ConfigurationBuilder builder = new();   //istanzia nuovo costruttore della configurazione

builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); //dice al costruttore che c'è un file JSON nella directory del progetto

IConfigurationRoot configuration = builder.Build(); // costruisce la configurazione del progetto; Level=Off e TraceError, TraceWarning e gli altri due sono tutti a false

TraceSwitch ts = new(
    displayName: "PacktSwitch",
    description: "This switch is set via a JSON config.");  //crea un TraceSwitch

configuration.GetSection("PacktSwitch").Bind(ts);   //legge il valore di PacktSwitch dal file json e lo imposta nella configurazione del progetto; ora Level = quello letto dal file json ed i Tracexxxxxxx sono su true fino a quel livello

Trace.WriteLineIf(ts.TraceError, "Trace error");
Trace.WriteLineIf(ts.TraceWarning, "Trace warning");
Trace.WriteLineIf(ts.TraceInfo, "Trace info");
Trace.WriteLineIf(ts.TraceVerbose, "Trace verbose");
