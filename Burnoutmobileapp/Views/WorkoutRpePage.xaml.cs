namespace Burnoutmobileapp.Views;

[QueryProperty(nameof(SessionTitle), "SessionTitle")]
[QueryProperty(nameof(TotalSeconds), "TotalSeconds")]
public partial class WorkoutRpePage : ContentPage
{
    private string _sessionTitle = "";
    private int _totalSeconds = 0;
    private int _selectedMood = -1;
    private int _selectedRpe = 5;

    private static readonly string[] MoodEmojis = { "😩", "😔", "😐", "😁", "🤩" };
    private static readonly string[] MoodLabels = { "Epuise", "Difficile", "Normal", "Bien", "Excellent" };
    private static readonly string[] RpeDescs =
    {
        "", "Tres facile", "Facile", "Modere", "Un peu dur",
        "Effort modere", "Difficile", "Tres difficile", "Tres dur", "Extremement dur", "Effort maximal"
    };

    public string SessionTitle { get => _sessionTitle; set { _sessionTitle = value; UpdateHeader(); } }
    public int TotalSeconds { get => _totalSeconds; set { _totalSeconds = value; UpdateHeader(); } }

    public WorkoutRpePage()
    {
        try { InitializeComponent(); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[WorkoutRpePage] Init: {ex}"); throw; }
        UpdateRpeSelection();
    }

    private void UpdateHeader()
    {
        if (SessionTitleLabel != null) SessionTitleLabel.Text = _sessionTitle;
        if (TotalTimeLabel != null) TotalTimeLabel.Text = FormatTime(_totalSeconds);
    }

    private void OnMoodTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is not string param || !int.TryParse(param, out int idx)) return;
        _selectedMood = idx;
        UpdateMoodSelection();
    }

    private void UpdateMoodSelection()
    {
        var borders = new[] { MoodBorder0, MoodBorder1, MoodBorder2, MoodBorder3, MoodBorder4 };
        for (int i = 0; i < borders.Length; i++)
        {
            bool active = i == _selectedMood;
            borders[i].Stroke = new SolidColorBrush(active ? Color.FromArgb("#005da1") : Color.FromArgb("#1a3a6b"));
            borders[i].BackgroundColor = active ? Color.FromArgb("#1A005da1") : Color.FromArgb("#112447");
        }
    }

    private void OnRpeTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is not string param || !int.TryParse(param, out int rpe)) return;
        _selectedRpe = rpe;
        UpdateRpeSelection();
    }

    private void UpdateRpeSelection()
    {
        var borders = new[] { Rpe1, Rpe2, Rpe3, Rpe4, Rpe5, Rpe6, Rpe7, Rpe8, Rpe9, Rpe10 };
        for (int i = 0; i < borders.Length; i++)
        {
            int rpeNum = i + 1;
            bool active = rpeNum == _selectedRpe;

            Color activeColor = rpeNum <= 3 ? Color.FromArgb("#22C55E")
                : rpeNum <= 6 ? Color.FromArgb("#F59E0B")
                : rpeNum <= 8 ? Color.FromArgb("#F97316")
                : Color.FromArgb("#EF4444");

            Color activeBg = rpeNum <= 3 ? Color.FromArgb("#3322C55E")
                : rpeNum <= 6 ? Color.FromArgb("#33F59E0B")
                : rpeNum <= 8 ? Color.FromArgb("#33F97316")
                : Color.FromArgb("#33EF4444");

            borders[i].Stroke = new SolidColorBrush(active ? activeColor : Color.FromArgb("#1a3a6b"));
            borders[i].BackgroundColor = active ? activeBg : Color.FromArgb("#112447");

            if (borders[i].Content is Label lbl)
                lbl.TextColor = active ? activeColor : Color.FromArgb("#9CA3AF");
        }

        if (RpeValueLabel != null) RpeValueLabel.Text = _selectedRpe.ToString();
        if (RpeDescLabel != null && _selectedRpe >= 1 && _selectedRpe <= 10)
            RpeDescLabel.Text = RpeDescs[_selectedRpe];
    }

    private async void OnValidateClicked(object sender, EventArgs e)
    {
        string moodEmoji = _selectedMood >= 0 ? MoodEmojis[_selectedMood] : "😐";
        string moodLabel = _selectedMood >= 0 ? MoodLabels[_selectedMood] : "Normal";

        await Shell.Current.GoToAsync("workoutsummary", new Dictionary<string, object>
        {
            { "SessionTitle", _sessionTitle },
            { "TotalSeconds", _totalSeconds },
            { "Rpe", _selectedRpe },
            { "MoodEmoji", moodEmoji },
            { "MoodLabel", moodLabel }
        });
    }

    private static string FormatTime(int totalSeconds)
    {
        totalSeconds = Math.Max(0, totalSeconds);
        int h = totalSeconds / 3600;
        int m = (totalSeconds % 3600) / 60;
        int s = totalSeconds % 60;
        return h > 0 ? $"{h}h{m:D2}min" : $"{m:D2}:{s:D2}";
    }
}