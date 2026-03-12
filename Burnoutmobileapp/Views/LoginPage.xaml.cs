namespace Burnoutmobileapp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void OnLoginClicked(object sender, EventArgs e)
    {
        Application.Current!.Windows[0].Page = new AppShell();
    }

    private void OnRegisterTapped(object sender, EventArgs e)
    {
        Application.Current!.Windows[0].Page = new RegisterPage();
    }
}
