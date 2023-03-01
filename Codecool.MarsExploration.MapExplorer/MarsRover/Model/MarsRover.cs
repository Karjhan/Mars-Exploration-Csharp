using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Model;

public record MarsRover()
{
    public int Id { get; init; }
    public int Sight { get; init; }
    public Dictionary<string, HashSet<Coordinate>> FoundResources { get; init; } = new Dictionary<string, HashSet<Coordinate>>();
    public HashSet<Coordinate> VisitedPlaces { get; init; } = new HashSet<Coordinate>();
    public Coordinate CurrentPosition { get; set; }

    public MarsRover(int id, int sight, Coordinate startingPosition, IEnumerable<string> symbols) : this()
    {
        Id = id;
        Sight = sight;
        CurrentPosition = startingPosition;
        foreach (var symbol in symbols)
        {
            FoundResources[symbol] = new HashSet<Coordinate>();
        }
    }
    
    public override string ToString()
    {
        return "R";
    }
};