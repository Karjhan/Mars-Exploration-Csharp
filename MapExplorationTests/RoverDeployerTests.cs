using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace MapExplorationTests;

public class RoverDeployerTests
{
    private static IRoverDeployer _roverDeployer = new RoverDeployer();
    private static IMapLoader _mapLoader = new MapLoader();
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly Map map = _mapLoader.Load($"{WorkDir}\\MapsForTests\\test-0.map");
    private static readonly MarsRover rover = _roverDeployer.Deploy(map, 1, new Coordinate(7, 27), 5);

    [Test]
    public void Deploy_PlacesRoverOnMap()
    {
        Assert.AreEqual(rover.ToString(),map.Representation[7,27]);
    }

    [Test]
    public void Deploy_GivesCorrectId()
    {
        Assert.AreEqual(1,rover.id);
    }

    [Test]
    public void Deploy_GivesCorrectSight()
    {
        Assert.AreEqual(5,rover.sight);
    }

    [Test]
    public void Deploy_StartsWithEmptyResources()
    {
        Assert.AreEqual(0,rover.foundResources.Count);
    }
}