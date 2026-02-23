using Burnoutmobileapp.Views;

namespace Burnoutmobileapp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute("eventdetail", typeof(EventDetailPage));
        Routing.RegisterRoute("createevent", typeof(CreateEventPage));
        Routing.RegisterRoute("challenge", typeof(ChallengePage));
    }
}