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
    public async Task<IActionResult> GetRequest([FromQuery] FibonacciRequestModel requestModel)
    {
        //Check if structure or values are invalid
        
        if (!ModelState.IsValid) return BadRequest();
        if (!Util.IsModelValid(requestModel)) return BadRequest();
        
        var result = await _fibonacciService.ParseFibonacciModel(requestModel);
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostRequest([FromBody] FibonacciRequestModel requestModel)
    {
        //Check if structure or values are invalid
        
        if (!ModelState.IsValid) return BadRequest();
        if (!Util.IsModelValid(requestModel)) return BadRequest();
        
        var result = await _fibonacciService.ParseFibonacciModel(requestModel);
        
        return Ok(result);
    }
}