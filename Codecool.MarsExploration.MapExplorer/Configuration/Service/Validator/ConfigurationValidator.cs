using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Service.Validator;

public class ConfigurationValidator : IConfigurationValidator
{
    private readonly ICoordinateCalculator _coordinateCalculator;
    private readonly IMapLoader _mapLoader;

    public ConfigurationValidator(ICoordinateCalculator coordinateCalculator, IMapLoader mapLoader)
    {
        _coordinateCalculator = coordinateCalculator;
        _mapLoader = mapLoader;
    }
    
    public bool Validate(SimulatorConfiguration configuration)
    {
        Map loadedMap = _mapLoader.Load(configuration.mapFile);

        return CheckLandingSpot(loadedMap, configuration.landingSpot) 
               && CheckSpecifiedResources(loadedMap, configuration.symbols) 
               && CheckForEmptyMap(loadedMap) 
               && configuration.stepsTillTimeout > 0;
    }

    private bool CheckLandingSpot(Map map, Coordinate landindSpot)
    {
        bool isSpaceToLand = map.GetByCoordinate(landindSpot) is null;
        bool isSpaceAround = _coordinateCalculator.GetAdjacentCoordinates(landindSpot, map.Dimension).Select(coordinate => map.GetByCoordinate(coordinate)).Any(spot => spot is null);

        return isSpaceToLand && isSpaceAround;
    }

    private bool CheckSpecifiedResources(Map map, IEnumerable<string> symbols)
    {
        bool anySymbols = symbols.Count() > 0;
        bool anySymbolsOnMap = !symbols.Select(symbol => map.Count(symbol)).Any(symbolCount => symbolCount == 0);

        return anySymbols && anySymbolsOnMap;
    }

    private bool CheckForEmptyMap(Map map)
    {
        for (int i = 0; i < map.Dimension; i++)
        {
            for (int j = 0; j < map.Dimension; j++)
            {
                if (!map.IsEmpty(new Coordinate(i, j)))
                {
                    return true;
                }
            }
        }

        return false;
    }
}