namespace Burnoutmobileapp.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public string LocationDetail { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsFree => Price == 0;
    public string PriceDisplay => IsFree ? "Gratuit" : $"{Price}€";
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }
    public int RemainingSpots => MaxParticipants - CurrentParticipants;
    public string ParticipantsDisplay => $"+{CurrentParticipants} participants";
    public List<string> ParticipantAvatars { get; set; } = new();
    public Coach? Coach { get; set; }
    public string Intensity { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    
    public string DateDisplay => Date.ToString("dd MMM");
    public string TimeDisplay => $"{StartTime:hh\\:mm}";
    public string FullDateTimeDisplay => $"{Date:dddd dd MMM} • {StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
}
