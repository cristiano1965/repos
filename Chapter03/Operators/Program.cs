using static System.Console;

int a = 3;
int b = a++; //prima b=4, poi a++
WriteLine($"a is {a}, b is {b}");

int c = 3;
int d = ++c; //prima c++, poi c assegnato a d
WriteLine($"c is {c}, d is {d}");
