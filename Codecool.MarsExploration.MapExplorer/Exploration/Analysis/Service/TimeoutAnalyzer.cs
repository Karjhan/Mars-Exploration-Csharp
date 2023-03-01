namespace Codecool.MarsExploration.MapExplorer.Exploration.Analysis;

public class TimeoutAnalyzer : IAnalyzer
{
    public void Update(IOutcomeAnalyzer subject)
    {
        if (subject.CurrentContext.Outcome is not null)
        {
            return;
        }

        if (subject.CurrentContext.Steps > subject.CurrentContext.StepsTillTimeout)
        {
            subject.CurrentContext.Outcome = ExplorationOutcome.Timeout;
        }
    }
}