using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Service;

public class RoverDeployer : IRoverDeployer
{
    private int id = 1;
    
    private ICoordinateCalculator _coordinateCalculator;

    public RoverDeployer(ICoordinateCalculator coordinateCalculator)
    {
        _coordinateCalculator = coordinateCalculator;
    }
    public Model.MarsRover Deploy(Map map, Coordinate landingSpot, int sight)
    {
        Random rand = new Random();
        
        Coordinate[] possibleStartingSpots = _coordinateCalculator.GetAdjacentCoordinates(landingSpot, map.Dimension, 1)
            .Where(coordinate => map.Representation[coordinate.X, coordinate.Y] is null).ToArray();
        Coordinate startingPlace = possibleStartingSpots[rand.Next(possibleStartingSpots.Length)];
        
        Model.MarsRover result = new Model.MarsRover(id,sight,startingPlace);
        map.Representation[startingPlace.X, startingPlace.Y] = result.ToString();

        id++;
        return result;
    }
}