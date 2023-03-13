using static System.Console;

WriteLine($"There are {args.Length} arguments.");

foreach (string arg in args) {
    WriteLine(arg);
}

if (args.Length < 3)
{
    WriteLine("Devi specificare due colori e ampiezza cursore, esempio:  red yellow 50");
    return; // stop running
}

ForegroundColor = (ConsoleColor)Enum.Parse(
    enumType: typeof(ConsoleColor),
    value: args[0],
    ignoreCase: true
    );
BackgroundColor = (ConsoleColor)Enum.Parse(
    enumType: typeof(ConsoleColor),
    value: args[1],
    ignoreCase: true
    );

try
{
    CursorSize = int.Parse(args[2]);
}
catch (PlatformNotSupportedException) {
    WriteLine("The current platform doesn't support changing the size of the cursor.");
}