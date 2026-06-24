using Microsoft.AspNetCore.Mvc;
using PracticeProblems.Api.Contracts;
using PracticeProblems.Services.MainServices;

namespace PracticeProblems.Api.Controllers;

[ApiController]
[Route("api/problems")]
public class ProblemsController(ProblemsService problemsService) : ControllerBase
{
    // GET
    [HttpGet]
    public async Task<ActionResult<List<ProblemsChunkResponse>>> GetProblems()
    {

        // mock data for now, will be replaced with actual data from the database later
        var problems = new ProblemsChunkResponse[]
        {
            new(1, "Calculate Sum", "Easy"),
            new(2, "Check Palindrome", "Medium"),
            new(3, "Check repeats in unordered list", "Hard")
        };

        // getting the actual data from the db thru the service layer
        var problemsList = await problemsService.GetProblemsAsync();

        return Ok(problems);
    }
}