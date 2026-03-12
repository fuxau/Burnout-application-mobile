using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Burnoutmobileapp.Models;
using Burnoutmobileapp.Services;
using System.Collections.ObjectModel;

namespace Burnoutmobileapp.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly IMockDataService _dataService;

    [ObservableProperty] private int _currentStep = 1;
    public int TotalSteps => 5;
    public bool IsStep1 => CurrentStep == 1;
    public bool IsStep2 => CurrentStep == 2;
    public bool IsStep3 => CurrentStep == 3;
    public bool IsStep4 => CurrentStep == 4;
    public bool IsStep5 => CurrentStep == 5;
    public bool CanGoBack => CurrentStep > 1;
    public Color Step2Color => CurrentStep >= 2 ? Color.FromArgb("#005da1") : Color.FromArgb("#1e3a5f");
    public Color Step3Color => CurrentStep >= 3 ? Color.FromArgb("#005da1") : Color.FromArgb("#1e3a5f");
    public Color Step4Color => CurrentStep >= 4 ? Color.FromArgb("#005da1") : Color.FromArgb("#1e3a5f");
    public Color Step5Color => CurrentStep >= 5 ? Color.FromArgb("#005da1") : Color.FromArgb("#1e3a5f");

    public string StepLabel => $"Etape {CurrentStep} / {TotalSteps}";
    public string NextButtonText => CurrentStep == TotalSteps ? "Creer mon compte" : "Suivant";
    public string BackButtonText => CurrentStep == 1 ? "Annuler" : "Retour";

    // Step 1 - Informations Generales
    [ObservableProperty] private string _presentation = string.Empty;
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string _gender = "Homme";
    [ObservableProperty] private DateTime _birthDate = new DateTime(1990, 1, 1);
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _phone = string.Empty;
    [ObservableProperty] private string _address = string.Empty;
    [ObservableProperty] private string _postalCode = string.Empty;
    [ObservableProperty] private string _city = string.Empty;
    [ObservableProperty] private string _country = "France";
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;

    public List<string> GenderOptions { get; } = new() { "Homme", "Femme", "Autre" };
    public List<string> CountryOptions { get; } = new() { "France", "Belgique", "Suisse", "Canada", "Autre" };

    // Step 2 - Situation Personnelle & Professionnelle
    [ObservableProperty] private string _maritalStatus = "Celibataire";
    [ObservableProperty] private string _partnerName = string.Empty;
    [ObservableProperty] private string _personalPhysicalFatigue = "Peu Fatigante";
    [ObservableProperty] private string _personalMentalFatigue = "Peu Fatigante";
    [ObservableProperty] private string _personalFatigueComment = string.Empty;
    [ObservableProperty] private string _newProActivityName = string.Empty;
    [ObservableProperty] private string _newProPhysicalFatigue = "Peu Fatigante";
    [ObservableProperty] private string _newProMentalFatigue = "Peu Fatigante";
    [ObservableProperty] private string _professionalFatigueComment = string.Empty;
    public ObservableCollection<ProfessionalActivity> ProfessionalActivities { get; } = new();

    public List<string> MaritalStatusOptions { get; } = new() { "Celibataire", "En couple", "Marie(e)", "Divorce(e)", "Veuf/Veuve" };
    public List<string> FatigueOptions { get; } = new() { "Peu Fatigante", "Fatigante", "Tres Fatigante" };

    // Step 3 - Antecedents Medicaux
    [ObservableProperty] private string _tobaccoConsumption = "Aucune";
    [ObservableProperty] private string _alcoholConsumption = "Aucune";
    [ObservableProperty] private string _drugConsumption = "Aucune";
    [ObservableProperty] private string _movementLimitations = string.Empty;
    [ObservableProperty] private string _plannedOperation = string.Empty;
    [ObservableProperty] private string _medicalCondition = string.Empty;
    [ObservableProperty] private string _specificMedicalTreatment = string.Empty;
    [ObservableProperty] private string _maxHeartRateText = string.Empty;
    [ObservableProperty] private string _restHeartRateText = string.Empty;
    public List<string> ConsumptionOptions { get; } = new() { "Aucune", "Occasionnelle", "Reguliere", "Quotidienne" };

    // Step 4 - Disponibilites & Qualite de Vie
    [ObservableProperty] private string _weeklyTrainingCountText = string.Empty;
    [ObservableProperty] private string _sessionsPerTrainingText = string.Empty;
    [ObservableProperty] private string _timePerSession = string.Empty;

    // Weekly schedule slots (14 time slots per day: 6h to 20h)
    [ObservableProperty] private bool _mon0; [ObservableProperty] private bool _mon1;
    [ObservableProperty] private bool _mon2; [ObservableProperty] private bool _mon3;
    [ObservableProperty] private bool _mon4; [ObservableProperty] private bool _mon5;
    [ObservableProperty] private bool _mon6; [ObservableProperty] private bool _mon7;
    [ObservableProperty] private bool _mon8; [ObservableProperty] private bool _mon9;
    [ObservableProperty] private bool _mon10; [ObservableProperty] private bool _mon11;
    [ObservableProperty] private bool _mon12; [ObservableProperty] private bool _mon13;

    [ObservableProperty] private bool _tue0; [ObservableProperty] private bool _tue1;
    [ObservableProperty] private bool _tue2; [ObservableProperty] private bool _tue3;
    [ObservableProperty] private bool _tue4; [ObservableProperty] private bool _tue5;
    [ObservableProperty] private bool _tue6; [ObservableProperty] private bool _tue7;
    [ObservableProperty] private bool _tue8; [ObservableProperty] private bool _tue9;
    [ObservableProperty] private bool _tue10; [ObservableProperty] private bool _tue11;
    [ObservableProperty] private bool _tue12; [ObservableProperty] private bool _tue13;

    [ObservableProperty] private bool _wed0; [ObservableProperty] private bool _wed1;
    [ObservableProperty] private bool _wed2; [ObservableProperty] private bool _wed3;
    [ObservableProperty] private bool _wed4; [ObservableProperty] private bool _wed5;
    [ObservableProperty] private bool _wed6; [ObservableProperty] private bool _wed7;
    [ObservableProperty] private bool _wed8; [ObservableProperty] private bool _wed9;
    [ObservableProperty] private bool _wed10; [ObservableProperty] private bool _wed11;
    [ObservableProperty] private bool _wed12; [ObservableProperty] private bool _wed13;

    [ObservableProperty] private bool _thu0; [ObservableProperty] private bool _thu1;
    [ObservableProperty] private bool _thu2; [ObservableProperty] private bool _thu3;
    [ObservableProperty] private bool _thu4; [ObservableProperty] private bool _thu5;
    [ObservableProperty] private bool _thu6; [ObservableProperty] private bool _thu7;
    [ObservableProperty] private bool _thu8; [ObservableProperty] private bool _thu9;
    [ObservableProperty] private bool _thu10; [ObservableProperty] private bool _thu11;
    [ObservableProperty] private bool _thu12; [ObservableProperty] private bool _thu13;

    [ObservableProperty] private bool _fri0; [ObservableProperty] private bool _fri1;
    [ObservableProperty] private bool _fri2; [ObservableProperty] private bool _fri3;
    [ObservableProperty] private bool _fri4; [ObservableProperty] private bool _fri5;
    [ObservableProperty] private bool _fri6; [ObservableProperty] private bool _fri7;
    [ObservableProperty] private bool _fri8; [ObservableProperty] private bool _fri9;
    [ObservableProperty] private bool _fri10; [ObservableProperty] private bool _fri11;
    [ObservableProperty] private bool _fri12; [ObservableProperty] private bool _fri13;

    [ObservableProperty] private bool _sat0; [ObservableProperty] private bool _sat1;
    [ObservableProperty] private bool _sat2; [ObservableProperty] private bool _sat3;
    [ObservableProperty] private bool _sat4; [ObservableProperty] private bool _sat5;
    [ObservableProperty] private bool _sat6; [ObservableProperty] private bool _sat7;
    [ObservableProperty] private bool _sat8; [ObservableProperty] private bool _sat9;
    [ObservableProperty] private bool _sat10; [ObservableProperty] private bool _sat11;
    [ObservableProperty] private bool _sat12; [ObservableProperty] private bool _sat13;

    [ObservableProperty] private bool _sun0; [ObservableProperty] private bool _sun1;
    [ObservableProperty] private bool _sun2; [ObservableProperty] private bool _sun3;
    [ObservableProperty] private bool _sun4; [ObservableProperty] private bool _sun5;
    [ObservableProperty] private bool _sun6; [ObservableProperty] private bool _sun7;
    [ObservableProperty] private bool _sun8; [ObservableProperty] private bool _sun9;
    [ObservableProperty] private bool _sun10; [ObservableProperty] private bool _sun11;
    [ObservableProperty] private bool _sun12; [ObservableProperty] private bool _sun13;

    [ObservableProperty] private string _sleepQuality = "Bon";
    [ObservableProperty] private string _sleepQuantity = "Bon";
    [ObservableProperty] private string _dietQuality = "Bonne";
    [ObservableProperty] private string _dietQuantity = "Bonne";

    public List<string> SleepQualityOptions { get; } = new() { "Tres mauvais", "Mauvais", "Moyen", "Bon", "Tres bon" };
    public List<string> DietQualityOptions { get; } = new() { "Tres mauvaise", "Mauvaise", "Moyenne", "Bonne", "Tres bonne" };

    // Step 5 - Activites Physiques et Sportives
    public ObservableCollection<SportActivity> SportActivities { get; } = new();
    [ObservableProperty] private string _newSportName = string.Empty;
    [ObservableProperty] private string _newSportWeeklyFreq = "3 a 4 X semaine";
    [ObservableProperty] private string _newSportDailyFreq = "1h";
    [ObservableProperty] private string _newSportLevel = "Debutant";

    public List<string> SportLevelOptions { get; } = new() { "Debutant", "Intermediaire", "Avance", "Expert" };
    public List<string> WeeklyFreqOptions { get; } = new() { "1 X semaine", "2 X semaine", "3 a 4 X semaine", "5+ X semaine" };
    public List<string> DailyFreqOptions { get; } = new() { "30 min", "1h", "1h - 1h30", "1h30 - 2h", "2h+" };

    public Action? NavigateToLogin { get; set; }

    public RegisterViewModel(IMockDataService dataService)
    {
        _dataService = dataService;
        Title = "Creer un compte";
    }

    partial void OnCurrentStepChanged(int value)
    {
        OnPropertyChanged(nameof(IsStep1));
        OnPropertyChanged(nameof(IsStep2));
        OnPropertyChanged(nameof(IsStep3));
        OnPropertyChanged(nameof(IsStep4));
        OnPropertyChanged(nameof(IsStep5));
        OnPropertyChanged(nameof(CanGoBack));
        OnPropertyChanged(nameof(StepLabel));
        OnPropertyChanged(nameof(NextButtonText));
        OnPropertyChanged(nameof(BackButtonText));
        OnPropertyChanged(nameof(Step2Color));
        OnPropertyChanged(nameof(Step3Color));
        OnPropertyChanged(nameof(Step4Color));
        OnPropertyChanged(nameof(Step5Color));
    }

    [RelayCommand]
    public void NextStep()
    {
        if (CurrentStep < TotalSteps) CurrentStep++;
    }

    [RelayCommand]
    public void PreviousStep()
    {
        if (CurrentStep > 1) CurrentStep--;
    }

    [RelayCommand]
    private void AddProfessionalActivity()
    {
        if (string.IsNullOrWhiteSpace(NewProActivityName)) return;
        ProfessionalActivities.Add(new ProfessionalActivity
        {
            Name = NewProActivityName,
            PhysicalFatigue = NewProPhysicalFatigue,
            MentalFatigue = NewProMentalFatigue
        });
        NewProActivityName = string.Empty;
    }

    [RelayCommand]
    private void RemoveProfessionalActivity(ProfessionalActivity activity)
        => ProfessionalActivities.Remove(activity);

    [RelayCommand]
    private void AddSportActivity()
    {
        if (string.IsNullOrWhiteSpace(NewSportName)) return;
        SportActivities.Add(new SportActivity
        {
            Name = NewSportName,
            WeeklyFrequency = NewSportWeeklyFreq,
            DailyFrequency = NewSportDailyFreq,
            Level = NewSportLevel
        });
        NewSportName = string.Empty;
    }

    [RelayCommand]
    private void RemoveSportActivity(SportActivity activity)
        => SportActivities.Remove(activity);

    [RelayCommand]
    public async Task CreateAccountAsync()
    {
        if (IsBusy) return;
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
        { await Application.Current!.MainPage!.DisplayAlert("Erreur", "Le prenom et le nom sont obligatoires.", "OK"); return; }
        if (string.IsNullOrWhiteSpace(Email))
        { await Application.Current!.MainPage!.DisplayAlert("Erreur", "L adresse email est obligatoire.", "OK"); return; }
        if (string.IsNullOrWhiteSpace(Password))
        { await Application.Current!.MainPage!.DisplayAlert("Erreur", "Le mot de passe est obligatoire.", "OK"); return; }
        if (Password != ConfirmPassword)
        { await Application.Current!.MainPage!.DisplayAlert("Erreur", "Les mots de passe ne correspondent pas.", "OK"); return; }
        try
        {
            IsBusy = true;
            int.TryParse(MaxHeartRateText, out int maxHr);
            int.TryParse(RestHeartRateText, out int restHr);
            int.TryParse(WeeklyTrainingCountText, out int weeklyTraining);
            int.TryParse(SessionsPerTrainingText, out int sessionsPerTraining);
            var user = new User
            {
                FirstName = FirstName, LastName = LastName, Gender = Gender,
                BirthDate = BirthDate, Email = Email, Phone = Phone, Password = Password,
                Presentation = Presentation, Address = Address, PostalCode = PostalCode,
                City = City, Country = Country, MaritalStatus = MaritalStatus,
                PartnerName = PartnerName, PersonalPhysicalFatigue = PersonalPhysicalFatigue,
                PersonalMentalFatigue = PersonalMentalFatigue,
                PersonalFatigueComment = PersonalFatigueComment,
                ProfessionalActivities = ProfessionalActivities.ToList(),
                ProfessionalFatigueComment = ProfessionalFatigueComment,
                TobaccoConsumption = TobaccoConsumption, AlcoholConsumption = AlcoholConsumption,
                DrugConsumption = DrugConsumption, MovementLimitations = MovementLimitations,
                PlannedOperation = PlannedOperation, MedicalCondition = MedicalCondition,
                SpecificMedicalTreatment = SpecificMedicalTreatment,
                MaxHeartRate = maxHr, RestHeartRate = restHr,
                WeeklyTrainingCount = weeklyTraining, SessionsPerTraining = sessionsPerTraining,
                TimePerSession = TimePerSession, SleepQuality = SleepQuality,
                SleepQuantity = SleepQuantity, DietQuality = DietQuality,
                DietQuantity = DietQuantity, SportActivities = SportActivities.ToList()
            };
            var success = await _dataService.CreateAccountAsync(user);
            if (success)
            {
                await Application.Current!.MainPage!.DisplayAlert("Succes", $"Bienvenue {FirstName} ! Votre compte a ete cree.", "OK");
                NavigateToLogin?.Invoke();
            }
            else
                await Application.Current!.MainPage!.DisplayAlert("Erreur", "Un compte avec cet email existe deja.", "OK");
        }
        finally { IsBusy = false; }
    }
}