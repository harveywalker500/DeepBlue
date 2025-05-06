using DeepBlue.Models;
using DeepBlue.Models.ZHL_16;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeepBlue.Tests;

[TestClass]
public class ZHL_16Test
{
    Zhl16 zhl16_test;
    List<DiveLevel> diveLevels;
    List<Gas> gasList;

    [TestInitialize]
    public void Setup()
    {
        diveLevels = new List<DiveLevel>();

        gasList = new List<Gas>();
        gasList.Add(new Gas("18/45", 0.45, 0.18, false, 232));
        gasList.Add(new Gas("50%", 0, 0.50, false, 232));

        diveLevels.Add(new DiveLevel(50, 25, gasList[0], false));

        zhl16_test = new Zhl16(100, 100, gasList, new List<DiveLevel>(), 1, 0.0567);
    }
    
    [TestMethod]
    public void TestInitialize()
    {
        zhl16_test.Initialize();
        
        Assert.AreEqual(0.745207, zhl16_test.Compartments[0].PN2, 1e-5);
    }
    
    [TestMethod]
    public void TestDescent()
    {
        zhl16_test.CalculateDescent(diveLevels[0], gasList[0], 18);
        
        Assert.AreEqual(1.524415037, zhl16_test.Compartments[0].PN21e-5);
    }

    [TestMethod]
    public void TestAtDepth()
    {
        zhl16_test.CalculateAtDepth(diveLevels[0], gasList[0]);

        Assert.AreEqual(2.153589312, zhl16_test.Compartments[0].PN2, 1e-5);
    }

    [TestMethod]
    public void TestAscentCeiling()
    {
        zhl16_test.CalculateDescent(diveLevels[0], gasList[0], 18);
        zhl16_test.CalculateAtDepth(diveLevels[0], gasList[0]);
        
        var ceiling = zhl16_test.CalculateAscentCeiling(false);
        Assert.IsTrue(ceiling >= 10 && ceiling <= 20);
    }
}