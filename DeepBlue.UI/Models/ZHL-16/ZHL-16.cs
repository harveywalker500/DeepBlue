using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DeepBlue.Models.ZHL_16;

/// <summary>
/// An implementation of the ZHL-16 algorithm for decompression planning.
/// Adapted from http://www.lizardland.co.uk/DIYDeco.html
/// </summary>
public class Zhl16 : IAlgorithm
{
    /// <summary> Visual name of the algorithm. </summary>
    public string Name { get; }
    
    /// <summary> Gets or sets a List of dive levels for the dive plan. </summary>
    public List<DiveLevel> DiveLevels { get; set; }
    
    /// <summary> List of gasses to be used for the dive plan. </summary>
    public List<Gas> GasList { get; set; }

    /// <summary> List of compartments for the algorithm. </summary>
    public List<Compartment> Compartments { get; set; }
    
    /// <summary> Gradient Factor low. </summary>
    public int GfLow { get; set; }
    
    /// <summary> Gradient Factor high. </summary>
    public int GfHigh { get; set; }
    
    /// <summary> Initial atmosphere for initializing the algorithm. Can be used to represent altitude.</summary>
    double Atm { get; set; }
    
    /// <summary> Amount of water vapour in lungs. Dependent on temperature. See [1]. </summary>
    double Watervapour { get; set; }

    /// <summary>
    /// Constructor for the ZHL-16 algorithm.
    /// </summary>
    /// <param name="gflow">Gradient Factor low.</param>
    /// <param name="gfhigh">Gradient Factor high.</param>
    /// <param name="gasList">A list of gasses to be used for a dive plan.</param>
    /// <param name="diveLevels">A list of dive levels to be used for planning the dive.</param>
    /// <param name="atm">Initial atmosphere for initializing the algorithm. Can be used to represent altitude.</param>
    /// <param name="watervapour">Amount of water vapour in lungs. Dependent on temperature. See [1].</param>
    public Zhl16(int gflow, int gfhigh, List<Gas> gasList, List<DiveLevel> diveLevels, double atm = 1, double watervapour = 0.0618)
    {
        Name = "ZHL-16";
        Compartments = new List<Compartment>();
        GasList = gasList;
        DiveLevels = diveLevels;
        Atm = atm;
        Watervapour = watervapour;

        Compartments = ImportCompartments("/Users/harveywalker/RiderProjects/DeepBlue/DeepBlue.UI/Models/ZHL-16/Compartments.csv");

        GfLow = gflow;
        GfHigh = gfhigh;

        Initialize();
    }

    /// <summary>
    /// Overall dive calculation method for the dive levels provided.
    /// </summary>
    public void CalculateDive()
    {
        DiveLevel previousLevel = new DiveLevel(0, 0, GasList[0], false);
        List<DiveLevel> divePlan = new List<DiveLevel>(DiveLevels);

        foreach (DiveLevel level in divePlan)
        {
            Console.WriteLine($"Next level: {level.depth}m for {level.time} mins.");
            if (level.ATM > previousLevel.depth)
            {
                 CalculateDescent(level, GasList[0]);
            }
            else
            {
                var ascentCeiling = CalculateAscentCeiling();
                if (level.ATM > ascentCeiling)
                {
                    CalculateDescent(level, GasList[0], -9);
                }
                else
                {
                    CalculateDecompression();
                }
            }
            CalculateAtDepth(level, GasList[0]);
            previousLevel = level;
        }
        // Add a surface level at the end of the dive plan and calculates decompression
        divePlan.Add(new DiveLevel(0, 0, GasList[0], true));
        CalculateDecompression();
    }

    /// <summary>
    /// Initialize all tissues to the surface pressure.
    /// </summary>
    public void Initialize()
    {
        foreach (var compartment in Compartments)
        {
            compartment.PN2 = 0.79 * (Atm - Watervapour);
            compartment.PHe = 0;
            compartment.PTotal = compartment.PN2 + compartment.PHe;
        }
    }
    
    /// <summary>
    /// Calculates descent for each tissue given a dive level.
    /// </summary>
    /// <param name="diveLevel">Dive level for the calculation</param>
    /// <param name="gas">Gas used for the descent.</param>
    /// <param name="descentRate">Descent rate used.</param>
    public void CalculateDescent(DiveLevel diveLevel, Gas gas, double descentRate = 18)
    {
        double pressureRate = descentRate / 10.0; // Bar per minute
        double t = diveLevel.ATM / descentRate; // Time in minutes

        foreach (var compartment in Compartments)
        {
            // Nitrogen loading
            double pio = (diveLevel.ATM - Watervapour) * gas.N2;
            double r = pressureRate * gas.N2;
            double k = Math.Log(2) / compartment.HalfTimeN2;

            compartment.PN2 = pio + r * (t - 1 / k)
                              - (pio - compartment.PN2 - r / k) * Math.Exp(-k * t);

            // Helium loading
            pio = (diveLevel.ATM - Watervapour) * gas.He;
            r = pressureRate * gas.He;
            k = Math.Log(2) / compartment.HalfTimeHe;

            compartment.PHe = pio + r * (t - 1 / k)
                              - (pio - compartment.PHe - r / k) * Math.Exp(-k * t);

            compartment.PTotal = compartment.PN2 + compartment.PHe;
        }

        Console.WriteLine($"Descent calculated for {diveLevel.depth}m.");
    }

    /// <summary>
    /// Calculates compartment loading during the bottom time of a dive level.
    /// </summary>
    /// <param name="diveLevel">Dive level to be calculated.</param>
    /// <param name="gas">Gas to be used.</param>
    public void CalculateAtDepth(DiveLevel diveLevel, Gas gas)
    {
        foreach (var compartment in Compartments)
        {
            var po = compartment.PN2;
            var pi = (diveLevel.ATM - Watervapour) * gas.N2;
            compartment.PN2 = po + (pi - po) * (1 - Math.Pow(2, -diveLevel.time / compartment.HalfTimeN2));

            pi = (diveLevel.ATM - Watervapour) * gas.He;
            po = compartment.PHe;
            compartment.PHe = po + (pi - po) * (1 - Math.Pow(2, -diveLevel.time / compartment.HalfTimeHe));

            compartment.PTotal = compartment.PN2 + compartment.PHe;
        }

        Console.WriteLine($"Depth {diveLevel.depth}m calculated for {diveLevel.time} minutes.");
    }

    /// <summary>
    /// Calculates the ascent ceiling for the current compartment loading.
    /// </summary>
    /// <param name="rounding">Should the result be rounded?</param>
    /// <param name="inMetres">Should the result be in metres instead of ATM?</param>
    /// <returns>Depth of the ascent ceiling in ATM/metres.</returns>
    public double CalculateAscentCeiling()
    {
        double criticalCeiling = 0;
        foreach (var compartment in Compartments)
        {
            double totalLoading = compartment.PN2 + compartment.PHe;
            if (totalLoading <= 0)
                continue;

            // Interpolate a and b coefficients based on current loadings.
            double a = ((compartment.AN2 * compartment.PN2) + (compartment.AHe * compartment.PHe)) / totalLoading;
            double b = ((compartment.BN2 * compartment.PN2) + (compartment.BHe * compartment.PHe)) / totalLoading;

            double tissueCeiling = (totalLoading - a) * b;
            if (tissueCeiling > criticalCeiling)
                criticalCeiling = tissueCeiling;
        }
        return criticalCeiling;
    }

    /// <summary>
    /// Calculates the no-decompression limit for a given dive level.
    /// </summary>
    /// <param name="diveLevel"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Determines the most suitable gas for a list of gases based on it's Maximum Operating Depth.
    /// </summary>
    /// <param name="depth">Depth to evaluate most suitable gas.</param>
    /// <param name="gasList">Gases to be evaluated.</param>
    /// <returns>The most suitable gas diven the depth.</returns>
    public Gas DetermineSuitableGas(double depth, List<Gas> gasList)
    {
        return GasList
            .Where(g => g.IsDeco && (depth <= g.MaxOperatingDepth))
            .OrderByDescending(g => g.O2) // Prefer highest O2 first
            .FirstOrDefault()  ?? GasList[0];
    }

    /// <summary>
    /// Calculates the decompression stops for the dive plan.
    /// </summary>
    public void CalculateDecompression()
    {
        // Get the first stop and round up to nearest 3m
        double pressureCeiling = CalculateAscentCeiling();
        double depthCeiling = (pressureCeiling - Atm) * 10.0;
        var firstStop = Math.Floor(Math.Round(depthCeiling / 3) * 3);
        var currentStop = firstStop;

        const double lastStop = 3;
        const double stopInterval = 3;

        Console.WriteLine($"Starting decompression from {firstStop}m");
        DiveLevels.Remove(DiveLevels.Last());
        
        while (currentStop >= lastStop)
        {
            Gas decoGas = DetermineSuitableGas(currentStop, GasList);
            Console.WriteLine($"Using gas {decoGas.Name} at {currentStop}m stop");

            // Create a dive level for this stop
            DiveLevel stopLevel = new DiveLevel(currentStop, 0, decoGas, true);

            int stopLength = 0;
            double nextStop = currentStop - (stopInterval / 10);

            // Keep calculating tissue loadings until the ceiling drops below the next stop
            while (true)
            {
                // Calculate tissues for 1 minute at this stop
                CalculateAtDepth(new DiveLevel(currentStop, 1, decoGas, true), decoGas);
                stopLength++;

                // Check if the ascent ceiling allows moving to the next stop
                double ceiling = (CalculateAscentCeiling() - Atm) * 10.0;
                Console.WriteLine($"After {stopLength} min at {currentStop}m, ceiling: {ceiling}m");

                // Break if we can move to the next stop or safety limit reached
                if (ceiling <= nextStop)
                {
                    break;
                }
            }

            // Update the stop time and add to dive plan
            stopLevel.time = stopLength;
            DiveLevels.Add(stopLevel);

            Console.WriteLine($"Completed {stopLength} min stop at {currentStop}m");

            // Move to the next stop
            currentStop -= stopInterval;
        }

        // Add a final stop at the surface
        DiveLevels.Add(new DiveLevel(0, 0, GasList[0], true));
        Console.WriteLine("Decompression calculation complete");
    }

    /// <summary>
    /// Imports the compartment values from a CSV file.
    /// </summary>
    /// <param name="filename">Directory of the file.</param>
    /// <returns>A list of compartments.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the selected file is not found.</exception>
    private List<Compartment> ImportCompartments(string filename)
    {
        List<Compartment> DecoCompartments = new List<Compartment>();
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
                DecoCompartments.Add(compartment);
            }
        }
        return DecoCompartments;
    }
}

/// <summary>
/// Reperesents a compartment in the ZHL-16 algorithm.
/// </summary>
public class Compartment
{
    /// <summary> Half-time for nitrogen in the compartment. </summary>
    public double HalfTimeN2 { get; set; }
    
    /// <summary> Half-time for helium in the compartment. </summary>
    public double HalfTimeHe { get; set; }
    
    /// <summary> A coefficient for nitrogen in the compartment. </summary>
    public double AN2 { get; set; }
    
    /// <summary> B coefficient for nitrogen in the compartment. </summary>
    public double BN2 { get; set; }
    
    /// <summary> A coefficient for helium in the compartment. </summary>
    public double AHe { get; set; }
    
    /// <summary> B coefficient for helium in the compartment. </summary>
    public double BHe { get; set; }
    
    /// <summary> Nitrogen pressure in the compartment. </summary>
    public double PN2 { get; set; }
    
    /// <summary> Helium pressure in the compartment. </summary>
    public double PHe { get; set; }
    
    /// <summary> Total pressure in the compartment. </summary>
    public double PTotal { get; set; }

    /// <summary>
    /// Default constructor for the compartment. 
    /// </summary>
    /// <param name="halfTimeN2">Half-time for nitrogen.</param>
    /// <param name="halfTimeHe">Half-time for helium.</param>
    /// <param name="aN2">A coefficient for nitrogen in the compartment.</param>
    /// <param name="bN2">B coefficient for nitrogen in the compartment</param>
    /// <param name="aHe">A coefficient for helium in the compartment</param>
    /// <param name="bHe">B coefficient for nitrogen in the compartment</param>
    /// <param name="pN2">Nitrogen pressure in the compartment.</param>
    /// <param name="pHe">Helium pressure in the compartment.</param>
    /// <param name="pTotal">Total pressure in the compartment.</param>
    public Compartment(double halfTimeN2, double halfTimeHe, double aN2, double bN2, double aHe, double bHe, double pN2,
        double pHe, double pTotal)
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

    /// <summary>
    /// Alternative constructor for use cases where the compartment is not initialized. Used when initializing the algorithm.
    /// </summary>
    /// <param name="halfTimeN2">Half-time for nitrogen.</param>
    /// <param name="halfTimeHe">Half-time for helium.</param>
    /// <param name="aN2">A coefficient for nitrogen in the compartment.</param>
    /// <param name="bN2">B coefficient for nitrogen in the compartment</param>
    /// <param name="aHe">A coefficient for helium in the compartment</param>
    /// <param name="bHe">B coefficient for nitrogen in the compartment</param>
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