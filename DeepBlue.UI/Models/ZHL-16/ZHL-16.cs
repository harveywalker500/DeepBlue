using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Path = Avalonia.Controls.Shapes.Path;

namespace DeepBlue.Models.ZHL_16;

public class Zhl16 : IAlgorithm
{
    public string Name { get; }
    public List<DiveLevel> DiveLevels { get; set; }
    public List<Gas> GasList { get; set; }
    
    public List<Compartment> Compartments { get; set; }
    public int GfLow { get; set; }
    public int GfHigh { get; set; }
    
    public Zhl16(int gflow, int gfhigh)
    {
        Name = "ZHL-16";
        Compartments = new List<Compartment>();
        ImportCompartments("A:\\DeepBlue\\DeepBlue.UI\\Models\\ZHL-16\\Compartments.csv");
        GfLow = gflow;
        GfHigh = gfhigh;
    }
    
    public void Initialize(List<DiveLevel> diveLevels)
    {
        throw new System.NotImplementedException();
    }

    public DiveLevel GetDiveLevel(List<DiveLevel> diveLevels)
    {
        throw new System.NotImplementedException();
    }

    public void CalculateDescent()
    {
        throw new System.NotImplementedException();
    }

    public void CalculateAtDepth()
    {
        throw new System.NotImplementedException();
    }

    public void CalculateAscent()
    {
        throw new System.NotImplementedException();
    }

    public DiveLevel CalculateAscentCeiling()
    {
        throw new System.NotImplementedException();
    }

    public DiveLevel CalculateNDL()
    {
        throw new System.NotImplementedException();
    }

    public void DetermineSuitableGas()
    {
        throw new System.NotImplementedException();
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