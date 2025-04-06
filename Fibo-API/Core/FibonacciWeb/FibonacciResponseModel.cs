using System.ComponentModel.DataAnnotations;

namespace Core;

public class FibonacciResponseModel
{
    public List<String> Sequence { get; set; }
    public List<String>? Errors { get; set; }
}