using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Services;

namespace Burnoutmobileapp.ViewModels;

public partial class CreateEventViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty]
    private string _eventName = string.Empty;

    [ObservableProperty]
    private DateTime _eventDate = DateTime.Today;

    [ObservableProperty]
    private TimeSpan _eventTime = new TimeSpan(10, 0, 0);

    [ObservableProperty]
    private string _location = string.Empty;

    [ObservableProperty]
    private string _selectedTheme = "Yoga";

    [ObservableProperty]
    private string _price = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private string? _imageUrl;

    public List<string> Themes { get; } = new() { "Yoga", "Cardio", "Force", "Crossfit", "Autre" };

    public CreateEventViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Créer un Événement";
    }

    [RelayCommand]
    private async Task PublishEventAsync()
    {
        if (string.IsNullOrWhiteSpace(EventName))
        {
            await Shell.Current.DisplayAlert("Erreur", "Veuillez entrer un nom d'événement", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Location))
        {
            await Shell.Current.DisplayAlert("Erreur", "Veuillez entrer un lieu", "OK");
            return;
        }

        await Shell.Current.DisplayAlert("Succès", "Événement publié avec succès!", "OK");
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task SelectImageAsync()
    {
        await Shell.Current.DisplayAlert("Image", "Sélection d'image de couverture", "OK");
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
