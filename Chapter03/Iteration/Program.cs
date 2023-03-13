using static System.Console;

string? password;
int trial = 1;
Boolean boolGiusta = false;

do
{
    
    Write($" Tentativo ({trial}) di 10 - Inserisci la password: ");
    password = ReadLine();

    if (password == "Pa$$w0rd") {
        WriteLine("Giusta !");
        boolGiusta = true;
        break;
    }
    trial++;

}
while (trial <= 10);

if (!boolGiusta)
    WriteLine("Tentativi falliti!");