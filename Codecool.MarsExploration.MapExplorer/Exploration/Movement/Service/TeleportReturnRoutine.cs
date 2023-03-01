using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Movement;

public class TeleportReturnRoutine : IMoveRoutine
{
    public ICoordinateCalculator CoordinateCalculator { get; }
    
    private Random rand = new Random();

    public TeleportReturnRoutine(ICoordinateCalculator coordinateCalculator)
    {
        CoordinateCalculator = coordinateCalculator;
    }
    public Coordinate GetNextMove(SimulationContext context)
    {
        var possibleReturnSpots = CoordinateCalculator
            .GetAdjacentCoordinates(context.SpaceshipPosition, context.Map.Dimension, 1)
            .Where(coordinate => context.Map.IsEmpty(coordinate)).ToArray();

        return possibleReturnSpots[rand.Next(possibleReturnSpots.Length)];
    }
}