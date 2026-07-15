using PracticeProblems.Core.Entities;
using PracticeProblems.Core.Interfaces;

namespace PracticeProblems.Services.MainServices;

public class ProblemsService(IProblemsRepo problems, IJudge judge) 
{
    public Task<List<ProblemsChunk>> GetProblemsAsync(int page, int pageSize, bool diffDesc)
    {
        return problems.GetProblemsAsync(page, pageSize, diffDesc);
    }

    public Task<Problem> GetProblemByIdAsync(string id)
    {
        Console.WriteLine($"ProblemsService->GetProblemByIdAsync: Getting problem with id: {id}");
        return problems.GetProblemByIdAsync(id);
    }

    public async Task<Result> GetResultsAsync(string problemId, string solution, string solutionFuncName, string lang = "python")
    {
        // - create worker service that will run the solution against the test cases and return the result
        if (string.IsNullOrWhiteSpace(solution))
        {
            throw new ArgumentException("Solution cannot be null or empty.", nameof(solution));
        }
        List<TestCase> testcases = await problems.GetTestCasesByIdAsync(problemId);

        Console.WriteLine($"ProblemsService: Received submission for problem id: {problemId}, solution: {solution}");

        // todo: address security vulnerabilities: infinite loops, solution code accessing env
        //         todo: execute each submission inside a throwaway container

        // give it to the judge to get ispassed and fail cases
        return await judge.CheckSolutionAsync(problemId, solution, solutionFuncName, testcases, lang);
    }

}
