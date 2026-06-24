namespace PracticeProblems.Data;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string Database { get; set; } = "devpracticeproblems";
}
