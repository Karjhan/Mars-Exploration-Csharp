using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Model;

public record SimulatorConfiguration(string mapFile, Coordinate landingSpot, IEnumerable<string> symbols, int stepsTillTimeout);