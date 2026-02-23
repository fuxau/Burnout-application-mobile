using Burnoutmobileapp.ViewModels;
using Burnoutmobileapp.Services;

namespace Burnoutmobileapp.Views;

public partial class ChallengePage : ContentPage
{
    public ChallengePage()
    {
        InitializeComponent();
        BindingContext = new ChallengeViewModel(new MockDataService());
    }
}
