namespace PracticeProblems.Core.Entities;

public class Result
{
    public bool IsPassed { get; set; } = false;
    public TestCase FailedCase { get; set; } = null!;

    public string RuntimeErrorMessage { get; set; } = string.Empty;
    public string CompilationErrorMessage { get; set; } = string.Empty;

    public string TimeoutError { get; set; } = string.Empty;

    // to do add list of fail cases and the respective correct output for each fail case
    // public List<FailCases> FailCases { get; set; } = null!;
}