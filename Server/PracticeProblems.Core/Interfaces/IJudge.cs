using PracticeProblems.Core.Entities;

namespace PracticeProblems.Core.Interfaces;

public interface IJudge
{
    Task<Result> CheckSolutionAsync(string problemId, string solution, string language = "python");
}