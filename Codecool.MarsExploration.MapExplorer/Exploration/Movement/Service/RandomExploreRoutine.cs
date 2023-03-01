using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Movement;

public class RandomExploreRoutine : IMoveRoutine
{
    public ICoordinateCalculator CoordinateCalculator { get; }
    
    private Random rand = new Random();

    public RandomExploreRoutine(ICoordinateCalculator coordinateCalculator)
    {
        CoordinateCalculator = coordinateCalculator;
    }
    
    public Coordinate GetNextMove(SimulationContext context)
    {
        if (CanMove(context))
        {
            Coordinate[] possibleMoves = CoordinateCalculator.GetAdjacentCoordinates(context.Rover.CurrentPosition,context.Map.Dimension,1)
                .Where(coordinate => context.Map.IsEmpty(coordinate)).ToArray();

            return possibleMoves[rand.Next(possibleMoves.Length)];
        }

        return context.Rover.CurrentPosition;
    }
    
    public bool CanMove(SimulationContext context)
    {
        IEnumerable<Coordinate> possibleMoves = CoordinateCalculator.GetAdjacentCoordinates(context.Rover.CurrentPosition,context.Map.Dimension,1)
            .Where(coordinate => context.Map.IsEmpty(coordinate));

        return possibleMoves.Count() > 0;
    }
}