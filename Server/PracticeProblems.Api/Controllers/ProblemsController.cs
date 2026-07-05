using Microsoft.AspNetCore.Mvc;
using PracticeProblems.Api.Contracts;
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

    [HttpPost("{id}/submit")]
    public async Task<ActionResult<SubmittedSolution>> SubmitSolution(string id, [FromBody] SubmittedSolution request)
    {
        if (id == null || id == "" || request == null || request.Solution == null || request.Solution == "")
        {
            return BadRequest("missing problem id");
        }

        var result = await problemsService.PostSolutionAsync(id, request.Solution);
        return Ok(result);
    }
}