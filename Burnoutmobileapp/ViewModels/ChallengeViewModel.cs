using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Models;
using Burnoutmobileapp.Services;

namespace Burnoutmobileapp.ViewModels;

[QueryProperty(nameof(ChallengeId), "id")]
public partial class ChallengeViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty]
    private int _challengeId;

    [ObservableProperty]
    private Challenge? _challenge;

    public ChallengeViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Défi";
    }

    partial void OnChallengeIdChanged(int value)
    {
        LoadChallengeCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadChallengeAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            Challenge = await _dataService.GetChallengeByIdAsync(ChallengeId);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task StartTodayTaskAsync()
    {
        if (Challenge == null) return;
        await Shell.Current.DisplayAlert("Défi du jour", $"Commencer: {Challenge.TodayTask}", "C'est parti!");
    }

    [RelayCommand]
    private async Task JoinChallengeAsync()
    {
        if (Challenge == null) return;

        var success = await _dataService.JoinChallengeAsync(Challenge.Id);
        if (success)
        {
            await Shell.Current.DisplayAlert("Succès", "Vous avez rejoint ce défi!", "OK");
            await LoadChallengeAsync();
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
