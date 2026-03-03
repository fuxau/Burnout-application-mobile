namespace Burnoutmobileapp.Models;

public class WorkoutSession
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ProgramName { get; set; } = string.Empty;
    public string WeekLabel { get; set; } = string.Empty;
    public string SessionLabel { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public Coach? Coach { get; set; }
    public List<WorkoutBlock> Blocks { get; set; } = new();
}

public class WorkoutBlock
{
    public string Name { get; set; } = string.Empty;
    public List<WorkoutExercise> Exercises { get; set; } = new();
}

public class WorkoutExercise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SeriesCount { get; set; }
    public int RepsPerSerie { get; set; }
    public int RecupSeconds { get; set; }
    public string Note { get; set; } = string.Empty;
    public List<WorkoutSet> Sets { get; set; } = new();

    public string DisplaySeries => RepsPerSerie > 1
        ? $"{SeriesCount} x {RepsPerSerie} reps"
        : $"{SeriesCount} serie(s)";

    public string DisplayRecup => RecupSeconds > 0
        ? $"Recup : {RecupSeconds}s"
        : string.Empty;
}

public class WorkoutSet
{
    public int SetNumber { get; set; }
    public int? Reps { get; set; }
    public double? Weight { get; set; }
    public double? Duration { get; set; }
}