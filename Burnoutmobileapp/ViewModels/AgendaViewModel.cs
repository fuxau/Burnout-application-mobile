using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Models;
using Burnoutmobileapp.Services;
using System.Collections.ObjectModel;

namespace Burnoutmobileapp.ViewModels;

public partial class DayItem : ObservableObject
{
    public string DisplayText { get; set; } = string.Empty;
    public string DisplayColor { get; set; } = "#9CA3AF";
    public double DisplayFontSize { get; set; } = 12;
    public string DisplayFontAttributes { get; set; } = "None";
    public string StrokeBrush { get; set; } = "#1a3a6b";
    public bool IsCompleted { get; set; }
    public int DayNumber { get; set; }
}

public partial class AgendaViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<DayItem> _dayItems = new();

    [ObservableProperty]
    private double _progressPercent = 0;

    [ObservableProperty]
    private double _progressBarWidth = 0;

    [ObservableProperty]
    private int _remainingDays = 0;

    [ObservableProperty]
    private string _todayTask = string.Empty;

    [ObservableProperty]
    private string _challengeTitle = string.Empty;

    [ObservableProperty]
    private WorkoutSession? _todaySession;

    public AgendaViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Agenda";
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            var challenges = await _dataService.GetChallengesAsync();
            var challenge = challenges.FirstOrDefault();
            if (challenge != null)
            {
                ChallengeTitle = challenge.Title;
                ProgressPercent = challenge.ProgressPercentage;
                RemainingDays = challenge.RemainingDays;
                ProgressBarWidth = 260 * (challenge.ProgressPercentage / 100.0);

                DayItems.Clear();
                int day = 1;
                foreach (var completed in challenge.DayCompletionStatus.Take(30))
                {
                    bool isToday = day == challenge.CompletedDays + 1;
                    DayItems.Add(new DayItem
                    {
                        IsCompleted = completed,
                        DayNumber = day,
                        DisplayText = completed ? "✓" : day.ToString(),
                        DisplayColor = completed ? "#005da1" : isToday ? "#FFFFFF" : "#6B7280",
                        DisplayFontSize = completed ? 14 : 12,
                        DisplayFontAttributes = isToday ? "Bold" : "None",
                        StrokeBrush = isToday ? "#005da1" : completed ? "#005da1" : "#1a3a6b"
                    });
                    day++;
                }
            }

            var session = await _dataService.GetTodayWorkoutSessionAsync();
            TodaySession = session;
            TodayTask = session?.Title ?? "Aucune seance";
        }
        finally
        {
            IsBusy = false;
        }
    }
}