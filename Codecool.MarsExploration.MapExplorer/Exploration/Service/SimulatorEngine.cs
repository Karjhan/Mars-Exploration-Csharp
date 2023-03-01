using Codecool.MarsExploration.MapExplorer.Exploration.Analysis;
using Codecool.MarsExploration.MapExplorer.Exploration.Movement;
using Codecool.MarsExploration.MapExplorer.Exploration.Scanning;
using Codecool.MarsExploration.MapExplorer.Logger;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service;

public class SimulatorEngine
{
    private IEnumerable<ILogger> LoggingOptions;




    public void RunStep(SimulationContext context, IMovementEngine movementEngine, IScanner scanner, IOutcomeAnalyzer analyzer)
    {
        string stepMessage = "";
        
        

        
        
    }

    public string RunAndLogMovement(SimulationContext context, IMovementEngine movementEngine)
    {
        movementEngine.Move(context);
        string result =  $"STEP {context.Steps}; EVENT {movementEngine.ToString()}; UNIT rover-{context.Rover.Id}; POSITION [{context.Rover.CurrentPosition.X},{context.Rover.CurrentPosition.Y}]\n";

        return result;
    }

    public string RunAndLogScanning(SimulationContext context, IScanner scanner)
    {
        var alreadyFoundResources = context.Rover.FoundResources;
        scanner.Scan(context);
        context.Rover.FoundResources
    }
}