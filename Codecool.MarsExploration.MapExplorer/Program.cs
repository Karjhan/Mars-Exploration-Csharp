using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.ContextResults.Repository;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Exploration.Analysis;
using Codecool.MarsExploration.MapExplorer.Exploration.Movement;
using Codecool.MarsExploration.MapExplorer.Exploration.Scanning;
using Codecool.MarsExploration.MapExplorer.Exploration.Service;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer;

class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        //results database path
        string dbFile = $"{WorkDir}\\Resources\\ContextResults.db";
        
        //repository service for results database
        IContextResultsRepository repository = new ContextResultsRepository(dbFile);
        
        //generate configuration
        string mapFile = $@"{WorkDir}\Resources\exploration-2.map";
        Coordinate landingSpot = new Coordinate(20, 11);
        string[] symbols = new[] { "*", "%" };
        int stepsTillTimeout = 1000;
        SimulatorConfiguration configuration = new SimulatorConfiguration(mapFile, landingSpot, symbols, stepsTillTimeout);

        //coordinate calculator for various other services
        ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
        
        //map loader used in context generator
        IMapLoader _mapLoader = new MapLoader.MapLoader();

        //rover deployer used in context generator
        IRoverDeployer _roverDeployer = new RoverDeployer(_coordinateCalculator);

        //generate context
        ISimulationContextGenerator _simulationContextGenerator= new SimulationContextGenerator(_mapLoader, _roverDeployer);
        SimulationContext context = _simulationContextGenerator.Generate(configuration);
        
        //loggers (options for logging simulation steps)
        ILogger consoleLogger = new ConsoleLogger();
        
        string outputDir = $"{WorkDir}\\LoggedResults";
        ILogger fileLogger = new FileLogger(outputDir);
        ILogger[] loggers = new[] { consoleLogger, fileLogger };
        
        //Analyzing service
        IAnalyzer HighResourceColonizationAnalyzer = new HighResourceColonizationAnalyzer();
        IAnalyzer LackOfResourceAnalyzer = new LackOfResourcesAnalyzer(80);
        IAnalyzer TimeoutAnalyzer = new TimeoutAnalyzer();
        IAnalyzer WaterNearShipColonizationAnalyzer = new WaterNearShipColonizationAnalyzer(_coordinateCalculator);

        IOutcomeAnalyzer outcomeAnalyzer = new OutcomeAnalyzer();
        outcomeAnalyzer.Attach(TimeoutAnalyzer);
        outcomeAnalyzer.Attach(HighResourceColonizationAnalyzer);
        outcomeAnalyzer.Attach(LackOfResourceAnalyzer);
        
        //Scanning service
        IScanner scanner = new Scanner(_coordinateCalculator);
        
        //Movement service
        IMoveRoutine SmartExploreRoutine = new SmartExploreRoutine(_coordinateCalculator);
        IMoveRoutine RandomExploreRoutine = new RandomExploreRoutine(_coordinateCalculator);
        IMoveRoutine QuickReturnRoutine = new QuickReturnRoutine(_coordinateCalculator);
        IMoveRoutine TeleportReturnRoutine = new TeleportReturnRoutine(_coordinateCalculator);

        IMovementEngine movementEngine = new MovementEngine();
        movementEngine.SetRoutine(SmartExploreRoutine);

        //simulation engine that generates and logs steps
        SimulatorEngine simulationEngine = new SimulatorEngine(loggers);
        
        //simulate
        while (context.Outcome is null)
        {
            simulationEngine.RunStep(context, movementEngine, scanner, outcomeAnalyzer);
        }

        movementEngine.SetRoutine(QuickReturnRoutine);
        outcomeAnalyzer.Detach(HighResourceColonizationAnalyzer);
        outcomeAnalyzer.Detach(LackOfResourceAnalyzer);
        while (context.Outcome == ExplorationOutcome.Colonizable && !_coordinateCalculator
                   .GetAdjacentCoordinates(context.SpaceshipPosition, context.Map.Dimension, 1)
                   .Contains(context.Rover.CurrentPosition))
        {
            simulationEngine.RunStep(context, movementEngine, scanner, outcomeAnalyzer);
        }

        foreach (var logger in loggers)
        {
            logger.Log(context.ToString());
        }
        
        repository.Add(context.Steps-1, context.StepsTillTimeout, context.Rover.FoundResources.Select(pair => pair.Value.Count).Sum(),context.Outcome.ToString());
    }
}
