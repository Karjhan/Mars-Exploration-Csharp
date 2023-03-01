namespace Codecool.MarsExploration.MapExplorer.Exploration.Analysis;

public class HighResourceColonizationAnalyzer : IAnalyzer
{
    public void Update(IOutcomeAnalyzer subject)
    {
        if (subject.CurrentContext.Outcome is not null)
        {
            return;
        }


        foreach (var resource in subject.CurrentContext.Symbols)
        {
            if (subject.CurrentContext.Rover.FoundResources[resource].Count() < 5)
            {
                return;
            }
        }

        subject.CurrentContext.Outcome = ExplorationOutcome.Colonizable;
    }
}