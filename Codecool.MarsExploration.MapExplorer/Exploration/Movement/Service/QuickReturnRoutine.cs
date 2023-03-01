using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Movement;

public class QuickReturnRoutine : IMoveRoutine
{
    public ICoordinateCalculator CoordinateCalculator { get; }

    private Coordinate[] returnRoute;

    private int stepInRoute = 0;

    public QuickReturnRoutine(ICoordinateCalculator coordinateCalculator)
    {
        CoordinateCalculator = coordinateCalculator;
    }
    
    public Coordinate GetNextMove(SimulationContext context)
    {
        if (stepInRoute == 0)
        {
            returnRoute = CoordinateCalculator.ShortestRoute(context.Map, context.Rover.CurrentPosition, context.SpaceshipPosition).ToArray();
        }

        stepInRoute++;
        if (returnRoute[stepInRoute] == context.SpaceshipPosition)
        {
            return returnRoute[returnRoute.Length-1];
        }
        return returnRoute[stepInRoute];
    }
}