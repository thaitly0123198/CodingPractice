using PracticeProblems.Core.Entities;

namespace PracticeProblems.Core.Interfaces;

public interface IProblemsRepo
{
    Task<List<Problem>> GetProblemsAsync();

    Task<Problem> GetProblemByIdAsync(string id);

    Task<SubmittedSolution> PersistResultByIdTaskAsync(string id, string solution);

    Task<SubmittedSolution> PostSolutionAsync(string id, string solution);
}