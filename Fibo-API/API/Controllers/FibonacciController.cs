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
        
        //Check if structure or values are valid
        if (!ModelState.IsValid || !model.IsModelValid()) return BadRequest();
        
        var result = _fibonacciService.GetFibonacciRangeCached(Int32.Parse(model.Start), Int32.Parse(model.End)).Result;
        
        String[] items = new string[result.Length];
        
        for (int i = 0; i < result.Length; i++)
        {
            items[i] = result[i].ToString();
        }
        return Ok(items);
    }
}