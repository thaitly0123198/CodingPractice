using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using PracticeProblems.Api.Contracts;
using PracticeProblems.Core.Entities;
using PracticeProblems.Services.MainServices;

namespace PracticeProblems.Api.Controllers;

[ApiController]
[Route("nav/problems")]
public class ProblemsController(ProblemsService problemsService) : ControllerBase
{
    // GET
    [HttpGet]
    public async Task<ActionResult<List<ProblemsChunkResponse>>> GetProblems([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] bool diffDesc)
    {
        // getting the actual data from the db thru the service layer
        var problemsList = await problemsService.GetProblemsAsync(page, pageSize, diffDesc);

    
        return Ok(problemsList.Select(p => new ProblemsChunkResponse(p.Id, p.Title, p.Difficulty)).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProblemByIdResponse>> GetSingleProblem(string id)
    {
        // to do: id == null
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("missing problem id");
        }
        var problem = await problemsService.GetProblemByIdAsync(id);
        var s = problem.Solution ?? throw new ArgumentNullException("Error getting placeholder.");
        string solutionBoxPlaceholder = s.Stubs;
        string fname = s.FunctionName;
        var responseChunk = new ProblemByIdResponse (
            problem.Id,
            problem.Title,
            problem.Description,
            problem.Constraint,
            problem.Examples,
            problem.Category,
            solutionBoxPlaceholder,
            fname
        //,problem.TestCases
        );
        return Ok(responseChunk);
    }

    [HttpPost("submission/{id}")]
    public async Task<ActionResult<ResultsResponse>> SubmitSolution(string id, [FromBody] Contracts.SubmittedSolution request)
    {
        string solutionFuncName = request.FunctionName;
        if (string.IsNullOrEmpty(id) || request == null || string.IsNullOrEmpty(request.Solution))
        {
            return BadRequest("missing problem id");
        }

        Console.WriteLine($"ProblemsController: Received submission for problem id: {id}, solution: {request.Solution}");

        // todo: check if the solution is correct or not, and return the result to the user
        // todo: add time limit to prevent infinite loop, long runtime
        var result = await problemsService.GetResultsAsync(id, request.Solution, solutionFuncName); 
        FailCase? fc = result.FailedCase is null ? null : new FailCase(result.FailedCase.Input.ToJson());
        var resultResponse = new ResultsResponse(result.IsPassed, fc, result.RuntimeErrorMessage, result.CompilationErrorMessage);

        // todo: persist the user result to the database
        // make sure "result" is a "Result" obj
        return Ok(resultResponse);
    }
}