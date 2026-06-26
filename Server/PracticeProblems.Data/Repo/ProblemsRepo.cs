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
}

