using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Packt.Shared;

public class DvdPlayer : IPlayable
{
    public void Pause()  //implementa interfaccia
    {
        WriteLine("DVD Player in PAUSA");
    }

    public void Play() //implementa interfaccia
    {
        WriteLine("DVD Player in PLAY");
    }

    public void Stop2() //implementa interfaccia
    {
        WriteLine("DVD Player in STOP2");
    }

    public void Stop3() //specifica della classe DVDPlayer
    {
        WriteLine("DVD Player in STOP2");
    }
}
