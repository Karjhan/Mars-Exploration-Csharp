using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Service;

public class RoverDeployer : IRoverDeployer
{
    public Model.MarsRover Deploy(Map map, int id, Coordinate startingPlace, int sight)
    {
        Model.MarsRover result = new Model.MarsRover(id,startingPlace,sight,new Dictionary<string, IEnumerable<Coordinate>>());
        map.Representation[startingPlace.X, startingPlace.Y] = result.ToString();
        return result;
    }
}