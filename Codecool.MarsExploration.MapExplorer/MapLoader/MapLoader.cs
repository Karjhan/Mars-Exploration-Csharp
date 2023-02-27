using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MapLoader;

public class MapLoader : IMapLoader
{
    public Map Load(string mapFile)
    {
        string[] rows = File.ReadAllLines(mapFile);

        string[,] representation = new string[rows.Length,rows.Length];

        for (int i = 0; i < rows.Length; i++)
        {
            for (int j = 0; j < rows[i].Length; j++)
            {
                representation[i, j] = rows[i][j].ToString() == " " ? null : rows[i][j].ToString();
            }
        }

        return new Map(representation, true);
    }
}