using Codecool.MarsExploration.MapExplorer.Configuration.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service;

public interface ISimulationContextGenerator
{
    public SimulationContext Generate(SimulatorConfiguration config);
}