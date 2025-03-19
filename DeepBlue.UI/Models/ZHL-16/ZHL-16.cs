using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DeepBlue.Models.ZHL_16;

public class Zhl16 : IAlgorithm
{
    public string Name { get; }
    public List<DiveLevel> DiveLevels { get; set; }
    public List<Gas> GasList { get; set; }
    
    public List<Compartment> Compartments { get; set; }
    public int GfLow { get; set; }
    public int GfHigh { get; set; }
    double Atm { get; set; }
    double Watervapour { get; set; }
    
    public Zhl16(int gflow, int gfhigh, List<Gas> gasList, double atm, double watervapour)
    {
        Name = "ZHL-16";
        Compartments = new List<Compartment>();
        GasList = gasList;
        Atm = atm;
        Watervapour = watervapour;
        // For debug purposes TODO: Remove
        gasList.Add(new Gas("21/35", 0.35, 0.21, false, 232));
        gasList.Add(new Gas("50", 0, 0.50, true, 232));
        
        DiveLevels = new List<DiveLevel>();
        DiveLevels.Add(new DiveLevel(50, 20, gasList[0], false));
        
        ImportCompartments("A:\\DeepBlue\\DeepBlue.UI\\Models\\ZHL-16\\Compartments.csv");
        
        GfLow = gflow;
        GfHigh = gfhigh;
        
        Initialize();
        CalculateDive();
    }

    public void CalculateDive()
    {
        DiveLevel previousLevel = new DiveLevel(0, 0, GasList[0], false);
        foreach (DiveLevel level in DiveLevels)
        {
            if (level.depth > previousLevel.depth)
            {
                CalculateDescent(level);
            }
            else
            {
                var ascentCeiling = CalculateAscentCeiling();
                if (level.depth > ascentCeiling)
                {
                    CalculateAscent();
                }
                else
                {
                    CalculateDecompression();
                }
            }
        }
    }
    
    public void Initialize()
    {
        foreach (var compartment in Compartments)
        {
            compartment.PN2 = 0.79 * (Atm - Watervapour);
            compartment.PHe = 0;
            compartment.PTotal = compartment.PN2 + compartment.PHe;
        }
    }

    public DiveLevel GetDiveLevel(List<DiveLevel> diveLevels)
    {
        throw new NotImplementedException();
    }

    public void CalculateDescent(DiveLevel diveLevel, int decentRate = 18)
    {
        var t = diveLevel.depth / decentRate;

        foreach (var compartment in Compartments)
        {
            // Nitrogen loading
            var po = compartment.PN2;
            var pio = (diveLevel.depth - Watervapour) * compartment.PN2;
            var r = decentRate * compartment.PN2;
            var k = Math.Log(2)/compartment.HalfTimeN2;
            compartment.PN2 = pio + r * (t - 1 / k) - (pio - po - r / k) * Math.Pow(Math.E, (-k * t)) ;
            
            // Helium loading
            po = compartment.PHe;
            pio = (diveLevel.depth - Watervapour) * compartment.PHe;
            k = Math.Log(2) / compartment.HalfTimeHe;

            compartment.PHe = pio + r * (t - 1 / k) - (pio - po - r / k) * Math.Pow(Math.E, (-k * t));
        }

        CalculateAtDepth(diveLevel);
    }

    public void CalculateAtDepth(DiveLevel diveLevel)
    {
        foreach (var compartment in Compartments)
        {
            var pi = (diveLevel.depth - Watervapour) * compartment.PN2;
            var po = compartment.PN2;
            compartment.PN2 = po + (pi - po) * (1 - Math.Pow(2, -diveLevel.time / compartment.HalfTimeN2));
            
            pi = (diveLevel.depth - Watervapour) * compartment.PHe;
            po = compartment.PHe;
            compartment.PHe = po + (pi - po) * (1 - Math.Pow(2, -diveLevel.time / compartment.HalfTimeHe));
        }
    }

    public void CalculateAscent()
    {
        throw new NotImplementedException();
    }

    public double CalculateAscentCeiling(bool rounding = false)
    {
        List<double> ascentCeilings = new List<double>();
        foreach (var compartment in Compartments)
        {
            var a = ((compartment.AN2 * compartment.PN2) + (compartment.AHe * compartment.PHe)) /
                (compartment.PN2 + compartment.PHe);
            var b = ((compartment.BN2 * compartment.PN2) + (compartment.BHe * compartment.PHe)) /
                (compartment.PN2 + compartment.PHe);
            ascentCeilings.Add((compartment.PN2 + compartment.PHe - a) / b);
        }
        if (rounding)
        {
            return Math.Round(ascentCeilings.Max(), 1);
        }
        return ascentCeilings.Max();
    }

    public DiveLevel CalculateNoDecoLimit()
    {
        throw new NotImplementedException();
    }

    public DiveLevel CalculateNdl()
    {
        throw new NotImplementedException();
    }

    public void DetermineSuitableGas()
    {
        throw new NotImplementedException();
    }
    
    public void CalculateDecompression()
    {
        throw new NotImplementedException();
    }


    private void ImportCompartments(string filename)
    {
        if (!File.Exists(filename))
        {
            throw new FileNotFoundException($"File {filename} not found.");
        }

        using (var reader = new StreamReader(filename))
        {
            var headerLine = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                
                Compartment compartment = new Compartment(
                    double.Parse(values[0]),
                    double.Parse(values[1]),
                    double.Parse(values[2]),
                    double.Parse(values[3]),
                    double.Parse(values[4]),
                    double.Parse(values[5])
                );
                Compartments.Add(compartment);
            }
        }
    }
}

public class Compartment
{
    public double HalfTimeN2 { get; set; }
    public double HalfTimeHe { get; set; }
    public double AN2 { get; set; }
    public double BN2 { get; set; }
    public double AHe { get; set; }
    public double BHe { get; set; }
    public double PN2 { get; set; }
    public double PHe { get; set; }
    public double PTotal { get; set; }
    
    public Compartment(double halfTimeN2, double halfTimeHe, double aN2, double bN2, double aHe, double bHe, double pN2, double pHe, double pTotal)
    {
        HalfTimeN2 = halfTimeN2;
        HalfTimeHe = halfTimeHe;
        this.AN2 = aN2;
        this.BN2 = bN2;
        this.AHe = aHe;
        this.BHe = bHe;
        this.PN2 = pN2;
        this.PHe = pHe;
        this.PTotal = pTotal;
    }
    
    public Compartment(double halfTimeN2, double halfTimeHe, double aN2, double bN2, double aHe, double bHe)
    {
        HalfTimeN2 = halfTimeN2;
        HalfTimeHe = halfTimeHe;
        this.AN2 = aN2;
        this.BN2 = bN2;
        this.AHe = aHe;
        this.BHe = bHe;
    }
    
    public override string ToString()
    {
        return $"""
                Half-time N2: {HalfTimeN2}, 
                Half-time He: {HalfTimeHe}, 
                A N2: {AN2}, B N2: {BN2}, 
                A He: {AHe}, B He: {BHe}, 
                P N2: {PN2}, P He: {PHe}, P Total: {PTotal}
                """;
    }
}