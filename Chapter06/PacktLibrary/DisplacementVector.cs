using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packt.Shared;

public record struct DisplacementVector
{
    public  int X;
    public  int Y;

    public DisplacementVector(int initialX, int initialY) 
    {
        X = initialX;
        Y = initialY;
    }

    public static   DisplacementVector operator +( // metodo che usa l'operatore "+" ed effettua dv1 + dv2 
        DisplacementVector vector1,
        DisplacementVector vector2
        )
    {
        return new( //ritorna una nuova istanza di questo tipo di struttura formata dalla somma dei due X (delle due struct ricevute) e dalla somma delle due Y (delle due struct ricevute)
            vector1.X + vector2.X,
            vector1.Y + vector2.Y
            );


    }

    public  static DisplacementVector operator +(DisplacementVector vector1, int valore) //overload del metodo precedente che effettua dv1 + (int)
    {
         vector1.X += valore;
         vector1.Y += valore;

        return new( //ritorna una nuova istanza di questo tipo di struttura formata dalla somma del numero ricevuto sia sulla X sia sulla Y
            vector1.X,
            vector1.Y 
            );

    }

} 
