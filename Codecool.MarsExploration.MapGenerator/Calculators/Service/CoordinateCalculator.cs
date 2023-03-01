using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapGenerator.Calculators.Service;

public class CoordinateCalculator : ICoordinateCalculator
{
    private static readonly Random Random = new();

    public Coordinate GetRandomCoordinate(int dimension)
    {
        return new Coordinate(
            Random.Next(dimension),
            Random.Next(dimension)
        );
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(Coordinate coordinate, int dimension, int reach = 1)
    {
        var adjacent = new[]
        {
            coordinate with { Y = coordinate.Y + reach },
            coordinate with { Y = coordinate.Y - reach },
            coordinate with { X = coordinate.X + reach },
            coordinate with { X = coordinate.X - reach },

        };

        return adjacent.Where(c => c.X >= 0 && c.Y >= 0 && c.X < dimension && c.Y < dimension);
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(IEnumerable<Coordinate> coordinates, int dimension)
    {
        return coordinates.SelectMany(c => GetAdjacentCoordinates(c, dimension));
    }

    public IEnumerable<Coordinate> ShortestRoute(Map map, Coordinate from, Coordinate where)
    {
        var queue = new Queue<Coordinate>();
        var visited = new HashSet<Coordinate>();
        var distance = new Dictionary<Coordinate, int>();
        var previous = new Dictionary<Coordinate, Coordinate>();

        queue.Enqueue(from);
        visited.Add(from);
        distance[from] = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.X == where.X && current.Y == where.Y)
            {
                var path = new List<Coordinate>();
                while (previous.ContainsKey(current))
                {
                    path.Insert(0, current);
                    current = previous[current];
                }
                path.Insert(0, current);
                return path;
            }

            var neighbors = GetAdjacentCoordinates(current,map.Dimension,1);
            foreach (var neighbor in neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    distance[neighbor] = distance[current] + 1;
                    previous[neighbor] = current;
                }
            }
        }

        return new List<Coordinate>(){from,where};
    }
    
    // public int ShortestRouteDistance(Map map, Coordinate from, Coordinate where)
    // {
    //     var queue = new Queue<Coordinate>();
    //     var visited = new HashSet<Coordinate>();
    //     var distance = new Dictionary<Coordinate, int>();
    //     
    //     queue.Enqueue(from);
    //     visited.Add(from);
    //     
    //     while (queue.Count > 0)
    //     {
    //         var current = queue.Dequeue();
    //         if (current.X == where.X && current.Y == where.Y)
    //         {
    //             return distance[current];
    //         }
    //
    //         var neighbors = GetAdjacentCoordinates(current, map.Dimension, 1).Where(coordinate => map.IsEmpty(coordinate));
    //         foreach (var neighbor in neighbors)
    //         {
    //             if (!visited.Contains(neighbor))
    //             {
    //                 queue.Enqueue(neighbor);
    //                 visited.Add(neighbor);
    //                 distance[neighbor] = distance[current] + 1;
    //             }
    //         }
    //     }
    //
    //     return -1;
    // }
}
