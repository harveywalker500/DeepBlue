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
    
    public Zhl16(int gflow, int gfhigh, List<Gas> gasList, List<DiveLevel> diveLevels, double atm, double watervapour)
    {
        Name = "ZHL-16";
        Compartments = new List<Compartment>();
        GasList = gasList;
        DiveLevels = diveLevels;
        Atm = atm;
        Watervapour = watervapour;
        
        ImportCompartments("A:\\DeepBlue\\DeepBlue.UI\\Models\\ZHL-16\\Compartments.csv");
        
        GfLow = gflow;
        GfHigh = gfhigh;
        
        Initialize();
    }

    public void CalculateDive()
    {
        DiveLevels.Add(new DiveLevel(0, 0, GasList[0], true));
        DiveLevel previousLevel = new DiveLevel(0, 0, GasList[0], false);
        List<DiveLevel> divePlan = new List<DiveLevel>(DiveLevels); 

        foreach (DiveLevel level in divePlan)
        {
            if (level.depth > previousLevel.depth)
            {
                CalculateDescent(level, GasList[0]);
            }
            else
            {
                CalculateDecompression();
                /*var ascentCeiling = CalculateAscentCeiling();
                if (level.depth > ascentCeiling)
                {
                    CalculateAscent();
                }
                else
                {
                    CalculateDecompression();
                }*/
            }
            
            previousLevel = level;
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

    public void CalculateDescent(DiveLevel diveLevel, Gas gas, double descentRate = 18)
    {
        double t = diveLevel.depth / descentRate;

        foreach (var compartment in Compartments)
        {
            // Nitrogen loading
            var po = compartment.PN2;
            var pio = (diveLevel.depth - Watervapour) * gas.N2;
            var r = descentRate * gas.N2;
            var k = Math.Log(2) / compartment.HalfTimeN2;

            var term1 = pio + r * (t - 1 / k);
            var term2 = pio - po - r / k;
            compartment.PN2 = term1 - term2 * Math.Exp(-k * t);
            
            // Helium loading
            po = compartment.PHe;
            pio = (diveLevel.depth - Watervapour) * gas.He;
            r = descentRate * gas.He;
            k = Math.Log(2) / compartment.HalfTimeHe;

            compartment.PHe = pio + r * (t - (1 / k)) - (pio - po - (r / k)) * Math.Exp(-k * t);
            
            compartment.PTotal = compartment.PN2 + compartment.PHe;
        }
        Console.WriteLine($"Descent calculated for {diveLevel.depth}m.");
        //CalculateAtDepth(diveLevel, gas);
    }

    public void CalculateAtDepth(DiveLevel diveLevel, Gas gas)
    {
        foreach (var compartment in Compartments)
        {
            var po = compartment.PN2;
            var pi = (diveLevel.depth - Watervapour) * gas.N2;
            compartment.PN2 = po + (pi - po) * (1 - Math.Pow(2, -diveLevel.time / compartment.HalfTimeN2));
            
            pi = (diveLevel.depth - Watervapour) * gas.He;
            po = compartment.PHe;
            compartment.PHe = po + (pi - po) * (1 - Math.Pow(2, -diveLevel.time / compartment.HalfTimeHe));
            
            compartment.PTotal = compartment.PN2 + compartment.PHe;
        }
        Console.WriteLine($"Depth {diveLevel.depth}m calculated for {diveLevel.time} minutes.");
    }

    public void CalculateAscent()
    {
        foreach (var compartment in Compartments)
        {
            
        }
    }

    public double CalculateAscentCeiling(bool rounding = false)
    {
        List<double> ascentCeilings = new List<double>();

        foreach (var compartment in Compartments)
        {
            var aTerm1 = ((compartment.AN2 * compartment.PN2) + (compartment.AHe * compartment.PHe));
            var a = aTerm1 / (compartment.PN2 + compartment.PHe);

            var bTerm1 = ((compartment.BN2 * compartment.PN2) + (compartment.BHe * compartment.PHe));
            var b = bTerm1 / (compartment.PN2 + compartment.PHe);

            double ceiling = (compartment.PN2 + compartment.PHe - a) * b; // atm
            ascentCeilings.Add(ceiling);
        }

        var maxAscentCeiling = ascentCeilings.Max(); // atm
        

        if (rounding)
        {
            Console.WriteLine($"Ascent Ceiling: {Math.Round(maxAscentCeiling, 2)} m.");
            return Math.Round(maxAscentCeiling, 2);
        }

        Console.WriteLine($"Ascent Ceiling: {maxAscentCeiling} m.");
        return maxAscentCeiling;
    }

    

    public DiveLevel CalculateNoDecoLimit(DiveLevel diveLevel)
    {
        diveLevel.time = 1;
        if (Compartments.Count == 0)
        {
            Initialize();
        }
        CalculateDescent(diveLevel, GasList[0]);
        while (CalculateAscentCeiling() < 0.1)
        {
            diveLevel.time++;
            CalculateAtDepth(diveLevel, GasList[0]);
        }
        return diveLevel;
    }
    public void DetermineSuitableGas()
    {
        throw new NotImplementedException();
    }
    
    public void CalculateDecompression()
    {
        double firstStop = CalculateAscentCeiling(true);
        double lastStop = 3.0; // Last stop depth in meters
        double stopInterval = 3.0; // Interval between stops in meters

        for (double currentStop = firstStop; currentStop >= lastStop; currentStop -= stopInterval)
        {
            Gas decoGas = GasList.Find(gas => gas.IsDeco);
            int stopLength = 0;

            while (CalculateAscentCeiling(true) >= currentStop - stopInterval)
            {
                DiveLevel stopLevel = new DiveLevel((int)currentStop, 1, decoGas, true);
                CalculateAtDepth(stopLevel, GasList[0]);
                stopLength++;
            }
            // Add deco stop to DiveLevels
            DiveLevels.Add(new DiveLevel((int)currentStop, stopLength, decoGas, true));
            Console.WriteLine($"Stop at {currentStop}m for {stopLength} minutes.");
        }
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