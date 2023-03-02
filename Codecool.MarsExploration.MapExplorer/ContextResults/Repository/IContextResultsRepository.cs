using Codecool.MarsExploration.MapExplorer.ContextResults.Model;

namespace Codecool.MarsExploration.MapExplorer.ContextResults.Repository;

public interface IContextResultsRepository
{
    void Add(int Steps, int Steps_Till_Timeout, int Total_Resources, string Outcome);
    void Update(int id, int Steps, int Steps_Till_Timeout, int Total_Resources, string Outcome);
    void Delete(int id);
    void DeleteAll();

    ContextResult Get(int id);
    IEnumerable<ContextResult> GetAll();
}