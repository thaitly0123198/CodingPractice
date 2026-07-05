namespace PracticeProblems.Core.Entities;

public class SubmittedSolution
{
    // todo: persist each user's submission to a collection in the database?
    string Id { get; set; }
    string Solution { get; set; }

    public SubmittedSolution(string id, string solution)
    {
        Id = id;
        Solution = solution;
    }   
}