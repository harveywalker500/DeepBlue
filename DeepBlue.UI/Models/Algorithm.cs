using System.Collections.Generic;

namespace DeepBlue.Models;

public interface IAlgorithm
{
    string Name { get; }
    List<DiveLevel> DiveLevels { get; set; }
    List<Gas> GasList { get; set; }

    public void Initialize();
    public DiveLevel GetDiveLevel(List<DiveLevel> diveLevels);
    public void CalculateDescent(DiveLevel diveLevel, Gas gas, double descentRate);
    public void CalculateAtDepth(DiveLevel diveLevel, Gas gas);
    public void CalculateAscent();
    public double CalculateAscentCeiling(bool rounding);
    public DiveLevel CalculateNoDecoLimit(DiveLevel diveLevel);
    public void DetermineSuitableGas();
}