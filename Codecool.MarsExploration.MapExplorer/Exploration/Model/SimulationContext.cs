using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public record SimulationContext()
{
    public int StepsTillTimeout { get; init; }
    public MarsRover.Model.MarsRover Rover { get; init; }
    public Coordinate SpaceshipPosition { get; init; }
    public Map Map { get; init; }
    public IEnumerable<string> Symbols { get; init; }
    public ExplorationOutcome? Outcome { get; set; }
    public int Steps { get; set; } = 0;

    public SimulationContext(int stepsTillTimeout, MarsRover.Model.MarsRover rover, Coordinate spaceshipPosition, Map map, IEnumerable<string> symbols) : this()
    {
        StepsTillTimeout = stepsTillTimeout;
        Rover = rover;
        SpaceshipPosition = spaceshipPosition;
        Map = map;
        Symbols = symbols;
    }
};