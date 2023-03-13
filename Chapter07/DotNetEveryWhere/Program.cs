using static System.Console;

WriteLine("Io posso girare dovunque!");

WriteLine($"Versione del Sistema Operativo: {Environment.OSVersion}");
if (OperatingSystem.IsMacOS())
{
    WriteLine("Sono su MacOS.");
}
else if (OperatingSystem.IsWindowsVersionAtLeast(major: 10))
{
    WriteLine("Sono su Windows 10 o 11.");
}
else if (OperatingSystem.IsLinux())
{
    WriteLine("Sono su Linux.");
}
else
{
    WriteLine("Sono in qualche altro misterioso Sistema Operativo.");
}

WriteLine("Premere ENTER per stopparmi.");
ReadLine();
