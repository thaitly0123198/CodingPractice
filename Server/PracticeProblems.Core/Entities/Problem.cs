namespace PracticeProblems.Core.Entities;

public class Problem
{
    // Title, description, time limit, memory limit
    public string Id { get; set; } = null!; 
    public string Title { get; set; } = null!; 
    public string LongDescription { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Difficulty { get; set; } = null!; // easy, medium, hard
}