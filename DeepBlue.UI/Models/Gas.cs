using System;
using Avalonia.Controls.Platform;

namespace DeepBlue.Models;

public class Gas
{
    public string Name { get; set; }
    public double He { get; set; }
    public double O2 { get; set; }
    public double N2 { get; set; }
    public bool IsDeco { get; set; }
    public int Pressure { get; set; }
    
    public Gas(string name, float he, float o2, float n2, bool isDeco, int pressure)
    {
        Name = name;
        He = he;
        O2 = o2;
        N2 = n2;
        IsDeco = isDeco;
        Pressure = pressure;
        ValidateTank();
    }

    public Gas(string name, double he, double o2, bool isDeco, int pressure)
    {
        Name = name;
        He = he;
        O2 = o2;
        N2 = 1 - he - o2;
        IsDeco = isDeco;
        Pressure = pressure;
        ValidateTank();
    }

    public void ValidateTank()
    {
        if (He + O2 + N2 == 1)
        {
            return;
        }
        throw new ArgumentException("Invalid tank. The sum of He, O2, and N2 must be 100.");
    }
    
    public override string ToString()
    {
        return $"Name: {Name}, " +
               $"   He: {He}, O2: {O2}, N2: {N2}, " +
               $"   IsDeco: {IsDeco}, Pressure: {Pressure}";
    }
}