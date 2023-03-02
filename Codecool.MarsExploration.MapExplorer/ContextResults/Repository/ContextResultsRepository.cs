using Codecool.MarsExploration.MapExplorer.ContextResults.Model;
using Microsoft.Data.Sqlite;

namespace Codecool.MarsExploration.MapExplorer.ContextResults.Repository;

public class ContextResultsRepository : IContextResultsRepository
{
    private readonly string _dbFilePath;

    public ContextResultsRepository(string dbFilePath)
    {
        _dbFilePath = dbFilePath;
    }

    private SqliteConnection GetPhysicalDbConnection()
    {
        var dbConnection = new SqliteConnection($"Data Source ={_dbFilePath};Mode=ReadWrite");
        dbConnection.Open();
        return dbConnection;
    }

    private void ExecuteNonQuery(string query)
    {
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);
        command.ExecuteNonQuery();
    }
    
    private static SqliteCommand GetCommand(string query, SqliteConnection connection)
    {
        return new SqliteCommand
        {
            CommandText = query,
            Connection = connection,
        };
    }
    
    public void Add(int Steps, int Steps_Till_Timeout, int Total_Resources, string Outcome)
    {
        var addResult =
            $@"
                INSERT INTO results (steps,steps_till_timeout,total_resources,outcome)
                VALUES ({Steps},{Steps_Till_Timeout},{Total_Resources},""{Outcome}"")
            ";
        ExecuteNonQuery(addResult);
    }

    public void Update(int id, int Steps, int Steps_Till_Timeout, int Total_Resources, string Outcome)
    {
        var updateResult =
            $@"
                UPDATE results
                SET
                    steps = {Steps},
                    stels_till_timeout = {Steps_Till_Timeout},
                    total_resources = {Total_Resources},
                    outcome = {Outcome}
                WHERE
                    id =""{id}""
            ";
        ExecuteNonQuery(updateResult);
    }

    public void Delete(int id)
    {
        var deleteResult =
            $@"
                DELETE FROM results
                WHERE id = ""{id}""
            ";
        ExecuteNonQuery(deleteResult);
    }

    public void DeleteAll()
    {
        var deleteAll =
            $@"
                DELETE FROM results
            ";
        ExecuteNonQuery(deleteAll);
    }

    public ContextResult Get(int id)
    {
        var query = @$"SELECT * FROM results WHERE id = {id}";
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);

        using var reader = command.ExecuteReader();
        return new ContextResult(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3),reader.GetString(4));
    }

    public IEnumerable<ContextResult> GetAll()
    {
        List<ContextResult> allResults = new List<ContextResult>();
        var query = 
            @"
                SELECT * 
                FROM results 
            ";
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            allResults.Add(new ContextResult(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3),reader.GetString(4)));
        }

        return allResults;
    }
}