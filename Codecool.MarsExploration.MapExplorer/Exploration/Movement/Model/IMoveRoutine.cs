using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Movement;

public interface IMoveRoutine
{
    public ICoordinateCalculator CoordinateCalculator { get; }

    public Coordinate GetNextMove(SimulationContext context);
}