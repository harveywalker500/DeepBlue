using System.Collections.Generic;

namespace DeepBlue.Models;

public interface IAlgorithm
{
    string Name { get; }
    List<DiveLevel> DiveLevels { get; set; }
    List<Gas> GasList { get; set; }

    public void Initialize(List<DiveLevel> diveLevels);
    public DiveLevel GetDiveLevel(List<DiveLevel> diveLevels);
    public void CalculateDescent();
    public void CalculateAtDepth();
    public void CalculateAscent();
    public DiveLevel CalculateAscentCeiling();
    public DiveLevel CalculateNDL();
    public void DetermineSuitableGas();
}