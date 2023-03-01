namespace Codecool.MarsExploration.MapExplorer.Exploration.Analysis;

public class OutcomeAnalyzer : IOutcomeAnalyzer
{
    public SimulationContext CurrentContext { get; set; }

    private List<IAnalyzer> Analyzers = new List<IAnalyzer>();

    public void Analyze(SimulationContext context)
    {
        CurrentContext = context;
        Notify();
    }

    public void Attach(IAnalyzer observer)
    {
        Analyzers.Add(observer);
    }

    public void Detach(IAnalyzer observer)
    {
        Analyzers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in Analyzers)
        {
            observer.Update(this);
        }
    }
    
    public override string ToString()
    {
        return "analysis";
    }
}