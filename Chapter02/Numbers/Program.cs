/*
// unsigned integer means positive whole number or 0
uint naturalNumber = 23;

// integer means negative or positive whole number or 9
int integerNumber = -23;

// float means single-precision floating point
// F suffix makes it a float literal
float realNumber = 2.3F;

// double means double-precision floating point
double anotherRealNumber = 2.3; //double literal

// 3 variabili che memorizzano il numero 2 milioni
int decimalNotation = 2_000_000;
int binaryNotation = 0b_0001_1110_1000_0100_1000_0000;
int hexNotation = 0x_001E_8480;

//controlla che le 3 variabili contengano lo stesso valore
Console.WriteLine($"{decimalNotation == binaryNotation}");
Console.WriteLine($"{decimalNotation == hexNotation}");
*/

/*
Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range {int.MinValue:N0} to {int.MaxValue:N0}.");
Console.WriteLine($"double uses {sizeof(double)} bytes and can store numbers in the range {double.MinValue:N0} to {double.MaxValue:N0}.");
Console.WriteLine($"decimal uses {sizeof(decimal)} bytes and can store numbers in the range {decimal.MinValue:N0} to {decimal.MaxValue:N0}.");
*/


Console.WriteLine("Using doubles:");
double a = 0.1;
double b = 0.2;

if (a + b == 0.3)
{
    Console.WriteLine($"{a} + {b} è uguale a {0.3}");
}
else {
    Console.WriteLine($"{a} + {b} non è uguale a {0.3}");
}


Console.WriteLine("Using decimals:");
decimal c = 0.1M; // il suffisso M indica un letterale decimale
decimal d = 0.2M;

if (c + d == 0.3M)
{
    Console.WriteLine($"{c} + {d} è uguale a {0.3}");
}
else
{
    Console.WriteLine($"{c} + {d} non è uguale a {0.3}");
}
