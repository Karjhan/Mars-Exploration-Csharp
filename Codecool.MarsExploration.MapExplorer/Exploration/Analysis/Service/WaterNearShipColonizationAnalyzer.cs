using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Analysis;

public class WaterNearShipColonizationAnalyzer : IAnalyzer
{
    private ICoordinateCalculator _coordinateCalculator;
    
    public WaterNearShipColonizationAnalyzer(ICoordinateCalculator coordinateCalculator)
    {
        _coordinateCalculator = coordinateCalculator;
    }
    public void Update(IOutcomeAnalyzer subject)
    {
        if (subject.CurrentContext.Outcome is not null)
        {
            return;
        }

        if (subject.CurrentContext.Rover.FoundResources["*"].Where(coordinate =>
                _coordinateCalculator.ShortestRoute(subject.CurrentContext.Map, coordinate,
                    subject.CurrentContext.SpaceshipPosition).Count() < 10).Count() >= 2)
        {
            subject.CurrentContext.Outcome = ExplorationOutcome.Colonizable;
        }

        
    }
}