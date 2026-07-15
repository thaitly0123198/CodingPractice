using PracticeProblems.Core.Entities;

namespace PracticeProblems.Core.Interfaces;

public interface IProblemsRepo
{
    Task<List<ProblemsChunk>> GetProblemsAsync(int page, int pageSize, bool diffDesc);

    Task<Problem> GetProblemByIdAsync(string id);

    Task<List<TestCase>> GetTestCasesByIdAsync(string id);
}