using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace MapExplorationTests;

public class MapLoaderTests
{
    private static IMapLoader _mapLoader = new MapLoader();
    
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    private static readonly object[] DimensionCases =
    {
        new object[]
        {
            $"{WorkDir}\\MapsForTests\\test-0.map", 32
        },
        new object[]
        {
            $"{WorkDir}\\MapsForTests\\test-1.map", 32
        },
        new object[]
        {
            $"{WorkDir}\\MapsForTests\\test-2.map", 30
        }
    };
    
    private static readonly object[] SymbolCases =
    {
        new object[]
        {
            $"{WorkDir}\\MapsForTests\\test-0.map", 
            new (string,int)[]
            {
                ("#",70),
                ("&",28),
                ("%",10),
                ("*",10)
            }
        },
        new object[]
        {
            $"{WorkDir}\\MapsForTests\\test-1.map",
            new (string,int)[]
            {
                ("#",70),
                ("&",20),
                ("%",10),
                ("*",10)
            }
        },
        new object[]
        {
            $"{WorkDir}\\MapsForTests\\test-2.map",
            new (string,int)[]
            {
                ("%",10),
                ("*",10)
            }
        }
    };
    
    
    

    [TestCaseSource(nameof(DimensionCases))]
    public void Load_Returns_CorrectDimension(string mapFile, int expected)
    {
        //Arrange + Act
        Map result = _mapLoader.Load(mapFile);
        int actual = result.Dimension;
        
        //Assert
        Assert.AreEqual(expected,actual);
    }

    [TestCaseSource(nameof(SymbolCases))]
    public void Load_Returns_CorrectAmountOfSymbols(string mapFile, (string,int)[] expectedSymbolsAndAmounts)
    {
        //Arrange+Act
        Map result = _mapLoader.Load(mapFile);
        int[] actual = expectedSymbolsAndAmounts.Select(tuple => result.Count(tuple.Item1)).ToArray();
        
        //Assert
        for (int i = 0; i < expectedSymbolsAndAmounts.Length; i++)
        {
            Assert.AreEqual(expectedSymbolsAndAmounts[i].Item2, actual[i]);
        }
    }
}