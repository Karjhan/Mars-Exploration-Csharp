using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Model;

public record MarsRover()
{
    public int Id { get; init; }
    public int Sight { get; init; }
    public Dictionary<string, IEnumerable<Coordinate>> FoundResources { get; init; } = new Dictionary<string, IEnumerable<Coordinate>>();
    public Coordinate CurrentPosition { get; set; }

    public MarsRover(int id, int sight, Coordinate startingPosition) : this()
    {
        Id = id;
        Sight = sight;
        CurrentPosition = startingPosition;
    }
    
    public override string ToString()
    {
        return "R";
    }
};