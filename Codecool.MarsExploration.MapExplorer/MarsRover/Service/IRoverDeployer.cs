using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Service;

public interface IRoverDeployer
{
    public Model.MarsRover Deploy(Map map, Coordinate landingSpot, int sight);
}