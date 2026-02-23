using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Models;
using Burnoutmobileapp.Services;
using System.Collections.ObjectModel;

namespace Burnoutmobileapp.ViewModels;

public partial class CoachViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Coach> _coaches = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    public CoachViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Contacter un Coach";
    }

    [RelayCommand]
    private async Task LoadCoachesAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var coaches = await _dataService.GetCoachesAsync();
            
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                coaches = coaches.Where(c => 
                    c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    c.Specialty.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            Coaches = new ObservableCollection<Coach>(coaches);
        }
        finally
        {
            IsBusy = false;
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        LoadCoachesCommand.Execute(null);
    }

    [RelayCommand]
    private async Task MessageCoachAsync(Coach coach)
    {
        await Shell.Current.DisplayAlert("Message", $"Ouverture de la conversation avec {coach.Name}", "OK");
    }

    [RelayCommand]
    private async Task CallCoachAsync(Coach coach)
    {
        if (!coach.IsAvailable)
        {
            await Shell.Current.DisplayAlert("Indisponible", $"{coach.Name} n'est pas disponible actuellement", "OK");
            return;
        }
        await Shell.Current.DisplayAlert("Appel", $"Appel de {coach.Name}...", "OK");
    }
}
