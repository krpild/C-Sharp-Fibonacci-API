using System.Diagnostics;
using Fibonacci;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/")]
[ApiController]
public class FibonacciController : Controller
{
    private IFibonacciService _fibonacciService;

    public FibonacciController(IFibonacciService fibonacciService)
    {
        _fibonacciService = fibonacciService;
    }
        
    [HttpGet]
    public IActionResult Index([FromQuery] FibonacciModel model)
    {
        //Check if structure or values are invalid
        if (!ModelState.IsValid) return BadRequest();
        if (!Util.IsModelValid(model)) return BadRequest();
        var result = _fibonacciService.ParseFibonacciModel(model);
        
        return Ok(result);
    }
}