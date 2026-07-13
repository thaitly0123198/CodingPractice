using PracticeProblems.Core.Entities;
using PracticeProblems.Core.Interfaces;

namespace PracticeProblems.Services.MainServices;

public class ProblemsService(IProblemsRepo problems, IJudge judge) 
{
    public Task<List<Problem>> GetProblemsAsync()
    {
        return problems.GetProblemsAsync();
    }

    public Task<Problem> GetProblemByIdAsync(string id)
    {
        Console.WriteLine($"ProblemsService->GetProblemByIdAsync: Getting problem with id: {id}");
        return problems.GetProblemByIdAsync(id);
    }

    // todo: persist user's submission result to a collection in the database
    public Task<SubmittedSolution> PersistResultByIdTaskAsync(string id, string solution)
    {
        return problems.PersistResultByIdTaskAsync(id, solution);
    }

    public async Task<Result> GetResultsAsync(string problemId, string solution, string lang = "python")
    {
        // todo 2: check if the solution is correct or not, and return the result to the user
        //         - create worker service that will run the solution against the test cases and return the result
        if (string.IsNullOrWhiteSpace(solution))
        {
            throw new ArgumentException("Solution cannot be null or empty.", nameof(solution));
        }
        List<TestCase> testcases = await problems.GetTestCasesByIdAsync(problemId);

        // just return the submitted solution for now
        Console.WriteLine($"ProblemsService: Received submission for problem id: {problemId}, solution: {solution}");
        // return new SubmittedSolution(problemId, solution);

        // give it to the judge to get ispassed and fail cases
        return await judge.CheckSolutionAsync(problemId, solution, testcases, lang);
    }

}
