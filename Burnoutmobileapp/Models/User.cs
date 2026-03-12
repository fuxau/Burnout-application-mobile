namespace Burnoutmobileapp.Models;

public class SportActivity
{
    public string Name { get; set; } = string.Empty;
    public string WeeklyFrequency { get; set; } = string.Empty;
    public string DailyFrequency { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
}

public class ProfessionalActivity
{
    public string Name { get; set; } = string.Empty;
    public string PhysicalFatigue { get; set; } = string.Empty;
    public string MentalFatigue { get; set; } = string.Empty;
}

public class WeeklySchedule
{
    public List<string> Monday { get; set; } = new();
    public List<string> Tuesday { get; set; } = new();
    public List<string> Wednesday { get; set; } = new();
    public List<string> Thursday { get; set; } = new();
    public List<string> Friday { get; set; } = new();
    public List<string> Saturday { get; set; } = new();
    public List<string> Sunday { get; set; } = new();
}

public class User
{
    public int Id { get; set; }
    public string Password { get; set; } = string.Empty;

    // Informations Générales
    public string Presentation { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Gender { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    // Coordonnées
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    // Adresse Postale
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    // Situation Personnelle
    public string MaritalStatus { get; set; } = string.Empty;
    public string PartnerName { get; set; } = string.Empty;
    public string PersonalPhysicalFatigue { get; set; } = string.Empty;
    public string PersonalMentalFatigue { get; set; } = string.Empty;
    public string PersonalFatigueComment { get; set; } = string.Empty;

    // Situation Professionnelle
    public List<ProfessionalActivity> ProfessionalActivities { get; set; } = new();
    public string ProfessionalFatigueComment { get; set; } = string.Empty;

    // Actualités & Antécédents
    public string TobaccoConsumption { get; set; } = string.Empty;
    public string AlcoholConsumption { get; set; } = string.Empty;
    public string DrugConsumption { get; set; } = string.Empty;
    public string MovementLimitations { get; set; } = string.Empty;
    public string PlannedOperation { get; set; } = string.Empty;
    public string MedicalCondition { get; set; } = string.Empty;
    public string SpecificMedicalTreatment { get; set; } = string.Empty;
    public int MaxHeartRate { get; set; }
    public int RestHeartRate { get; set; }

    // Disponibilités Hebdomadaires
    public int WeeklyTrainingCount { get; set; }
    public int SessionsPerTraining { get; set; }
    public string TimePerSession { get; set; } = string.Empty;
    public string TotalWeeklyTime { get; set; } = string.Empty;
    public WeeklySchedule Schedule { get; set; } = new();

    // Qualité de Vie
    public string SleepQuality { get; set; } = string.Empty;
    public string SleepQuantity { get; set; } = string.Empty;
    public string DietQuality { get; set; } = string.Empty;
    public string DietQuantity { get; set; } = string.Empty;

    // Activités Physiques et Sportives
    public List<SportActivity> SportActivities { get; set; } = new();

    // Legacy / computed fields kept for compatibility
    public string MembershipLevel { get; set; } = string.Empty;
    public int MemberSince { get; set; }
    public int Age => BirthDate.HasValue ? (int)((DateTime.Today - BirthDate.Value).TotalDays / 365.25) : 0;
    public string BloodType { get; set; } = string.Empty;
    public string FavoriteSport { get; set; } = string.Empty;
    public double Height { get; set; }
    public double Weight { get; set; }
    public double BMI => Height > 0 ? Weight / ((Height / 100) * (Height / 100)) : 0;
    public string BMIDisplay => BMI.ToString("F1");
    public int WeeklyVisits { get; set; }
    public int CaloriesBurned { get; set; }
}
