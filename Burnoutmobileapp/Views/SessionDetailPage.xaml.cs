using Burnoutmobileapp.Models;

namespace Burnoutmobileapp.Views;

[QueryProperty(nameof(Session), "Session")]
public partial class SessionDetailPage : ContentPage
{
    private WorkoutSession? _session;

    public WorkoutSession? Session
    {
        get => _session;
        set
        {
            _session = value;
            if (_session != null)
                BuildUI();
        }
    }

    public SessionDetailPage()
    {
        try { InitializeComponent(); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[SessionDetailPage] Init error: {ex}"); throw; }
    }

    private void BuildUI()
    {
        var session = _session!;
        TitleLabel.Text = session.Title;
        ProgramLabel.Text = session.ProgramName;
        SessionTitleLabel.Text = session.Title;
        TimeLabel.Text = session.Time.ToString(@"hh\:mm");
        CoachLabel.Text = session.Coach?.Name ?? "—";

        BadgesRow.Children.Clear();
        foreach (var badge in new[] { session.WeekLabel, session.SessionLabel, session.Type })
        {
            if (string.IsNullOrEmpty(badge)) continue;
            bool isType = badge == session.Type;
            var b = new Border
            {
                StrokeThickness = 0,
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 6 },
                BackgroundColor = isType ? Color.FromArgb("#1A005da1") : Color.FromArgb("#112447"),
                Padding = new Thickness(10, 4)
            };
            b.Content = new Label
            {
                Text = badge,
                FontSize = 11,
                FontAttributes = FontAttributes.Bold,
                TextColor = isType ? Color.FromArgb("#005da1") : Color.FromArgb("#9CA3AF")
            };
            BadgesRow.Children.Add(b);
        }

        BlocksContainer.Children.Clear();
        foreach (var block in session.Blocks)
        {
            var blockBorder = new Border
            {
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Color.FromArgb("#1a3a6b")),
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 14 },
                BackgroundColor = Color.FromArgb("#0d1f3c"),
                Padding = new Thickness(0, 0, 0, 4)
            };

            var blockStack = new VerticalStackLayout();

            var blockHeader = new Border
            {
                StrokeThickness = 0,
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = new CornerRadius(14, 14, 0, 0) },
                BackgroundColor = Color.FromArgb("#112447"),
                Padding = new Thickness(16, 12)
            };
            blockHeader.Content = new Label
            {
                Text = block.Name,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#005da1")
            };
            blockStack.Children.Add(blockHeader);

            foreach (var exercise in block.Exercises)
            {
                var exGrid = new Grid
                {
                    Padding = new Thickness(16, 12),
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                var numBorder = new Border
                {
                    StrokeThickness = 1,
                    Stroke = new SolidColorBrush(Color.FromArgb("#1a3a6b")),
                    StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 16 },
                    BackgroundColor = Color.FromArgb("#112447"),
                    HeightRequest = 32, WidthRequest = 32,
                    VerticalOptions = LayoutOptions.Start,
                    Margin = new Thickness(0, 2, 0, 0)
                };
                numBorder.Content = new Label
                {
                    Text = exercise.Id.ToString(),
                    FontSize = 12, FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#005da1"),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(numBorder, 0);
                exGrid.Children.Add(numBorder);

                var infoStack = new VerticalStackLayout { Margin = new Thickness(12, 0, 0, 0), Spacing = 3 };
                infoStack.Children.Add(new Label { Text = exercise.Name, FontSize = 15, FontAttributes = FontAttributes.Bold, TextColor = Colors.White });

                var seriesRow = new HorizontalStackLayout { Spacing = 12 };
                seriesRow.Children.Add(new Label { Text = exercise.DisplaySeries, FontSize = 13, TextColor = Color.FromArgb("#9CA3AF") });
                if (!string.IsNullOrEmpty(exercise.DisplayRecup))
                    seriesRow.Children.Add(new Label { Text = exercise.DisplayRecup, FontSize = 13, TextColor = Color.FromArgb("#6B7280") });
                infoStack.Children.Add(seriesRow);

                if (!string.IsNullOrEmpty(exercise.Note))
                    infoStack.Children.Add(new Label { Text = exercise.Note, FontSize = 11, TextColor = Color.FromArgb("#4a7dbf") });

                Grid.SetColumn(infoStack, 1);
                exGrid.Children.Add(infoStack);

                var seriesBadge = new Border
                {
                    StrokeThickness = 0,
                    StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 8 },
                    BackgroundColor = Color.FromArgb("#112447"),
                    Padding = new Thickness(10, 6),
                    VerticalOptions = LayoutOptions.Start,
                    Margin = new Thickness(0, 2, 0, 0)
                };
                seriesBadge.Content = new Label { Text = $"{exercise.SeriesCount} series", FontSize = 11, TextColor = Color.FromArgb("#9CA3AF") };
                Grid.SetColumn(seriesBadge, 2);
                exGrid.Children.Add(seriesBadge);

                blockStack.Children.Add(exGrid);

                if (exercise != block.Exercises.Last())
                    blockStack.Children.Add(new BoxView { HeightRequest = 1, Color = Color.FromArgb("#1a3a6b"), Margin = new Thickness(16, 0) });
            }

            blockBorder.Content = blockStack;
            BlocksContainer.Children.Add(blockBorder);
        }
    }

    private async void OnLancerClicked(object sender, EventArgs e)
    {
        if (_session == null) return;
        await Shell.Current.GoToAsync("workouttimer", new Dictionary<string, object> { { "Session", _session } });
    }

    private async void OnBackTapped(object sender, TappedEventArgs e) => await Shell.Current.GoToAsync("..");
}