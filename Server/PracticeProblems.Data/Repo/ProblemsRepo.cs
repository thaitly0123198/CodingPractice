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
        // connect to mongo db later, for now just return mock data
        return await _problems.Find(_ => true)
                .SortBy(p => p.Title) // Sort lowest to highest
                .Limit(10)  
                .ToListAsync();
    }
     
    public async Task<Problem> GetProblemByIdAsync(string id)
    {
        return await _problems.Find(problem => problem.Id == id).FirstOrDefaultAsync();
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

