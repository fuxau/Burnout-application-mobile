namespace Burnoutmobileapp.Views;

public partial class FeedPage : ContentPage
{
    private record FeedPost(string UserName, string Avatar, string SessionName, string TimeAgo,
        string Duration, string Mood, int Rpe, string Caption);

    public FeedPage()
    {
        try { InitializeComponent(); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[FeedPage] Init: {ex}"); throw; }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BuildFeed();
    }

    private void BuildFeed()
    {
        FeedContainer.Children.Clear();

        var posts = new List<FeedPost>
        {
            new("Erik Dupont",   "ED", "Seance Musculation", "Il y a 2h",  "48:30", "😁", 7,  "Belle seance aujourd hui, les squats etaient au top ! Programme Force semaine 3."),
            new("Marie Lefort",  "ML", "Seance Cardio",      "Il y a 5h",  "35:12", "😐", 5,  "Run matinal sous la pluie, mais j ai tenu le coup ! RPE correct."),
            new("Toi",           "TO", "Seance Musculation", "Aujourd hui","52:10", "🤩", 8,  "Ma seance vient d etre postee. Resultat excellent, RPE 8/10 !"),
            new("Lucas Bernard", "LB", "Seance Full Body",   "Hier",       "41:00", "😔", 9,  "Seance intense, j ai tout donne. Recup demain obligatoire."),
            new("Sofia Morin",   "SM", "Yoga & Mobilite",    "Il y a 2j",  "30:00", "😩", 3,  "Seance douce pour recuperer. Exactement ce qu il fallait."),
        };

        foreach (var post in posts)
            FeedContainer.Children.Add(BuildPostCard(post));
    }

    private View BuildPostCard(FeedPost post)
    {
        var card = new Border
        {
            StrokeThickness = 1,
            Stroke = new SolidColorBrush(Color.FromArgb("#1a3a6b")),
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 18 },
            BackgroundColor = Color.FromArgb("#0d1f3c"),
            Padding = new Thickness(0)
        };

        var outer = new VerticalStackLayout { Spacing = 0 };

        // Header
        var header = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            },
            Padding = new Thickness(16, 14, 16, 10)
        };

        var avatarBorder = new Border
        {
            StrokeThickness = 0,
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 20 },
            BackgroundColor = Color.FromArgb("#005da1"),
            HeightRequest = 40, WidthRequest = 40,
            VerticalOptions = LayoutOptions.Center
        };
        avatarBorder.Content = new Label
        {
            Text = post.Avatar, FontSize = 12, FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center
        };
        Grid.SetColumn(avatarBorder, 0);
        header.Children.Add(avatarBorder);

        var nameStack = new VerticalStackLayout { Spacing = 1, Margin = new Thickness(10, 0, 0, 0), VerticalOptions = LayoutOptions.Center };
        nameStack.Children.Add(new Label { Text = post.UserName, FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Colors.White });
        nameStack.Children.Add(new Label { Text = post.SessionName, FontSize = 12, TextColor = Color.FromArgb("#005da1") });
        Grid.SetColumn(nameStack, 1);
        header.Children.Add(nameStack);

        var timeLabel = new Label { Text = post.TimeAgo, FontSize = 11, TextColor = Color.FromArgb("#6B7280"), VerticalOptions = LayoutOptions.Start, Margin = new Thickness(0, 4, 0, 0) };
        Grid.SetColumn(timeLabel, 2);
        header.Children.Add(timeLabel);

        outer.Children.Add(header);
        outer.Children.Add(new BoxView { HeightRequest = 1, Color = Color.FromArgb("#1a3a6b") });

        // Stats
        var statsGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            Padding = new Thickness(16, 12, 16, 12),
            ColumnSpacing = 8
        };

        var dur = new VerticalStackLayout { Spacing = 2, HorizontalOptions = LayoutOptions.Center };
        dur.Children.Add(new Label { Text = $"⏱ {post.Duration}", FontSize = 13, FontAttributes = FontAttributes.Bold, TextColor = Colors.White, HorizontalOptions = LayoutOptions.Center });
        dur.Children.Add(new Label { Text = "Duree", FontSize = 10, TextColor = Color.FromArgb("#6B7280"), HorizontalOptions = LayoutOptions.Center });
        Grid.SetColumn(dur, 0);
        statsGrid.Children.Add(dur);

        var mood = new VerticalStackLayout { Spacing = 2, HorizontalOptions = LayoutOptions.Center };
        mood.Children.Add(new Label { Text = post.Mood, FontSize = 22, HorizontalOptions = LayoutOptions.Center });
        mood.Children.Add(new Label { Text = "Ressenti", FontSize = 10, TextColor = Color.FromArgb("#6B7280"), HorizontalOptions = LayoutOptions.Center });
        Grid.SetColumn(mood, 1);
        statsGrid.Children.Add(mood);

        var rpe = new VerticalStackLayout { Spacing = 2, HorizontalOptions = LayoutOptions.Center };
        rpe.Children.Add(new Label { Text = $"💪 {post.Rpe}/10", FontSize = 13, FontAttributes = FontAttributes.Bold, TextColor = Colors.White, HorizontalOptions = LayoutOptions.Center });
        rpe.Children.Add(new Label { Text = "RPE", FontSize = 10, TextColor = Color.FromArgb("#6B7280"), HorizontalOptions = LayoutOptions.Center });
        Grid.SetColumn(rpe, 2);
        statsGrid.Children.Add(rpe);

        outer.Children.Add(statsGrid);

        // Caption
        if (!string.IsNullOrEmpty(post.Caption))
        {
            outer.Children.Add(new BoxView { HeightRequest = 1, Color = Color.FromArgb("#1a3a6b") });
            outer.Children.Add(new Label
            {
                Text = post.Caption,
                FontSize = 13, TextColor = Color.FromArgb("#D1D5DB"),
                Padding = new Thickness(16, 12, 16, 14),
                LineBreakMode = LineBreakMode.WordWrap
            });
        }

        // Actions
        outer.Children.Add(new BoxView { HeightRequest = 1, Color = Color.FromArgb("#1a3a6b") });
        var actionsRow = new HorizontalStackLayout { Spacing = 20, Padding = new Thickness(16, 10, 16, 12) };
        actionsRow.Children.Add(new Label { Text = "👍  J aime", FontSize = 13, TextColor = Color.FromArgb("#6B7280") });
        actionsRow.Children.Add(new Label { Text = "💬  Commenter", FontSize = 13, TextColor = Color.FromArgb("#6B7280") });
        outer.Children.Add(actionsRow);

        card.Content = outer;
        return card;
    }

    private async void OnBackTapped(object sender, TappedEventArgs e) => await Shell.Current.GoToAsync("..");
    private async void OnAccueilTapped(object sender, TappedEventArgs e) => await Shell.Current.GoToAsync("//home");
    private async void OnAgendaTapped(object sender, TappedEventArgs e) => await Shell.Current.GoToAsync("//events");
    private async void OnCoachTapped(object sender, TappedEventArgs e) => await Shell.Current.GoToAsync("//coach");
    private async void OnProfilTapped(object sender, TappedEventArgs e) => await Shell.Current.GoToAsync("//profile");
}