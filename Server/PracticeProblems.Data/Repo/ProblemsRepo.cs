using System.Data.Common;
using System.Reflection;
using MongoDB.Driver;
using PracticeProblems.Core.Entities;
using PracticeProblems.Core.Interfaces;

namespace PracticeProblems.Data.Repo;

public class ProblemsRepo(MongoContext mongoContext) : IProblemsRepo
{
    // get the Problem collection from mongo
    private readonly IMongoCollection<Problem> _problems = mongoContext.Problems;

    public async Task<List<ProblemsChunk>> GetProblemsAsync(int page, int pageSize, bool diffDesc = false)
    { 
        if (diffDesc)
        {
            return await _problems.Find(_ => true)
                .SortByDescending(p => p.DifficultyNum) // sort default by difficulty
                .ThenBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .Project(p => new ProblemsChunk { Id = p.Id, Title = p.Title, Difficulty = p.Difficulty })
                .ToListAsync();
        }
        return await _problems.Find(_ => true)
                .SortBy(p => p.DifficultyNum) // sort default by difficulty
                .ThenBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .Project(p => new ProblemsChunk { Id = p.Id, Title = p.Title, Difficulty = p.Difficulty })
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

        if (testCases == null)
        {
            throw new ArgumentNullException("Error fetching problem.");
        }
        
        // todo: when testcase is null, not found
        return testCases;
    }

}

