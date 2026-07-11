namespace PracticeProblems.Core.Entities;

public class TestCase(string input, string expectedOutput)
{
    public string Input { get; set; } = input;
    public string ExpectedOutput { get; set; } = expectedOutput;
}