using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Services;

namespace Burnoutmobileapp.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private bool _isPasswordVisible;

    public LoginViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Call of Phoenix";
    }

    [RelayCommand]
    private void TogglePasswordVisibility()
    {
        IsPasswordVisible = !IsPasswordVisible;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var success = await _dataService.LoginAsync(Email, Password);
            if (success)
            {
                await Shell.Current.GoToAsync("//main/home");
            }
            else
            {
                await Shell.Current.DisplayAlert("Erreur", "Email ou mot de passe incorrect", "OK");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ForgotPasswordAsync()
    {
        await Shell.Current.DisplayAlert("Mot de passe oublié", "Un email de réinitialisation vous sera envoyé.", "OK");
    }

    [RelayCommand]
    private async Task BiometricLoginAsync()
    {
        await Shell.Current.GoToAsync("//main/home");
    }
}
