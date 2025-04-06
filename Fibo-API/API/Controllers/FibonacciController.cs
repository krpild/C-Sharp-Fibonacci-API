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
        Console.WriteLine(model.Start);
        Console.WriteLine(model.End);

        if (!ModelState.IsValid) return BadRequest();
        Stopwatch sw = new Stopwatch();
        sw.Start();
        var result = _fibonacciService.GetFibonacciRangeCached(Int32.Parse(model.Start), Int32.Parse(model.End)).Result;
        Console.WriteLine(sw.ElapsedMilliseconds);
        sw.Stop();
        String[] items = new string[result.Length];
        

        for (int i = 0; i < result.Length; i++)
        {
            items[i] = result[i].ToString();
        }
        return Ok(items);
    }
}