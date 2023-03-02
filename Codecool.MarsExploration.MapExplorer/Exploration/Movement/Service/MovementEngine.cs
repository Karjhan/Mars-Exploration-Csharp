using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Movement;

public class MovementEngine : IMovementEngine
{
    private IMoveRoutine Routine { get; set; }

    public void Move(SimulationContext context)
    {
        Coordinate newPosition = Routine.GetNextMove(context);
        context.Rover.CurrentPosition = newPosition;
        context.Rover.VisitedPlaces.Add(newPosition);
    }

    public void SetRoutine(IMoveRoutine routine)
    {
        Routine = routine;
    }

    public override string ToString()
    {
        return "move";
    }
}