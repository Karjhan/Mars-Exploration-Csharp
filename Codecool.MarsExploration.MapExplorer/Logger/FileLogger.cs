namespace Codecool.MarsExploration.MapExplorer.Logger;

public class FileLogger : ILogger
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    
    private string outputDir = $"{WorkDir}\\LoggedResults";
    
    public void Log(string message)
    {
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        
        List<string> linesInFile = File.ReadAllLines(outputDir + "\\Results.txt").Skip(1).ToList();
        
        linesInFile.Insert(0, "Mars Exploration Results");
        linesInFile.Insert(1, "\n");
        
        linesInFile.Add(DateTime.Now.ToString());
        linesInFile.Add(message);
        linesInFile.Add("");
        
        File.WriteAllLines(outputDir + "\\Results.txt", linesInFile);
    }
}