using Codecool.MarsExploration.MapExplorer.Exploration.Analysis;
using Codecool.MarsExploration.MapExplorer.Exploration.Movement;
using Codecool.MarsExploration.MapExplorer.Exploration.Scanning;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service;

public class SimulatorEngine
{
    private List<ILogger> LoggingOptions = new List<ILogger>();

    public SimulatorEngine(IEnumerable<ILogger> loggers)
    {
        foreach (var option in loggers)
        {
            LoggingOptions.Add(option);
        }
    }

    public void RunStep(SimulationContext context, IMovementEngine movementEngine, IScanner scanner, IOutcomeAnalyzer analyzer)
    {
        string stepMessage;
        string[] results = new []{RunMovement(context,movementEngine), RunScanning(context,scanner), RunAnalyzer(context, analyzer)};
        stepMessage = string.Join("\n", results);
            
        //log
        foreach (var option in LoggingOptions) 
        { 
            option.Log(stepMessage);
        }
            
        //increment step
        context.Steps++;
    }

    public string RunMovement(SimulationContext context, IMovementEngine movementEngine)
    {
        movementEngine.Move(context);
        string result =  $"STEP {context.Steps}; EVENT {movementEngine.ToString()}; UNIT rover-{context.Rover.Id}; POSITION [{context.Rover.CurrentPosition.X},{context.Rover.CurrentPosition.Y}]";

        return result;
    }

    public string RunScanning(SimulationContext context, IScanner scanner)
    {
        var alreadyFoundResources = new Dictionary<string, int>();
        foreach (var pair in context.Rover.FoundResources)
        {
            alreadyFoundResources.Add(pair.Key,pair.Value.Count);
        }
        scanner.Scan(context);
        var difference = new Dictionary<string, int>();
        foreach (var symbol in context.Symbols)
        {
            difference.Add(symbol, context.Rover.FoundResources[symbol].Count - alreadyFoundResources[symbol]);
        }

        string result = $"STEP {context.Steps}; EVENT {scanner.ToString()}; UNIT rover-{context.Rover.Id}; RESOURCES: {string.Join(", ",difference.Select(pair => $"[{pair.Key}: {pair.Value}]"))}";

        return result;
    }

    public string RunAnalyzer(SimulationContext context, IOutcomeAnalyzer analyzer)
    {
        string result;
        analyzer.Analyze(context);
        
        if (context.Outcome is not null)
        {
            result = $"STEP {context.Steps}; EVENT outcome; UNIT rover-{context.Rover.Id}; OUTCOME {context.Outcome}";

        }
        else
        {
            result = $"STEP {context.Steps}; EVENT {analyzer.ToString()}; UNIT rover-{context.Rover.Id};";
        }

        return result;
    }
}