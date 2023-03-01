namespace Codecool.MarsExploration.MapExplorer.Exploration.Analysis;

public class LackOfResourcesAnalyzer : IAnalyzer
{
    private int MinimumPercentageToExplore;

    public LackOfResourcesAnalyzer(int minimumPercentageToExplore)
    {
        MinimumPercentageToExplore = minimumPercentageToExplore;
    }
    public void Update(IOutcomeAnalyzer subject)
    {
        if (subject.CurrentContext.Outcome is not null)
        {
            return;
        }

        if (subject.CurrentContext.Rover.FoundResources.Select(pair => pair.Value.Count()).Sum() >
            (MinimumPercentageToExplore / 100) * subject.CurrentContext.Symbols
                .Select(symbol => subject.CurrentContext.Map.Count(symbol)).Sum())
        {
            subject.CurrentContext.Outcome = ExplorationOutcome.Error;
        }
    }
}