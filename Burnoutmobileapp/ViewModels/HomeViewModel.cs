using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Models;
using Burnoutmobileapp.Services;
using System.Collections.ObjectModel;

namespace Burnoutmobileapp.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty]
    private User? _currentUser;

    [ObservableProperty]
    private Session? _nextSession;

    [ObservableProperty]
    private ObservableCollection<Challenge> _challenges = new();

    [ObservableProperty]
    private ObservableCollection<Event> _upcomingEvents = new();

    public HomeViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Accueil";
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            CurrentUser = await _dataService.GetCurrentUserAsync();
            NextSession = await _dataService.GetNextSessionAsync();
            
            var challenges = await _dataService.GetChallengesAsync();
            Challenges = new ObservableCollection<Challenge>(challenges);

            var events = await _dataService.GetEventsAsync();
            UpcomingEvents = new ObservableCollection<Event>(events.Take(3));
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task OpenGymAccessAsync()
    {
        await Shell.Current.DisplayAlert("Accès Salle", "Présentez ce QR code à l'entrée", "OK");
    }

    [RelayCommand]
    private async Task GoToEventsAsync()
    {
        await Shell.Current.GoToAsync("//events");
    }

    [RelayCommand]
    private async Task GoToSessionAsync()
    {
        await Shell.Current.GoToAsync("//agenda");
    }

    [RelayCommand]
    private async Task GoToChallengeAsync(Challenge challenge)
    {
        await Shell.Current.GoToAsync($"challenge?id={challenge.Id}");
    }

    [RelayCommand]
    private async Task GoToNotificationsAsync()
    {
        await Shell.Current.DisplayAlert("Notifications", "Aucune nouvelle notification", "OK");
    }
}
