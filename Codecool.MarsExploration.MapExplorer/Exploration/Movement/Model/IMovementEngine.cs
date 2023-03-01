namespace Codecool.MarsExploration.MapExplorer.Exploration.Movement;

public interface IMovementEngine
{
    public void SetRoutine(IMoveRoutine routine);
    public void Move(SimulationContext context);
}