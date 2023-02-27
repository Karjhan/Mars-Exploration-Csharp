using System.Text;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapGenerator.MapElements.Model;

public record Map(string?[,] Representation, bool SuccessfullyGenerated = false)
{
    public int Dimension => Representation.GetLength(0);

    private static string CreateStringRepresentation(string?[,] arr)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < arr.GetLength(0); i++)
        {
            string s = "";
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                s += arr[i, j] == null ? " " : arr[i, j];
            }

            sb.AppendLine(s);
        }

        return sb.ToString();
    }

    public string? GetByCoordinate(Coordinate coordinate)
    {
        return Representation[coordinate.X, coordinate.Y];
    }

    public int Count(string symbol)
    {
        int count = 0;
        
        for (int i = 0; i < Dimension; i++)
        {
            for (int j = 0; j < Dimension; j++)
            {
                if (Representation[i, j] == symbol)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public bool IsEmpty(Coordinate coordinate)
    {
        return string.IsNullOrEmpty(Representation[coordinate.X, coordinate.Y])
            || Representation[coordinate.X, coordinate.Y] == " ";
    }

    public override string ToString()
    {
        return CreateStringRepresentation(Representation);
    }
}
