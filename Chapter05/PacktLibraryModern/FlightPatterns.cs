namespace Packt.Shared; //sintassi del namespace per c# 10

public class BusinessClassPassenger
{
    public override string ToString()
    {
        return $"Business Class";
    }
}
public class FirstClassPassenger
{
    public int AirMiles { get; set; }


    public override string ToString()
    {
        return $"First Class con {AirMiles:N0} miglia aeree";
    }
}

public class CoachPassenger
{
    public double CarryOnKG { get; set; }


    public override string ToString()
    {
        return $"Coach Class con {CarryOnKG:N2} KG di bagaglio";
    }
}

