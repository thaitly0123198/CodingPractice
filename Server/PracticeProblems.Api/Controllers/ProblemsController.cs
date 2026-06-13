using Microsoft.AspNetCore.Mvc;

namespace PracticeProblems.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProblemsController : Controller
{
    // GET
    [HttpGet]
    public IActionResult GetProblems()
    {
        var problems = new[]
        {
            new { id = 1, title = "Two-Sum", difficulty = "Easy" },
            new { id = 2, title = "Reverse String", difficulty = "Medium" }
        };

        return Ok(problems);
    }
}