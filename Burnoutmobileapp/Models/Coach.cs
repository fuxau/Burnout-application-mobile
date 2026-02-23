namespace Burnoutmobileapp.Models;

public class Coach
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Availability { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public bool IsPro { get; set; }
}
