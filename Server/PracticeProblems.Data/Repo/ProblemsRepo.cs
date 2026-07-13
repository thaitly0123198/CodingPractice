using MongoDB.Driver;
using PracticeProblems.Core.Entities;
using PracticeProblems.Core.Interfaces;


namespace PracticeProblems.Data.Repo;

public class ProblemsRepo(MongoContext mongoContext) : IProblemsRepo
{
    // get the Problem collection from mongo
    private readonly IMongoCollection<Problem> _problems = mongoContext.Problems;

    public async Task<List<Problem>> GetProblemsAsync()
    { 
        return await _problems.Find(_ => true)
                .SortBy(p => p.Difficulty) // Sort lowest to highest
                .Limit(30)  
                .ToListAsync();
    }

    public async Task<Problem> GetProblemByIdAsync(string id)
    {
        return await _problems.Find(problem => problem.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<TestCase>> GetTestCasesByIdAsync(string id)
    {
        var filter = Builders<Problem>.Filter.Eq(problem => problem.Id, id);
        var options = new FindOptions<Problem, List<TestCase>>
        {
            Projection = Builders<Problem>.Projection.Expression(p => p.TestCases)
        };
        using var cursor = await _problems.FindAsync(filter, options);
        var testCases = await cursor.FirstOrDefaultAsync();

        // todo: when testcase is null, not found
        return testCases ?? new List<TestCase>();
    }

    public async Task<SubmittedSolution> PostSolutionAsync(string id, string solution)
    {
        // todo: check if the solution is correct or not, and return the result to the user
        // for now, just return the submitted solution
        return await Task.FromResult(new SubmittedSolution(id, solution));
    }

    public async Task<SubmittedSolution> PersistResultByIdTaskAsync(string id, string solution)
    {
        // todo: persist the submitted solution to the database
        // for now, just return the submitted solution
        return await Task.FromResult(new SubmittedSolution(id, solution));  
    }
}

