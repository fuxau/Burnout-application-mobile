namespace Burnoutmobileapp.Views;

public partial class EventsPage : ContentPage
{
    public EventsPage()
    {
        InitializeComponent();
    }

    private async void OnAccueilTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//home");
    }

    private async void OnCoachTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//coach");
    }

    private async void OnProfilTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//profile");
    }

    private async void OnBackTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//home");
    }
}
