using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.Configuration.Service.Validator;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace MapExplorationTests;

public class ConfigurationValidatorTests
{
    private static ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
    private static IMapLoader _mapLoader = new MapLoader();
    private static IConfigurationValidator _configurationValidator = new ConfigurationValidator(_coordinateCalculator, _mapLoader);
    
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    
    private static readonly object[] Cases =
    {
        new object[]
        {
            new SimulatorConfiguration($"{WorkDir}\\MapsForTests\\test-0.map", new Coordinate(19,2), new []{"#", "&", "%", "*"}, 10),
            false,
            "Should return false, for non empty landing spot!"
        },
        new object[]
        {
            new SimulatorConfiguration($"{WorkDir}\\MapsForTests\\test-0.map", new Coordinate(22,17), new []{"#", "&", "%", "*"}, 10),
            false,
            "Should return false, for not empty space around landing spot!"
        },
        new object[]
        {
            new SimulatorConfiguration($"{WorkDir}\\MapsForTests\\test-0.map", new Coordinate(12,15), new List<string>(), 10),
            false,
            "Should return false, for not empty resource symbol specifier!"
        },
        new object[]
        {
            new SimulatorConfiguration($"{WorkDir}\\MapsForTests\\test-0.map", new Coordinate(12,15), new []{"#", "&", "%", "*","^"}, 10),
            false,
            "Should return false, for wrong resource in the resource specifier!"
        },
        new object[]
        {
            new SimulatorConfiguration($"{WorkDir}\\MapsForTests\\test-0.map", new Coordinate(12,15), new []{"#", "&", "%", "*"}, 0),
            false,
            "Should return false, for 0 steps until timeout of simulation!"
        },
        new object[]
        {
            new SimulatorConfiguration($"{WorkDir}\\MapsForTests\\test-4.map", new Coordinate(0,0), new []{"#", "&", "%", "*"}, 10),
            false,
            "Should return false, for empty map!"
        },
    };

    [TestCaseSource(nameof(Cases))]
    public void Validate_Returns_Correct(SimulatorConfiguration configuration, bool expected, string message)
    {
        Assert.AreEqual(expected, _configurationValidator.Validate(configuration), message);
    }
}