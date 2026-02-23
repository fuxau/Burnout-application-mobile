namespace Burnoutmobileapp.Models;

public class Session
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Coach? Coach { get; set; }
    public string DateTimeDisplay => Date.Date == DateTime.Today 
        ? $"Aujourd'hui, {Time:hh\\:mm}" 
        : $"{Date:dd MMM}, {Time:hh\\:mm}";
}
