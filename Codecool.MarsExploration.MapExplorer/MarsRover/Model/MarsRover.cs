using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Model;

public record MarsRover(int id, Coordinate currentPosition, int sight, Dictionary<string, IEnumerable<Coordinate>> foundResources);