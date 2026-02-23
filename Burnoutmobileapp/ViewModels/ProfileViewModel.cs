using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Models;
using Burnoutmobileapp.Services;

namespace Burnoutmobileapp.ViewModels;

public partial class ProfileViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty]
    private User? _user;

    [ObservableProperty]
    private string _selectedGender = "Homme";

    [ObservableProperty]
    private string _selectedBloodType = "O+";

    public List<string> Genders { get; } = new() { "Homme", "Femme", "Autre" };
    public List<string> BloodTypes { get; } = new() { "O+", "A+", "B+", "AB+", "O-", "A-", "B-", "AB-" };

    public ProfileViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Profil";
    }

    [RelayCommand]
    private async Task LoadProfileAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            User = await _dataService.GetCurrentUserAsync();
            if (User != null)
            {
                SelectedGender = User.Gender;
                SelectedBloodType = User.BloodType;
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SaveProfileAsync()
    {
        await Shell.Current.DisplayAlert("Succès", "Profil enregistré avec succès!", "OK");
    }

    [RelayCommand]
    private async Task EditProfileAsync()
    {
        await Shell.Current.GoToAsync("editprofile");
    }

    [RelayCommand]
    private async Task ChangePhotoAsync()
    {
        await Shell.Current.DisplayAlert("Photo", "Changement de photo de profil", "OK");
    }

    [RelayCommand]
    private async Task OpenBMICalculatorAsync()
    {
        await Shell.Current.GoToAsync("bmicalculator");
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        var confirm = await Shell.Current.DisplayAlert("Déconnexion", "Voulez-vous vraiment vous déconnecter?", "Oui", "Non");
        if (confirm)
        {
            await Shell.Current.GoToAsync("login");
        }
    }

    [RelayCommand]
    private async Task GoToSettingsAsync()
    {
        await Shell.Current.DisplayAlert("Paramètres", "Page des paramètres", "OK");
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
