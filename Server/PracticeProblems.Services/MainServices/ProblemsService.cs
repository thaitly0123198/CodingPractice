using PracticeProblems.Core.Entities;
using PracticeProblems.Core.Interfaces;

namespace PracticeProblems.Services.MainServices;

public class ProblemsService(IProblemsRepo problems) 
{
    public Task<List<Problem>> GetProblemsAsync()
    {
        return problems.GetProblemsAsync();
    }

    public Task<Problem> GetProblemByIdAsync(string id)
    {
        return problems.GetProblemByIdAsync(id);
    }

}
