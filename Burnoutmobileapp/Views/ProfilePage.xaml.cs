namespace Burnoutmobileapp.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
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

    private async void OnCoachTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//coach");
    }

    private async void OnBackTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//home");
    }

    private async void OnSettingsTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//editprofile");
    }

    private async void OnImcTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//imc");
    }

    private async void OnAgeTapped(object sender, TappedEventArgs e)
    {
        string result = await DisplayPromptAsync("Modifier l'âge", "Entrez votre âge:", initialValue: AgeLabel.Text, keyboard: Keyboard.Numeric);
        if (!string.IsNullOrEmpty(result))
        {
            AgeLabel.Text = result;
        }
    }

    private async void OnGenreTapped(object sender, TappedEventArgs e)
    {
        string action = await DisplayActionSheet("Sélectionnez votre genre", "Annuler", null, "Homme", "Femme", "Autre");
        if (action != "Annuler" && !string.IsNullOrEmpty(action))
        {
            GenreLabel.Text = action;
        }
    }

    private async void OnBloodTypeTapped(object sender, TappedEventArgs e)
    {
        string action = await DisplayActionSheet("Sélectionnez votre groupe sanguin", "Annuler", null, "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-");
        if (action != "Annuler" && !string.IsNullOrEmpty(action))
        {
            BloodTypeLabel.Text = action;
        }
    }
}
