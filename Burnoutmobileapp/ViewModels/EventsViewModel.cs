using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Models;
using Burnoutmobileapp.Services;
using System.Collections.ObjectModel;

namespace Burnoutmobileapp.ViewModels;

public partial class EventsViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Event> _events = new();

    [ObservableProperty]
    private ObservableCollection<string> _categories = new() { "Tous", "Paris", "Annecy", "Yoga", "Cardio", "Force" };

    [ObservableProperty]
    private string _selectedCategory = "Tous";

    public EventsViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Événements";
    }

    [RelayCommand]
    private async Task LoadEventsAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var events = await _dataService.GetEventsByCategoryAsync(SelectedCategory);
            Events = new ObservableCollection<Event>(events);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SelectCategoryAsync(string category)
    {
        SelectedCategory = category;
        await LoadEventsAsync();
    }

    [RelayCommand]
    private async Task GoToEventDetailAsync(Event evt)
    {
        await Shell.Current.GoToAsync($"eventdetail?id={evt.Id}");
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
