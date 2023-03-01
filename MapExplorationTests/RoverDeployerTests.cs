using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace MapExplorationTests;

public class RoverDeployerTests
{
    private static ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
    private static IRoverDeployer _roverDeployer = new RoverDeployer(_coordinateCalculator);
    private static IMapLoader _mapLoader = new MapLoader();
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly Map map = _mapLoader.Load($"{WorkDir}\\MapsForTests\\test-0.map");
    private static readonly MarsRover rover = _roverDeployer.Deploy(map, new Coordinate(7, 27), 5, new []{"*","^"});

    [Test]
    public void Deploy_PlacesRoverOnMap()
    {
        Assert.AreEqual(rover.CurrentPosition,new Coordinate(7,28));
    }

    [Test]
    public void Deploy_GivesCorrectId()
    {
        Assert.AreEqual(1,rover.Id);
    }

    [Test]
    public void Deploy_GivesCorrectSight()
    {
        Assert.AreEqual(5,rover.Sight);
    }

    [Test]
    public void Deploy_StartsWithEmptyResources()
    {
        string[] resources = new[] { "*", "^" };
        Assert.AreEqual(2,rover.FoundResources.Count);
    }
}