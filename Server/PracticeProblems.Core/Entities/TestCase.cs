using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PracticeProblems.Core.Entities;

public class TestCase
{
    [BsonElement("input")]
    public object? Input { get; set; } = null!;
    [BsonElement("output")]
    public object? Output { get; set; } = null!;

    // hide for test cases that are not to be shown to the user, but used for testing
    [BsonElement("ishidden")]
    public bool IsHidden { get; set; } = true;
}