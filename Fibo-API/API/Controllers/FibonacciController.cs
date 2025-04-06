using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/")]
[ApiController]
public class FibonacciController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("hello");
    }
}