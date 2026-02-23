namespace Burnoutmobileapp.Views;

public partial class CoachPage : ContentPage
{
    public CoachPage()
    {
        InitializeComponent();
    }

    private async void OnAccueilTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//home");
    }

    private async void OnAgendaTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//events");
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
