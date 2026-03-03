using Burnoutmobileapp.Models;
using Microsoft.Maui.Graphics;

namespace Burnoutmobileapp.Views;

[QueryProperty(nameof(Session), "Session")]
public partial class WorkoutTimerPage : ContentPage
{
    private WorkoutSession? _session;
    private record ExerciseStep(string BlockName, WorkoutExercise Exercise, int SetIndex);
    private List<ExerciseStep> _steps = new();
    private int _currentStepIndex = 0;

    private IDispatcherTimer? _exerciseTimer;
    private IDispatcherTimer? _totalTimer;
    private IDispatcherTimer? _recupTimer;

    private int _exerciseSecondsElapsed = 0;
    private int _totalSecondsElapsed = 0;
    private int _recupSecondsRemaining = 0;
    private bool _isInRecup = false;

    private ArcDrawable _arcDrawable = new();

    public WorkoutSession? Session
    {
        get => _session;
        set { _session = value; if (_session != null) BuildSteps(); }
    }

    public WorkoutTimerPage()
    {
        try { InitializeComponent(); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[WorkoutTimerPage] Init: {ex}"); throw; }
        TimerArc.Drawable = _arcDrawable;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartTotalTimer();
        ShowCurrentStep();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopAllTimers();
    }

    private void BuildSteps()
    {
        _steps.Clear();
        if (_session == null) return;
        foreach (var block in _session.Blocks)
            foreach (var exercise in block.Exercises)
                for (int s = 0; s < exercise.SeriesCount; s++)
                    _steps.Add(new ExerciseStep(block.Name, exercise, s));
    }

    private void ShowCurrentStep()
    {
        StopExerciseTimer();
        _exerciseSecondsElapsed = 0;
        _isInRecup = false;
        RecupBanner.IsVisible = false;

        if (_currentStepIndex >= _steps.Count)
        {
            FinishWorkout();
            return;
        }

        var step = _steps[_currentStepIndex];
        var exercise = step.Exercise;
        var setIndex = step.SetIndex;

        ProgressLabel.Text = $"{_currentStepIndex + 1} / {_steps.Count}";
        BlockNameLabel.Text = step.BlockName;
        ExerciseNameLabel.Text = exercise.Name;
        SerieLabel.Text = $"Serie {setIndex + 1} / {exercise.SeriesCount}";

        var set = setIndex < exercise.Sets.Count ? exercise.Sets[setIndex] : null;
        if (set?.Duration.HasValue == true)
        {
            Stat1Label.Text = "Duree";
            Stat1Value.Text = FormatDuration(set.Duration!.Value);
            Stat2Label.Text = "Charge";
            Stat2Value.Text = set.Weight.HasValue ? $"{set.Weight} kg" : "—";
        }
        else
        {
            Stat1Label.Text = "Reps";
            Stat1Value.Text = set?.Reps?.ToString() ?? exercise.RepsPerSerie.ToString();
            Stat2Label.Text = "Charge";
            Stat2Value.Text = set?.Weight.HasValue == true ? $"{set.Weight} kg" : "—";
        }

        NoteLabel.IsVisible = !string.IsNullOrEmpty(exercise.Note);
        if (!string.IsNullOrEmpty(exercise.Note))
            NoteLabel.Text = $"💡 {exercise.Note}";

        var duration = set?.Duration;
        if (duration.HasValue && duration.Value > 0)
        {
            var totalSecs = (int)duration.Value;
            _arcDrawable.Progress = 1f;
            TimerArc.Invalidate();
            TimerLabel.Text = FormatTime(totalSecs);
            MainActionButton.Text = "Serie suivante";
            StartExerciseCountdown(totalSecs);
        }
        else
        {
            _arcDrawable.Progress = 0f;
            TimerArc.Invalidate();
            TimerLabel.Text = "00:00";
            MainActionButton.Text = "Serie terminee";
            StartExerciseCountUp();
        }
    }

    private void StartExerciseCountdown(int totalSeconds)
    {
        int remaining = totalSeconds;
        _exerciseTimer = Application.Current!.Dispatcher.CreateTimer();
        _exerciseTimer.Interval = TimeSpan.FromSeconds(1);
        _exerciseTimer.Tick += (s, e) =>
        {
            remaining--;
            _exerciseSecondsElapsed++;
            _arcDrawable.Progress = Math.Max(0, remaining / (float)totalSeconds);
            TimerLabel.Text = FormatTime(remaining);
            TimerArc.Invalidate();
            if (remaining <= 0)
            {
                StopExerciseTimer();
                MainThread.BeginInvokeOnMainThread(AdvanceWithRecup);
            }
        };
        _exerciseTimer.Start();
    }

    private void StartExerciseCountUp()
    {
        int elapsed = 0;
        _exerciseTimer = Application.Current!.Dispatcher.CreateTimer();
        _exerciseTimer.Interval = TimeSpan.FromSeconds(1);
        _exerciseTimer.Tick += (s, e) =>
        {
            elapsed++;
            _exerciseSecondsElapsed = elapsed;
            _arcDrawable.Progress = Math.Min(elapsed / 60f, 1f);
            TimerLabel.Text = FormatTime(elapsed);
            TimerArc.Invalidate();
        };
        _exerciseTimer.Start();
    }

    private void StartTotalTimer()
    {
        _totalTimer = Application.Current!.Dispatcher.CreateTimer();
        _totalTimer.Interval = TimeSpan.FromSeconds(1);
        _totalTimer.Tick += (s, e) =>
        {
            _totalSecondsElapsed++;
            TotalElapsedLabel.Text = FormatTime(_totalSecondsElapsed);
        };
        _totalTimer.Start();
    }

    private void StartRecupTimer(int recupSeconds)
    {
        if (recupSeconds <= 0) { AdvanceToNextStep(); return; }
        _isInRecup = true;
        RecupBanner.IsVisible = true;
        _recupSecondsRemaining = recupSeconds;
        RecupTimerLabel.Text = FormatTime(recupSeconds);
        MainActionButton.Text = "Passer recup";

        _recupTimer = Application.Current!.Dispatcher.CreateTimer();
        _recupTimer.Interval = TimeSpan.FromSeconds(1);
        _recupTimer.Tick += (s, e) =>
        {
            _recupSecondsRemaining--;
            RecupTimerLabel.Text = FormatTime(_recupSecondsRemaining);
            if (_recupSecondsRemaining <= 0)
            {
                StopRecupTimer();
                MainThread.BeginInvokeOnMainThread(AdvanceToNextStep);
            }
        };
        _recupTimer.Start();
    }

    private void AdvanceWithRecup()
    {
        StopExerciseTimer();
        if (_currentStepIndex >= _steps.Count - 1) { AdvanceToNextStep(); return; }
        StartRecupTimer(_steps[_currentStepIndex].Exercise.RecupSeconds);
    }

    private void AdvanceToNextStep()
    {
        StopRecupTimer();
        _isInRecup = false;
        RecupBanner.IsVisible = false;
        _currentStepIndex++;
        ShowCurrentStep();
    }

    private async void FinishWorkout()
    {
        StopAllTimers();
        await Shell.Current.GoToAsync("workoutrpe", new Dictionary<string, object>
        {
            { "SessionTitle", _session?.Title ?? "Seance" },
            { "TotalSeconds", _totalSecondsElapsed }
        });
    }

    private void OnMainActionClicked(object sender, EventArgs e)
    {
        if (_isInRecup) AdvanceToNextStep();
        else { StopExerciseTimer(); AdvanceWithRecup(); }
    }

    private void OnSkipClicked(object sender, EventArgs e)
    {
        StopExerciseTimer();
        StopRecupTimer();
        _currentStepIndex++;
        ShowCurrentStep();
    }

    private async void OnCloseTapped(object sender, TappedEventArgs e)
    {
        bool confirm = await DisplayAlert("Quitter la seance",
            "Es-tu sur de vouloir quitter la seance en cours ?", "Quitter", "Continuer");
        if (confirm) { StopAllTimers(); await Shell.Current.GoToAsync("../.."); }
    }

    private void StopExerciseTimer() { _exerciseTimer?.Stop(); _exerciseTimer = null; }
    private void StopRecupTimer() { _recupTimer?.Stop(); _recupTimer = null; }
    private void StopAllTimers() { StopExerciseTimer(); StopRecupTimer(); _totalTimer?.Stop(); _totalTimer = null; }

    private static string FormatTime(int totalSeconds)
    {
        totalSeconds = Math.Max(0, totalSeconds);
        return $"{totalSeconds / 60:D2}:{totalSeconds % 60:D2}";
    }

    private static string FormatDuration(double seconds)
    {
        var ts = TimeSpan.FromSeconds(seconds);
        return ts.TotalMinutes >= 1 ? $"{(int)ts.TotalMinutes:D2}:{ts.Seconds:D2}" : $"{(int)seconds}s";
    }
}

public class ArcDrawable : IDrawable
{
    public float Progress { get; set; } = 0f;

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float cx = dirtyRect.Width / 2;
        float cy = dirtyRect.Height / 2;
        float radius = Math.Min(cx, cy) - 10;

        canvas.StrokeColor = Color.FromArgb("#005da1");
        canvas.StrokeSize = 14;
        canvas.StrokeLineCap = LineCap.Round;

        float startAngle = -90f;
        float sweepAngle = 360f * Progress;

        if (sweepAngle > 0)
            canvas.DrawArc(cx - radius, cy - radius, radius * 2, radius * 2,
                startAngle, startAngle + sweepAngle, false, false);
    }
}