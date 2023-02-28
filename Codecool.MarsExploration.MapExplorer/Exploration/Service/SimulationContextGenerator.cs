using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service;

public class SimulationContextGenerator : ISimulationContextGenerator
{
    private IMapLoader _mapLoader;

    private IRoverDeployer _roverDeployer;

    public SimulationContextGenerator(IMapLoader mapLoader, IRoverDeployer roverDeployer)
    {
        _mapLoader = mapLoader;
        _roverDeployer = roverDeployer;
    }
    
    public SimulationContext Generate(SimulatorConfiguration config)
    {
        Map map = _mapLoader.Load(config.mapFile);
        MarsRover.Model.MarsRover rover = _roverDeployer.Deploy(map, config.landingSpot, 1);
        return new SimulationContext(config.stepsTillTimeout, rover, config.landingSpot, map, config.symbols);
    }
}