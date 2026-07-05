namespace PracticeProblems.Core.Entities;

public class SubmittedSolution(string id, string solution)
{
    // todo: persist each user's submission to a collection in the database?
    public string Id { get; set; } = id;
    public string Solution { get; set; } = solution;
}