using System;
using Avalonia.Controls.Platform;

namespace DeepBlue.Models;

/// <summary>
/// A class to represent a gas mixture that a diver might use.
/// </summary>
public class Gas
{
    /// <summary>
    /// Visual representation of the gas mixture. E.g. 18/45, 50%, etc.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// A decimal representation of the percentage of helium in the mixture. E.g. 0.45 for 45%.
    /// </summary>
    public double He { get; set; }
    /// <summary>
    /// A decimal representation of the percentage of oxygen in the mixture. E.g. 0.18 for 18%.
    /// </summary>
    public double O2 { get; set; }
    /// <summary>
    /// A decimal representation of the percentage of nitrogen in the mixture. E.g. 0.37 for 37%. Calculated as 1 - He - O2.
    /// </summary>
    public double N2 { get; set; }
    /// <summary>
    /// A boolean to indicate whether the gas mixture is a decompression gas.
    /// </summary>
    public bool IsDeco { get; set; }
    
    /// <summary>
    /// Maximum Operating Depth of the gas.
    /// </summary>
    public double MaxOperatingDepth { get; set; }
    
    /// <summary>
    /// An integer to represent the pressure of the gas mixture in the tank.
    /// </summary>
    public int Pressure { get; set; }
    
    /// <summary>
    /// Override constructor for manually adding a mixture. DO NOT USE UNLESS FOR TESTING.
    /// </summary>
    /// <param name="name">Name of the tank. E.g. "18/45", "Nx50", "Air" etc.</param>
    /// <param name="he">Percentage of helium. E.g. 0.45 = 45%</param>
    /// <param name="o2">Percentage of oxygen. E.g. 0.18 = 18%</param>
    /// <param name="n2">Percentage of nitrogen. E.g. 0.37 = 37%</param>
    /// <param name="isDeco">Determines whether the gas should only be used for decompression.</param>
    /// <param name="pressure">Pressure of the tank.</param>
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

    /// <summary>
    /// Default constructor for creating a gas mixture. N2 is calculated as 1 - He - O2.
    /// </summary>
    /// <param name="name">Name of the tank. E.g. "18/45", "Nx50", "Air" etc.</param>
    /// <param name="he">Percentage of helium. E.g. 0.45 = 45%</param>
    /// <param name="o2">Percentage of oxygen. E.g. 0.18 = 18%</param>
    /// <param name="isDeco">Determines whether the gas should only be used for decompression.</param>
    /// <param name="pressure">Pressure of the tank.</param>
    public Gas(string name, double he, double o2, bool isDeco, int pressure)
    {
        Name = name;
        He = he;
        O2 = o2;
        N2 = 1 - he - o2;
        IsDeco = isDeco;
        Pressure = pressure;
        MaxOperatingDepth = CalculateMaxOperatingDepth();
        ValidateTank();
    }

    /// <summary>
    /// Validates a tank to ensure that the sum of He, O2, and N2 is 1. Throws an exception if the sum is not 1.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the tank is not valid.</exception>
    private void ValidateTank()
    {
        if (He + O2 + N2 == 1)
        {
            return;
        }
        throw new ArgumentException("Invalid tank. The sum of He, O2, and N2 must be 100.");
    }

    public double CalculateMaxOperatingDepth(float po2 = 1.6f)
    {
        return ((po2 * 10) / O2) - 10;
    }
    
    /// <summary>
    /// Override for the ToString method to display the gas mixture in a human-readable format.
    /// </summary>
    /// <returns>ToString.</returns>
    public override string ToString()
    {
        return $"Name: {Name}, " +
               $"   He: {He}, O2: {O2}, N2: {N2}, " +
               $"   IsDeco: {IsDeco}, Pressure: {Pressure}";
    }
}