using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PracticeProblems.Core.Entities;

public class Problem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!; 

    [BsonElement("title")]
    public string Title { get; set; } = null!; 

    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonElement("examples")]
    public List<String> Examples { get; set; } = null!;
    [BsonElement("difficulty")]
    public string Difficulty { get; set; } = null!; // easy, medium, hard
    [BsonElement("category")]
    public string Category { get; set; } = null!;
    [BsonElement("constraint")]
    public string Constraint { get; set; } = null!;
    [BsonElement("testcases")]
    public List<TestCase> TestCases { get; set; } = new();
    [BsonElement("difficultyNum")]
    public int DifficultyNum { get; set; }

    [BsonElement("solution")] public SolutionStub Solution { get; set; } = null!;

    public class SolutionStub
    {
        [BsonElement("functionName")] public string FunctionName { get; set; } = string.Empty;
        [BsonElement("stubs")] public string Stubs { get; set; } = string.Empty;
    }

}