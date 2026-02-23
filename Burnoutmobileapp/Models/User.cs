namespace Burnoutmobileapp.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string MembershipLevel { get; set; } = string.Empty;
    public int MemberSince { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string BloodType { get; set; } = string.Empty;
    public string FavoriteSport { get; set; } = string.Empty;
    public double Height { get; set; }
    public double Weight { get; set; }
    public double BMI => Weight / ((Height / 100) * (Height / 100));
    public string BMIDisplay => BMI.ToString("F1");
    public int WeeklyVisits { get; set; }
    public int CaloriesBurned { get; set; }
}
