using Burnoutmobileapp.ViewModels;
using Burnoutmobileapp.Services;

namespace Burnoutmobileapp.Views;

public partial class EventDetailPage : ContentPage
{
    public EventDetailPage()
    {
        InitializeComponent();
        BindingContext = new EventDetailViewModel(new MockDataService());
    }
}
