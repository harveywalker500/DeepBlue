using DeepBlue.Models;
using DeepBlue.Models.ZHL_16;

namespace DeepBlue.Tests;

[TestFixture]
[TestOf(typeof(Zhl16))]
public class ZHL_16Test
{
    Zhl16 zhl16_test;
    List<DiveLevel> diveLevels;
    List<Gas> gasList;

    [SetUp]
    public void Setup()
    {
        diveLevels = new List<DiveLevel>();

        gasList = new List<Gas>();
        gasList.Add(new Gas("18/45", 0.45, 0.18, false, 232));
        gasList.Add(new Gas("50%", 0, 0.50, false, 232));

        diveLevels.Add(new DiveLevel(50, 25, gasList[0], false));

        zhl16_test = new Zhl16(100, 100, gasList, new List<DiveLevel>(), 1, 0.0567);
    }
    
    [Test]
    public void TestInitialize()
    {
        zhl16_test.Initialize();
        
        Assert.That(zhl16_test.Compartments[0].PN2, Is.EqualTo(0.745207).Within(1e-5));
    }
    
    [Test]
    public void TestDescent()
    {
        zhl16_test.CalculateDescent(diveLevels[0], gasList[0], 18);
        
        Assert.That(zhl16_test.Compartments[0].PN2, Is.EqualTo(9.55864).Within(1e-5));
    }

    [Test]
    public void TestAtDepth()
    {
        zhl16_test.CalculateAtDepth(diveLevels[0], gasList[0]);

        Assert.That(zhl16_test.Compartments[0].PN2, Is.EqualTo(17.92483931).Within(1e-5));
    }

    [Test]
    public void TestAscentCeiling()
    {
        zhl16_test.CalculateDescent(diveLevels[0], gasList[0], 18);
        zhl16_test.CalculateAtDepth(diveLevels[0], gasList[0]);
        Console.WriteLine(zhl16_test.Compartments[0].PN2);
        
        Assert.That(zhl16_test.CalculateAscentCeiling(false), Is.InRange(10, 30));
    }
}