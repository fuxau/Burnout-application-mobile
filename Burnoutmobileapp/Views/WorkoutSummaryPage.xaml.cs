namespace Burnoutmobileapp.Views;

[QueryProperty(nameof(SessionTitle), "SessionTitle")]
[QueryProperty(nameof(TotalSeconds), "TotalSeconds")]
[QueryProperty(nameof(Rpe), "Rpe")]
[QueryProperty(nameof(MoodEmoji), "MoodEmoji")]
[QueryProperty(nameof(MoodLabel), "MoodLabel")]
public partial class WorkoutSummaryPage : ContentPage
{
    private string _sessionTitle = "";
    private int _totalSeconds = 0;
    private int _rpe = 5;
    private string _moodEmoji = "😐";
    private string _moodLabel = "Normal";

    public string SessionTitle { get => _sessionTitle; set { _sessionTitle = value; UpdateUI(); } }
    public int TotalSeconds { get => _totalSeconds; set { _totalSeconds = value; UpdateUI(); } }
    public int Rpe { get => _rpe; set { _rpe = value; UpdateUI(); } }
    public string MoodEmoji { get => _moodEmoji; set { _moodEmoji = value; UpdateUI(); } }
    public string MoodLabel { get => _moodLabel; set { _moodLabel = value; UpdateUI(); } }

    public WorkoutSummaryPage()
    {
        try { InitializeComponent(); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[WorkoutSummaryPage] Init: {ex}"); throw; }
    }

    private void UpdateUI()
    {
        if (SessionTitleLabel == null) return;
        SessionTitleLabel.Text = _sessionTitle;
        TimeStatLabel.Text = FormatTime(_totalSeconds);
        RpeStatLabel.Text = _rpe.ToString();
        MoodEmojiStat.Text = _moodEmoji;
        MoodLabelStat.Text = _moodLabel;

        if (PostEditor != null && string.IsNullOrEmpty(PostEditor.Text))
        {
            var timeStr = FormatTime(_totalSeconds);
            var draft = $"{_sessionTitle} terminee ! {_moodEmoji} Ressenti : {_moodLabel} | RPE {_rpe}/10 | Duree : {timeStr}";
            PostEditor.Text = draft.Length > 160 ? draft[..160] : draft;
            CharCountLabel.Text = $"{PostEditor.Text.Length} / 160";
        }
    }

    private async void OnPickPhotoTapped(object sender, TappedEventArgs e)
    {
        try
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Choisir une photo" });
            if (result == null) return;
            var stream = await result.OpenReadAsync();
            SelectedPhoto.Source = ImageSource.FromStream(() => stream);
            SelectedPhoto.IsVisible = true;
            PhotoPlaceholder.IsVisible = false;
            RemovePhotoButton.IsVisible = true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[WorkoutSummaryPage] Photo: {ex}");
        }
    }

    private void OnRemovePhotoClicked(object sender, EventArgs e)
    {
        SelectedPhoto.Source = null;
        SelectedPhoto.IsVisible = false;
        PhotoPlaceholder.IsVisible = true;
        RemovePhotoButton.IsVisible = false;
    }

    private void OnPostTextChanged(object sender, TextChangedEventArgs e)
    {
        var len = e.NewTextValue?.Length ?? 0;
        CharCountLabel.Text = $"{len} / 160";
        CharCountLabel.TextColor = len >= 150 ? Color.FromArgb("#EF4444") : Color.FromArgb("#6B7280");
    }

    private async void OnPublishClicked(object sender, EventArgs e)
    {
        var text = PostEditor.Text?.Trim() ?? "";
        if (string.IsNullOrEmpty(text))
        {
            await DisplayAlert("Post vide", "Ecris quelque chose avant de publier !", "OK");
            return;
        }
        await DisplayAlert("Publie !", "Ta seance a ete partagee avec succes !", "Super !");
        await Shell.Current.GoToAsync("//events");
    }

    private async void OnHomeClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("//events");

    private static string FormatTime(int totalSeconds)
    {
        totalSeconds = Math.Max(0, totalSeconds);
        int h = totalSeconds / 3600;
        int m = (totalSeconds % 3600) / 60;
        int s = totalSeconds % 60;
        return h > 0 ? $"{h}h{m:D2}min" : $"{m:D2}:{s:D2}";
    }
}