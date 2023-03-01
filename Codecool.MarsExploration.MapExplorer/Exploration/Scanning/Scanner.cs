using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Scanning;

public class Scanner : IScanner
{
    private ICoordinateCalculator _coordinateCalculator;

    public Scanner(ICoordinateCalculator coordinateCalculator)
    {
        _coordinateCalculator = coordinateCalculator;
    }
    
    public void Scan(SimulationContext context)
    {
        List<Coordinate> placesToScan = new List<Coordinate>();
        for (int i = 1; i < context.Rover.Sight; i++)
        {
            placesToScan.AddRange(_coordinateCalculator.GetAdjacentCoordinates(context.Rover.CurrentPosition,context.Map.Dimension,i));
        }

        foreach (var place in placesToScan)
        {
            string symbolInPlace = context.Map.GetByCoordinate(place);
            if (context.Symbols.Contains(symbolInPlace))
            {
                context.Rover.FoundResources[symbolInPlace].Add(place);
            }
        }
    }
    
    public override string ToString()
    {
        return "scan";
    }
}