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

    // todo: persist user's submission result to a collection in the database
    public Task<SubmittedSolution> PersistResultByIdTaskAsync(string id, string solution)
    {
        return problems.PersistResultByIdTaskAsync(id, solution);
    }

    public async Task<SubmittedSolution> PostSolutionAsync(string id, string solution)
    {
        // todo 2: check if the solution is correct or not, and return the result to the user
        //         - create worker service that will run the solution against the test cases and return the result

        // just return the submitted solution for now
        Console.WriteLine($"ProblemsService: Received submission for problem id: {id}, solution: {solution}");
        return new SubmittedSolution(id, solution);
    }

}
