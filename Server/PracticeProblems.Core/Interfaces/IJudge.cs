using PracticeProblems.Core.Entities;

namespace PracticeProblems.Core.Interfaces;

public interface IJudge
{
    Task<Result> CheckSolutionAsync(string problemId, string solution, List<TestCase> tc, string language = "python");
}