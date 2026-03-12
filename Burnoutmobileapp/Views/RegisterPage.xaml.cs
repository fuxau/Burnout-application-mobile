using Burnoutmobileapp.Services;
using Burnoutmobileapp.ViewModels;

namespace Burnoutmobileapp.Views;

public partial class RegisterPage : ContentPage
{
    private readonly RegisterViewModel _vm;

    public RegisterPage()
    {
        InitializeComponent();
        _vm = new RegisterViewModel(new MockDataService());
        _vm.NavigateToLogin = () =>
            Application.Current!.Windows[0].Page = new LoginPage();
        BindingContext = _vm;
    }

    private void OnBackClicked(object sender, EventArgs e)
    {
        if (_vm.CanGoBack)
            _vm.PreviousStepCommand.Execute(null);
        else
            Application.Current!.Windows[0].Page = new LoginPage();
    }

    private async void OnNextClicked(object sender, EventArgs e)
    {
        if (_vm.CurrentStep == _vm.TotalSteps)
            await _vm.CreateAccountCommand.ExecuteAsync(null);
        else
            _vm.NextStepCommand.Execute(null);
    }
}
