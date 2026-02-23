namespace Burnoutmobileapp.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnAgendaTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//events");
    }

    private async void OnCoachTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//coach");
    }

    private async void OnProfilTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//profile");
    }

    private async void OnEventsTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//events");
    }
}
