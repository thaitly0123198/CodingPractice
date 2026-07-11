using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<List<ProblemsChunkResponse>>> GetProblems()
    {
        // getting the actual data from the db thru the service layer
        var problemsList = await problemsService.GetProblemsAsync();

    
        return Ok(problemsList.Select(p => new ProblemsChunkResponse(p.Id, p.Title, p.Difficulty)).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProblemByIdResponse>> GetSingleProblem(string id)
    {
        // to do: id == null
        var problem = await problemsService.GetProblemByIdAsync(id);
        return Ok(problem);
    }

    [HttpPost("submission/{id}")]
    public async Task<ActionResult<Result>> SubmitSolution(string id, [FromBody] Contracts.SubmittedSolution request)
    {
        if (id == "" || request == null || request.Solution == null || request.Solution == "")
        {
            return BadRequest("missing problem id");
        }
        
        Console.WriteLine($"ProblemsController: Received submission for problem id: {id}, solution: {request.Solution}");

        // todo: check if the solution is correct or not, and return the result to the user
        var result = await problemsService.GetResultsAsync(id, request.Solution);

        // todo: persist the user result to the database
        
        return Ok(result);
    }
}