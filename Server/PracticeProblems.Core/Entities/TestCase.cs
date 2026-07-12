namespace PracticeProblems.Core.Entities;

public class TestCase
{
    public string Input { get; set; } = string.Empty;
    public string ExpectedOutput { get; set; } = string.Empty;
    
    // hide for test cases that are not to be shown to the user, but used for testing
    public bool IsHidden { get; set; } = false;
}