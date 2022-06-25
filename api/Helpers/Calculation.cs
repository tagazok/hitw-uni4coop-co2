using System;

namespace HITW.Function;

public static class Calculation
{
    public static decimal GoVeggie()
    {
        return 4.0m;
    }

    public static decimal TakePublicTransportationOrBicycle(int distanceInKm)
    {
        return 110m * distanceInKm * 0.67m / 1000;
    }

    public static decimal TakeShowerInsteadOfBath()
    {
        return 1.1m;
    }

    public static decimal ReusePlasticBag()
    {
        return 0.2m;
    }

    public static decimal TurnOffComputers()
    {
        return 0.2m;
    }

    public static decimal TurnDownThermostats()
    {
        return 1.0m;
    }

    public static decimal Recycling()
    {
        return 2.0m;
    }
    
    public static decimal Donation()
    {
        return 1.0m;
    }
}