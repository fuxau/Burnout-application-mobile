namespace Burnoutmobileapp.Models;

public class Challenge
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int TotalDays { get; set; }
    public int CompletedDays { get; set; }
    public int RemainingDays => TotalDays - CompletedDays;
    public double ProgressPercentage => (double)CompletedDays / TotalDays * 100;
    public string ProgressDisplay => $"Jours {CompletedDays}/{TotalDays}";
    public bool IsJoined { get; set; }
    public string TodayTask { get; set; } = string.Empty;
    public string IconName { get; set; } = string.Empty;
    public List<bool> DayCompletionStatus { get; set; } = new();
}
