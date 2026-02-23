namespace Burnoutmobileapp.Views;

public partial class EditProfilePage : ContentPage
{
    public EditProfilePage()
    {
        InitializeComponent();
    }

    private async void OnBackTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//profile");
    }

    private void OnClearEmailTapped(object sender, TappedEventArgs e)
    {
        EmailEntry.Text = string.Empty;
    }

    private void OnClearPhoneTapped(object sender, TappedEventArgs e)
    {
        PhoneEntry.Text = string.Empty;
    }

    private void OnClearAddressTapped(object sender, TappedEventArgs e)
    {
        AddressEntry.Text = string.Empty;
    }

    private void OnClearSportTapped(object sender, TappedEventArgs e)
    {
        SportEntry.Text = string.Empty;
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Succès", "Vos modifications ont été enregistrées.", "OK");
        await Shell.Current.GoToAsync("//profile");
    }
}
