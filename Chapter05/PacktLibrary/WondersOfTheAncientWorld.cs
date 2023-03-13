using System;
using System.Collections.Generic;
using System.Text;

namespace Packt.Shared
{
    [System.Flags] // indica che un enum può essere trattato come un bit; cioè una lista di flags -> 2 | 8 | 16 = 26 vuol dire aver scelto i giardini e tempio e mausoleo 
    
    /*
     * fino a  8 valori derivare da byte
     * fino a 16 valori derivare da ushort
     * fino a 32 valori derivare da unit
     * fin oa 64 valori derivare da ulong
     */
    public enum WondersOfTheAncientWorld : byte
    {
        None                        = 0b_0000_0000, // 0
        GrandePiramideDiGiza        = 0b_0000_0001, // 1
        GiardiniPensiliDiBabilonia  = 0b_0000_0010, // 2
        StatuaDiZeusAOlimpia        = 0b_0000_0100, // 4
        TempioDiArtemideInEfeso     = 0b_0000_1000, // 8
        MausoleoDiAlicarnasso       = 0b_0001_0000, // 16
        ColossoDiRodi               = 0b_0010_0000, // 32
        FaroDiAlessandria           = 0b_0100_0000  // 64
    }
}
