using System.ComponentModel.DataAnnotations;

namespace Fibonacci;

public class FibonacciModel
{
    [Required]
    public String Start { get; set; }
    [Required]
    public String End { get; set; }
    public bool? Cache { get; set; }
    public int? MaxTime { get; set; }
    public int? MaxMemory { get; set; }
}