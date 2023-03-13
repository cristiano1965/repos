using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packt.Shared;

public abstract class Shape
{
    // fields che possono essere ereditati dalle classi derivate
    protected double height;
    protected double width;

    // properties
    public virtual double Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
        }
    }

    public virtual double Width
    {
        get
        {
            return width;
        }
        set
        {
            width = value;
        }
    }

    // Area must be implemented by derived classes
    // as a read-only property
    public abstract double Area { get; }
}

public class Rectangle : Shape  //implementazione della classe astratta Shape
{
    public Rectangle(double height, double width) 
    {
        Height = height;
        Width = width;
    }
    public override double Area 
    {
        get {
            return Height * Width;
        }
    }
}

public class Square : Rectangle //implementazione della classe reale Rectangle derivandone campi, prorpietà e metodi
{
    public Square(double width) : base(   //nel costruttore della classe base, il rettangolo ha due dimensioni quindi impostiamo, per Square, che l'unico parametro che riceviamo imposterà sia height che width
        height: width, 
        width: width)
    { }

    // qui non c'è alcun setter, quindi viene usato quello della classe base (cioè Rectangle); avremmo potuto scriverli comunque così, cioè impostando il "value" ricevuto sia nell'height che nel width
    /*
     * public override double Height
        {
        set
        {
            height = value;
            width = value;
        }
        }

        public override double Width
        {
        set
        {
            height = value;
            width = value;
        }
        }
     * 
     */

}

public class Circle : Shape
{
    public Circle(double radius)
    {
        // altezza ed ampiezza di un cerchio = diametro, cioè raggio * 2
        Height = radius * 2;
        Width = radius * 2;
    }
    public override double Area
    {
        // area di un cerchio = raggio * raggio * 3,14, ma l'area non riceve il raggio, come parametro, quindi prendiamo altezza e ampiezza e le dividiamo * 2
        get
        {
            return Height/2  * Width/2 * Math.PI;
        }
    }
}

