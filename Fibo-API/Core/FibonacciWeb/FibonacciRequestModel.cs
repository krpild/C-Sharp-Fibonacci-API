using System.ComponentModel.DataAnnotations;

namespace Core;
/// <summary>
/// Fibonacci Request model
/// </summary>
public class FibonacciRequestModel
{
    [Required]
    public int Start { get; set; }
    [Required]
    public int End { get; set; }
    public bool? Cache { get; set; }
    public int? MaxTime { get; set; }
    public int? MaxMemory { get; set; }
}