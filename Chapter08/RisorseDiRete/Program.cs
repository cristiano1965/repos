using static System.Console;
using System.Net; // IPHostEntry, DNS, IPaddress
using System.Net.NetworkInformation; // Ping, PingReply, IPStatus

Write("Enter a valid web address: ");
string? url = ReadLine();

if (string.IsNullOrWhiteSpace(url)) 
{
    url = "https://stackoverflow.com/search?q=securestring";
}

Uri uri = new(url); //spacchetta l'url 

WriteLine($"URL: {url}");
WriteLine($"Scheme: {uri.Scheme}");
WriteLine($"Port: {uri.Port}");
WriteLine($"Host: {uri.Host}");
WriteLine($"Path: {uri.AbsolutePath}");
WriteLine($"Query: {uri.Query}");

IPHostEntry entry = Dns.GetHostEntry(uri.Host);
WriteLine($"{entry.HostName} ha i seguenti indirizzi IP; ");
foreach (IPAddress indirizzo in entry.AddressList) 
{
    WriteLine($"{indirizzo} ({indirizzo.AddressFamily})");

}

try 
{
    Ping ping = new();
    WriteLine("Pinging server. Please wait...");
    PingReply rispostaPing = ping.Send(uri.Host);

    WriteLine($"{uri.Host} was pinged and replied: {rispostaPing.Status}");
    if (rispostaPing.Status == IPStatus.Success) 
    {
        WriteLine($"Reply from {rispostaPing.Address} took {rispostaPing.RoundtripTime:N0}ms");
    }
}
catch(Exception ex)
{
    WriteLine($"{ex.GetType().ToString()} dice {ex.Message}");
}
