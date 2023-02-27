using Codecool.MarsExploration.MapExplorer.Configuration.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Service.Validator;

public interface IConfigurationValidator
{
    public bool Validate(SimulatorConfiguration configuration);
}