using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Models;
using Burnoutmobileapp.Services;

namespace Burnoutmobileapp.ViewModels;

[QueryProperty(nameof(EventId), "id")]
public partial class EventDetailViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty]
    private int _eventId;

    [ObservableProperty]
    private Event? _event;

    public EventDetailViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Détails";
    }

    partial void OnEventIdChanged(int value)
    {
        LoadEventCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadEventAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            Event = await _dataService.GetEventByIdAsync(EventId);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        if (Event == null) return;

        var success = await _dataService.RegisterForEventAsync(Event.Id);
        if (success)
        {
            await Shell.Current.DisplayAlert("Succès", "Vous êtes inscrit à cet événement!", "OK");
            await LoadEventAsync();
        }
        else
        {
            await Shell.Current.DisplayAlert("Erreur", "Impossible de s'inscrire à cet événement", "OK");
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task ShareAsync()
    {
        if (Event == null) return;
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Title = Event.Title,
            Text = $"Rejoins-moi à {Event.Title} le {Event.DateDisplay}!"
        });
    }

    [RelayCommand]
    private async Task OpenMapAsync()
    {
        if (Event == null) return;
        await Shell.Current.DisplayAlert("GPS", $"Ouverture de la navigation vers {Event.LocationDetail}", "OK");
    }
}
