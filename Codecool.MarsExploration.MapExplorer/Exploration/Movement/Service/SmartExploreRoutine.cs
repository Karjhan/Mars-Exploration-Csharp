using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Movement;

public class SmartExploreRoutine : IMoveRoutine
{
    public ICoordinateCalculator CoordinateCalculator { get; }

    private HashSet<Coordinate> ExploredPositions = new HashSet<Coordinate>();

    private Random rand = new Random();

    public SmartExploreRoutine(ICoordinateCalculator coordinateCalculator)
    {
        CoordinateCalculator = coordinateCalculator;
    }
    
    public Coordinate? GetNextMove(SimulationContext context)
    {
        if (CanMove(context))
        {
            Coordinate[] possibleMoves = CoordinateCalculator.GetAdjacentCoordinates(context.Rover.CurrentPosition,context.Map.Dimension,1)
                .Where(coordinate => context.Map.IsEmpty(coordinate)).ToArray();

            Coordinate[] visitedSpots = possibleMoves.Where(coordinate => ExploredPositions.Contains(coordinate)).ToArray();
            Coordinate[] unvisitedSpots = possibleMoves.Where(coordinate => !ExploredPositions.Contains(coordinate)).ToArray();

            if (unvisitedSpots.Length > 0)
            {
                Coordinate choice = unvisitedSpots[rand.Next(unvisitedSpots.Length)];
                ExploredPositions.Add(choice);
                return choice;
            }

            return visitedSpots[rand.Next(visitedSpots.Length)];
        }

        return context.Rover.CurrentPosition;
    }

    private bool CanMove(SimulationContext context)
    {
        IEnumerable<Coordinate> possibleMoves = CoordinateCalculator.GetAdjacentCoordinates(context.Rover.CurrentPosition,context.Map.Dimension,1)
            .Where(coordinate => context.Map.IsEmpty(coordinate));

        return possibleMoves.Count() > 0;
    }
}