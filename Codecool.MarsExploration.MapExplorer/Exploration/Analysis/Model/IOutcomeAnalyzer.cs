namespace Codecool.MarsExploration.MapExplorer.Exploration.Analysis;

public interface IOutcomeAnalyzer
{
    SimulationContext CurrentContext { get; set; }
    
    void Analyze(SimulationContext context);

    // Attach an observer to the subject.
    void Attach(IAnalyzer observer);

    // Detach an observer from the subject.
    void Detach(IAnalyzer observer);

    // Notify all observers about an event.
    void Notify();
}