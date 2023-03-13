using static System.Console;
using System.Diagnostics;

Stopwatch watch = new();
Write("Press ENTER to start. ");
ReadLine();
watch.Start();

int max = 45;

IEnumerable<int> numbers = Enumerable.Range(1, max); //numbers: una lista di interi da 1 a 45

WriteLine($"Calculating Fibonacci sequence up to {max}. Please wait...");

int[] fibonacciNumbers = numbers.AsParallel().Select(n => Fibonacci(n)).OrderBy(n=>n).ToArray();

watch.Stop();

WriteLine("Trascorsi {0:#,##0} millisecondi.", watch.ElapsedMilliseconds);

Write("Risultati:");
foreach(int number in fibonacciNumbers) 
{
    Write($" {number}");
}

static int Fibonacci(int term) =>

    term switch
    {
        1 => 0,
        2 => 1,
        _ => Fibonacci(term - 1) + Fibonacci(term - 2)
    };
    


