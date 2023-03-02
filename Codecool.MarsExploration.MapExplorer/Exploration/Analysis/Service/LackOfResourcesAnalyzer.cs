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
        
        int alreadyExploredPlacesCount = subject.CurrentContext.Rover.VisitedPlaces.Count();
        int allEmptyPlacesOnMapCount = subject.CurrentContext.Map.Count(null);
        double minimumPlacesToExplore = ((double)MinimumPercentageToExplore / (double)100) * allEmptyPlacesOnMapCount;
        
        if (alreadyExploredPlacesCount > minimumPlacesToExplore)
        {
            subject.CurrentContext.Outcome = ExplorationOutcome.Error;
        }
    }
}