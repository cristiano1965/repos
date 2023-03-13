using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Packt.Shared;

public interface IPlayable
{
    void Play();
    void Pause();

    void Stop() // default interface implementation;
    {
        WriteLine("Default implementation of stop");
    }

    void Stop2() // default interface implementation (ma non viene implemetata da dvdplayer)
    {
        WriteLine("Default implementation of stop2");
    }
}
