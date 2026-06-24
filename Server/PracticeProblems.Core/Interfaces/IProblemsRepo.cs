using PracticeProblems.Core.Entities;

namespace PracticeProblems.Core.Interfaces;

public interface IProblemsRepo
{
    Task<List<Problem>> GetProblemsAsync();

    Task<Problem> GetProblemByIdAsync(string id);
}