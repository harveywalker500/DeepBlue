using System.Collections.Generic;

namespace DeepBlue.Models;

public interface IAlgorithm
{
    string Name { get; }
    List<DiveLevel> DiveLevels { get; set; }
    List<Gas> GasList { get; set; }

    public void Initialize();
    public void CalculateDescent(DiveLevel diveLevel, Gas gas, double descentRate);
    public void CalculateAtDepth(DiveLevel diveLevel, Gas gas);
    public double CalculateAscentCeiling(bool rounding, bool inMetres);
    public DiveLevel CalculateNoDecoLimit(DiveLevel diveLevel);
    public Gas DetermineSuitableGas(double depth, List<Gas> gasList);
}