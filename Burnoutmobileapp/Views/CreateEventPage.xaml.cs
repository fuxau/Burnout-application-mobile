using Burnoutmobileapp.ViewModels;
using Burnoutmobileapp.Services;

namespace Burnoutmobileapp.Views;

public partial class CreateEventPage : ContentPage
{
    public CreateEventPage()
    {
        InitializeComponent();
        BindingContext = new CreateEventViewModel(new MockDataService());
    }
}
