using Burnoutmobileapp.Models;
using Burnoutmobileapp.Services;
using Burnoutmobileapp.ViewModels;

namespace Burnoutmobileapp.Views;

public partial class EventsPage : ContentPage
{
    private readonly AgendaViewModel _viewModel;
    private readonly IMockDataService _dataService;
    private DateTime _weekStart;
    private DateTime _selectedDate;

    public EventsPage()
    {
        try { InitializeComponent(); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[EventsPage] Init error: {ex}"); throw; }

        _dataService = new MockDataService();
        _viewModel = new AgendaViewModel(_dataService);
        BindingContext = _viewModel;

        var today = DateTime.Today;
        _weekStart = today.AddDays(-(int)today.DayOfWeek == 0 ? 6 : (int)today.DayOfWeek - 1);
        _selectedDate = today;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            await _viewModel.LoadCommand.ExecuteAsync(null);
            BuildWeekDays();
            await LoadSessionsForDate(_selectedDate);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[EventsPage] OnAppearing error: {ex}");
        }
    }

    private void BuildWeekDays()
    {
        DaysContainer.Children.Clear();
        var dayNames = new[] { "LUN.", "MAR.", "MER.", "JEU.", "VEN.", "SAM.", "DIM." };
        for (int i = 0; i < 7; i++)
        {
            var day = _weekStart.AddDays(i);
            bool isSelected = day.Date == _selectedDate.Date;
            bool isToday = day.Date == DateTime.Today;

            var dayView = BuildDayButton(day, dayNames[i], isSelected, isToday);
            DaysContainer.Children.Add(dayView);
        }
    }

    private View BuildDayButton(DateTime day, string dayName, bool isSelected, bool isToday)
    {
        var bg = isSelected ? Color.FromArgb("#005da1") : Color.FromArgb("#0d1f3c");
        var textColor = isSelected ? Colors.White : (isToday ? Color.FromArgb("#005da1") : Color.FromArgb("#9CA3AF"));
        var numColor = isSelected ? Colors.White : Colors.White;
        var stroke = isSelected ? Color.FromArgb("#005da1") : Color.FromArgb("#1a3a6b");

        var stack = new VerticalStackLayout
        {
            Spacing = 2,
            Padding = new Thickness(10, 8),
            HorizontalOptions = LayoutOptions.Center
        };

        var border = new Border
        {
            StrokeThickness = isSelected ? 0 : 1,
            Stroke = new SolidColorBrush(stroke),
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 16 },
            BackgroundColor = bg,
            MinimumWidthRequest = 52,
            Padding = new Thickness(6, 10)
        };

        stack.Children.Add(new Label
        {
            Text = dayName,
            FontSize = 11,
            TextColor = textColor,
            HorizontalOptions = LayoutOptions.Center
        });
        stack.Children.Add(new Label
        {
            Text = day.Day.ToString(),
            FontSize = isSelected ? 22 : 20,
            FontAttributes = isSelected ? FontAttributes.Bold : FontAttributes.None,
            TextColor = numColor,
            HorizontalOptions = LayoutOptions.Center
        });

        border.Content = stack;

        var captured = day;
        border.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(async () =>
            {
                _selectedDate = captured;
                BuildWeekDays();
                await LoadSessionsForDate(captured);
            })
        });

        return border;
    }

    private async Task LoadSessionsForDate(DateTime date)
    {
        SessionsContainer.Children.Clear();
        var sessions = await _dataService.GetSessionsForDateAsync(date);

        if (sessions == null || sessions.Count == 0)
        {
            NoSessionLabel.IsVisible = true;
            return;
        }

        NoSessionLabel.IsVisible = false;
        foreach (var session in sessions)
        {
            var card = BuildSessionCard(session);
            SessionsContainer.Children.Add(card);
        }
    }

    private View BuildSessionCard(WorkoutSession session)
    {
        var timeStr = session.Time.ToString(@"hh\:mm");

        var typeBg = session.Type == "MUSCU"
            ? Color.FromArgb("#1A005da1")
            : Color.FromArgb("#1A22C55E");
        var typeTextColor = session.Type == "MUSCU"
            ? Color.FromArgb("#005da1")
            : Color.FromArgb("#22C55E");

        var card = new Border
        {
            StrokeThickness = 1,
            Stroke = new SolidColorBrush(Color.FromArgb("#1a3a6b")),
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 16 },
            BackgroundColor = Color.FromArgb("#0d1f3c"),
            Padding = new Thickness(20, 16)
        };

        var mainStack = new VerticalStackLayout { Spacing = 10 };

        // Title
        mainStack.Children.Add(new Label
        {
            Text = session.Title,
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });

        // Time + type row
        var infoRow = new HorizontalStackLayout { Spacing = 16 };
        infoRow.Children.Add(new Label
        {
            Text = $"⏰  {timeStr}",
            FontSize = 14,
            TextColor = Color.FromArgb("#9CA3AF")
        });

        var typeBadge = new Border
        {
            StrokeThickness = 0,
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 6 },
            BackgroundColor = typeBg,
            Padding = new Thickness(10, 4)
        };
        typeBadge.Content = new Label
        {
            Text = session.Type,
            FontSize = 11,
            FontAttributes = FontAttributes.Bold,
            TextColor = typeTextColor
        };
        infoRow.Children.Add(typeBadge);
        mainStack.Children.Add(infoRow);

        // "Voir la séance" hint
        mainStack.Children.Add(new Label
        {
            Text = "Appuyez pour voir la seance  ›",
            FontSize = 12,
            TextColor = Color.FromArgb("#005da1")
        });

        card.Content = mainStack;

        var captured = session;
        card.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(async () =>
            {
                await Shell.Current.GoToAsync("sessiondetail", new Dictionary<string, object>
                {
                    { "Session", captured }
                });
            })
        });

        return card;
    }

    private async void OnPrevWeekTapped(object sender, TappedEventArgs e)
    {
        _weekStart = _weekStart.AddDays(-7);
        _selectedDate = _weekStart;
        BuildWeekDays();
        await LoadSessionsForDate(_selectedDate);
    }

    private async void OnNextWeekTapped(object sender, TappedEventArgs e)
    {
        _weekStart = _weekStart.AddDays(7);
        _selectedDate = _weekStart;
        BuildWeekDays();
        await LoadSessionsForDate(_selectedDate);
    }

    private async void OnAccueilTapped(object sender, TappedEventArgs e) =>
        await Shell.Current.GoToAsync("//home");

    private async void OnFeedTapped(object sender, TappedEventArgs e) =>
        await Shell.Current.GoToAsync("feed");

    private async void OnCoachTapped(object sender, TappedEventArgs e) =>
        await Shell.Current.GoToAsync("//coach");

    private async void OnProfilTapped(object sender, TappedEventArgs e) =>
        await Shell.Current.GoToAsync("//profile");
}