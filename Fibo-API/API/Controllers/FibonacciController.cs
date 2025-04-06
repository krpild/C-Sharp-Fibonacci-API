using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FibonacciController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("hello");
    }
}