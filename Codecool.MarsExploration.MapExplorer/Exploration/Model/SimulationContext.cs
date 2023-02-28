using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public record SimulationContext(int stepsTillTimeout, MarsRover.Model.MarsRover rover, Coordinate spaceshipPosition, Map map, IEnumerable<string> symbols, ExplorationOutcome outcome, int steps = 0);