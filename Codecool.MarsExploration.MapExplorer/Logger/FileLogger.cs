namespace Codecool.MarsExploration.MapExplorer.Logger;

public class FileLogger : ILogger
{
    private string outputDir;
    
    private int id = 1;

    public FileLogger(string resultsFolderPath)
    {
        outputDir = resultsFolderPath;
        
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
    }
    
    public void Log(string message)
    {
        if (message.Contains("STEP 0"))
        {
            CheckExistanceAndCreateFile();
        }

        List<string> linesInFile = File.ReadAllLines(outputDir + $"\\Results-{id}.txt").Skip(3).ToList();
        
        linesInFile.Insert(0, "Mars Exploration Results");
        linesInFile.Insert(1, "\n");
        
        linesInFile.Add(DateTime.Now.ToString());
        linesInFile.Add(message);
        linesInFile.Add("");
        
        File.WriteAllLines(outputDir + $"\\Results-{id}.txt", linesInFile);
    }

    private void CheckExistanceAndCreateFile()
    {
        if (!File.Exists(outputDir + $"\\Results-{id}.txt"))
        {
            using (StreamWriter sw = File.CreateText(outputDir + $"\\Results-{id}.txt"));
            return;
        }
        id++;
        CheckExistanceAndCreateFile();
    }
}