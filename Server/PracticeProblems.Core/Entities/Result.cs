namespace PracticeProblems.Core.Entities;

public class Result
{
    public Result(string error = "None")
    {
        ErrorMessage = error;
    }
    public bool IsPassed { get; set; } = true;
    public string ErrorMessage { get; set; } = string.Empty;

    // to do add list of fail cases and the respective correct output for each fail case
    // public List<FailCases> FailCases { get; set; } = null!;
}