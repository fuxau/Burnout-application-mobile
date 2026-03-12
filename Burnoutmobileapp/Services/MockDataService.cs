using Burnoutmobileapp.Models;

namespace Burnoutmobileapp.Services;

public class MockDataService : IMockDataService
{
    private readonly List<Event> _events;
    private readonly List<Coach> _coaches;
    private readonly List<Challenge> _challenges;
    private readonly User _currentUser;
    private readonly Session _nextSession;
    private readonly List<User> _registeredUsers = new();

    public MockDataService()
    {
        _coaches = new List<Coach>
        {
            new Coach
            {
                Id = 1,
                Name = "Jean Dupont",
                Specialty = "Expert Musculation",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBmbwEFj5TKWV-201_ftoAkWLTZEd29MqEBarRXRKAnS8MWHmDpNJwkwwpQP6IvXLPH3IK_Sn_YuqAVP1zqAd8_9zvHbpQSEb8IytCit9HKsvOq4NSl9j7gDSm5uriuW4e60EQ9B-KnYKf7KfpIgp3cd6nSV66k9hTQBuV4mt6h7W5mCjv9XzBJpa16Aoql-eOJB_z72UffpDjA2Rz3Eua52hS2ZGA9qJ6bF-UUYzmhTCeMqIHREeD3NeJwQwA9c4b1KvB0WxvKT5rP",
                Availability = "Disponible aujourd'hui à 14h",
                IsAvailable = true,
                IsPro = true
            },
            new Coach
            {
                Id = 2,
                Name = "Marie Curie",
                Specialty = "Yoga Master",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuAkibmsICns5DYlRd3uBudFy4LwEz896-mw4nmEmtYWIBuArdlVPBJ6OjsIFTYy64malPI_5MY7ewpUC8Eqqb5Gs7nE9A7yoFM-yAsKVijFJy67tzr9CFoNuhWIgfnPz1iXC_UKGhJdX5_aMhszACADZbG9R8e1gDfDqFOA9fMzbtxe8XNtUpbctrEb-BiuDuUg71MjwGPIDpEbiFx_X9BnTEx8Gv1n016VCHxRXkSn8WStLEb0ejqRIjjXcUtbxWhW4kCOcdu_Etq_",
                Availability = "En séance actuellement",
                IsAvailable = false,
                IsPro = true
            },
            new Coach
            {
                Id = 3,
                Name = "Lucas Martin",
                Specialty = "Crossfit & Cardio",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCeGfbQBqxIC3tOEV4QDWtpFFBKls7yyIzKfN-0_Tq0HzkEhraMTr9wYzX80Odm_wp6LjAs_Awi0V96gT9BvfMN8HucXyEGva0UVDgUkdidrdqfpk2vS-8GReFrgGXiqx45oXonz0RU49ASoNpO3PYJ45TOlCklYMkHi7XhV_0JatiU5bJ7MEOSjT__ZaBW7Gz3AssBA2Smftm2pZbDLkGmop2-woBl9u87el0-QKXdz0mx77YWk52CLH8GEof6EDa5heZ_KmTZCBdA",
                Availability = "Absent jusqu'au 15 Oct",
                IsAvailable = false,
                IsPro = false
            },
            new Coach
            {
                Id = 4,
                Name = "Sophie Bernard",
                Specialty = "Nutritionniste",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuADAchHqxLUlaykGUxbJG4DFP-CTNuyJ0zMKmMiWXaKI3PO72hFtOYRANl6u0oN8y4rLrzVHr6kcz5iunhDnKod9OCQyZR6QEaY_g2hdvCtkU2Wvfi_Gv5nRCJg37DYn4oawo4RA_bPY1q_ElSY108toC9SjOQVBNnaHfDO8a9skjMbo2vP-lRrYDwRCjzke3SFj06B3dACnd8DsUSJ62npR1PWQsCpwN0aQ7FowotxbvZKzoWeGBEW9ytsUYpWlGp6iSFY__nuq9ps",
                Availability = "Disponible demain matin",
                IsAvailable = true,
                IsPro = true
            },
            new Coach
            {
                Id = 5,
                Name = "Sarah Leclere",
                Specialty = "Yoga & Flexibilité",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCmDxM0SYPbZ_8EFhZA0SZrGj5QuZfkRMYgjZ94zbZuJVrUAGVce8_2LSzpEcK5xEy-wMvMBC7kMfedwhnuvdRSjHoTGHCSsMZhOqbi2LfRdoK8wB3b3-7pHLjDyIzU3RWvmrMcagGNP-99qmEK106dQtojGcFqWUEf2S1bS1-JmyGxh2-fZzp1I6mZC9vevjMDRQ2zZcH9iKhS3pB2kew2ps8lke6KXGSWBy9goLW3XeiIgU9VJD-jIwK7I0wEVAVkngvBMdyax13m",
                Availability = "Disponible",
                IsAvailable = true,
                IsPro = true
            },
            new Coach
            {
                Id = 6,
                Name = "Marc Dubois",
                Specialty = "Force & Musculation",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuANZVhojxzwzQDxwfoAoIVtZw6t8x9BDKxzS6DoEgfiJASpk56W4wEfeWno0R3Ejr9pz_Ba1ajVLlzz6heY_OTDWk1gk8UU6mQ6mY2otbRlqOoaXY6pnURYMs3JHr3TL0IEGdhWHm-9woRWEQR3ju_UqKZfIsMb_WiYE27yql-p-mEVdfoJDAnIX_pmTeZRd0aS30zZo0QbFCTMFxbUgbmzP2-xZk6Vinu8TZMssfOW40l_HjisuCx9OgVj6k-j42Sv7yEFSw6K9YIn",
                Availability = "Disponible",
                IsAvailable = true,
                IsPro = true
            }
        };

        _events = new List<Event>
        {
            new Event
            {
                Id = 1,
                Title = "Morning Yoga Flow",
                Category = "Yoga",
                Description = "Rejoignez-nous pour une session intense de Yoga Flow matinal conçue pour les membres du \"Call of Phoenix\". Ce cours combine respiration et mouvements dynamiques pour améliorer votre flexibilité et renforcer vos muscles profonds. N'oubliez pas votre serviette !",
                Date = new DateTime(2024, 10, 12),
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(11, 0, 0),
                Location = "Paris",
                LocationDetail = "Salle A • Call of Phoenix Gym",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBQFy95_ingZGKzx-cCcbukWIdy3e1V-3dKezBXyT1g650wtyiYUNabHw1QHIkxb0xqEcffYjsoC31ZjpNBmmf7anEDLQ2U3tYlG3kiiqCQ-vgsiDfCMshw1tyXrMz78bMrEzQ28tQd2Pla2alOl2KGznY8wjwtRTcR2w6wBL2z3aYPedpRtvLrDcUC3aLr47V-X8HJ9sICxyV5cINk9Wc4o_50jZmN48lpGmi0WHPrzLt2lqgintBQxs88mhBGhcOsBa_DZ3WqnYUT",
                Price = 15,
                MaxParticipants = 20,
                CurrentParticipants = 16,
                Coach = _coaches[4],
                Intensity = "Intense",
                Level = "Intermédiaire",
                ParticipantAvatars = new List<string>
                {
                    "https://lh3.googleusercontent.com/aida-public/AB6AXuCgV4NouZ4LBunLyAmZoRsuTz3HNsRxqord7kbtjM0q5XP1ZSKNpt0Vyy4n9mpbophd3AVxNdwTsJhV1ZD0CqOYN2ESD2QxIRo2veMmyI8O0DufdFNomcqwwft6W9Oy0Dgdq5hA9CCHCw_L_Jo1fWbVk4M32bx8DYU7X9ybxQE9a7eTEgmnd_YmzvKN_lR473ZyjIWSJwPnMrn9U27RqYCBNS2fugfTqRBnZY_wvtgWloOwoLhKU1WR8Pc1QJQWPR82LlMuGme08pU8",
                    "https://lh3.googleusercontent.com/aida-public/AB6AXuC24ppwcvNOSRshCy_bXVf1oTj21M-wDStF4d344xJeeXPJdiooarRL_GKKgdjhHeFNmT-xtVmRFPS2VdTFt9BgLlCwy_DqAfN-hLLyhn5DVGnStcVmHTb7EpmSG-_bL024Jmr4asN75Qah1lZaH0JMeN9dFa05EaSYwqVd-fdW30dphjrQ7OpctRWge1IPxeBTaJVpMyWn-pAZMmiyQMefIoIw_CmqAQPU5EJeUAJq9bh_rqL59-G-fnBVuDXKKZa2bzhdpYk9nlvy",
                    "https://lh3.googleusercontent.com/aida-public/AB6AXuDEcYMqWhe-mlsLvGvA96xIR2_2YjjkAbx0Sy-VZljg4UhMIPN1GpnY1LR1t-SmqDdPQ2QkHUVnSOpJJD-kkwD4Aknv5Ixm2gCsSMv4U1TKHpW6fzJXFxFLrg2vuklDzWQVeGvO7lasE6DqjwrK4QMCoB-3Ry15R6dRI3-4_okeL8GKZTG43d3qa1fau-d4LqviICsE84DX4fHHi-BFw7kt-jpYSfKs4a42CMLRF5679vYj_xF2uV4wuV99Ickc8aN4E1JV3Qg3zRyv"
                }
            },
            new Event
            {
                Id = 2,
                Title = "Urban Cross Training",
                Category = "Cardio",
                Description = "Entraînement en plein air combinant cardio et renforcement musculaire.",
                Date = new DateTime(2024, 10, 14),
                StartTime = new TimeSpan(18, 30, 0),
                EndTime = new TimeSpan(19, 30, 0),
                Location = "Annecy",
                LocationDetail = "Le Pâquier",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCIDz6uARmNX2mf6bjr2OggXNnG7nMru03wXPIE1Ow1JZkbIyPgU8wZslMHeam_el3QtGxzFiYSH_f0I-j0245CAuvyL33hRUgcWKuWcGqcab4PGEsYQ1US4QvHgo1Dn0BBB_eNSAIhZxleOHoka0SttfIagm1W9dg0KR0FeFOrsNjzEgT_6NFPMkFUk3sM--V6Do123o6nX5_ORRNvxoYhY6NrZE1UCcKtFHx0MF7Q4UfFOVHmy5hW3WDufvbK-aWIMUymB5kJnA9k",
                Price = 0,
                MaxParticipants = 50,
                CurrentParticipants = 28,
                Coach = _coaches[2],
                Intensity = "Élevée",
                Level = "Tous niveaux",
                ParticipantAvatars = new List<string>
                {
                    "https://lh3.googleusercontent.com/aida-public/AB6AXuAK0mkE7r2vjHawQs4V3HoNmL07iFe1ruitDLLaBgo8v_aTTGC17FB-aP1j0Rn0uiSjCgmnM1wxLRry0gqW9lNaTGhQG_-SzQsGXMdgwEUPTVzJ1FC_jhmweO4fFJGnIYs1ZlTOy4p7aRobzcVEzr00zq17oM_IiSFQwyaEprP08CPAiqgAYzoC9_yz7vQNSJkAJIYadtZSNrgGeYr5lBGxLEkGZBrvcz7jiGBXuq67qXmzp-w86kQ1UOuEORf39pk708DEHTHxZl0w",
                    "https://lh3.googleusercontent.com/aida-public/AB6AXuCmDxM0SYPbZ_8EFhZA0SZrGj5QuZfkRMYgjZ94zbZuJVrUAGVce8_2LSzpEcK5xEy-wMvMBC7kMfedwhnuvdRSjHoTGHCSsMZhOqbi2LfRdoK8wB3b3-7pHLjDyIzU3RWvmrMcagGNP-99qmEK106dQtojGcFqWUEf2S1bS1-JmyGxh2-fZzp1I6mZC9vevjMDRQ2zZcH9iKhS3pB2kew2ps8lke6KXGSWBy9goLW3XeiIgU9VJD-jIwK7I0wEVAVkngvBMdyax13m"
                }
            },
            new Event
            {
                Id = 3,
                Title = "Technique Powerlifting",
                Category = "Force",
                Description = "Masterclass sur les techniques de powerlifting avec un coach expert.",
                Date = new DateTime(2024, 10, 20),
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(16, 0, 0),
                Location = "Paris",
                LocationDetail = "Zone Force",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCmDxM0SYPbZ_8EFhZA0SZrGj5QuZfkRMYgjZ94zbZuJVrUAGVce8_2LSzpEcK5xEy-wMvMBC7kMfedwhnuvdRSjHoTGHCSsMZhOqbi2LfRdoK8wB3b3-7pHLjDyIzU3RWvmrMcagGNP-99qmEK106dQtojGcFqWUEf2S1bS1-JmyGxh2-fZzp1I6mZC9vevjMDRQ2zZcH9iKhS3pB2kew2ps8lke6KXGSWBy9goLW3XeiIgU9VJD-jIwK7I0wEVAVkngvBMdyax13m",
                Price = 30,
                MaxParticipants = 15,
                CurrentParticipants = 7,
                Coach = _coaches[5],
                Intensity = "Intense",
                Level = "Avancé",
                ParticipantAvatars = new List<string>
                {
                    "https://lh3.googleusercontent.com/aida-public/AB6AXuCgV4NouZ4LBunLyAmZoRsuTz3HNsRxqord7kbtjM0q5XP1ZSKNpt0Vyy4n9mpbophd3AVxNdwTsJhV1ZD0CqOYN2ESD2QxIRo2veMmyI8O0DufdFNomcqwwft6W9Oy0Dgdq5hA9CCHCw_L_Jo1fWbVk4M32bx8DYU7X9ybxQE9a7eTEgmnd_YmzvKN_lR473ZyjIWSJwPnMrn9U27RqYCBNS2fugfTqRBnZY_wvtgWloOwoLhKU1WR8Pc1QJQWPR82LlMuGme08pU8"
                }
            }
        };

        _challenges = new List<Challenge>
        {
            new Challenge
            {
                Id = 1,
                Title = "30 Day Squat",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuD09BNViI4muxpjteb6UDdiYjWrlI-T7qYWt3Nko9G64mpdOIPRKwPhKrpj8_SPDeQc48e2gQu_2bm2IfqOZFIqm3IjDO6FH96hivgIGjy9Wz-qpt_RjZcjaKdNGGz4TG79-bhI8KkFypB_smAWyS5Oh6YVkVAmP7kjdC9C53pGrvh_1aRMrQIoXfbk8-asMvHD2TySStO3e0tDjVxltXRYv8yLziZehY1tzmZTKkpvuyKjrBuuQNsHrFg9M43Ec8gkLRwWeYOMJ4SP",
                TotalDays = 30,
                CompletedDays = 10,
                IsJoined = true,
                TodayTask = "50 Squats",
                IconName = "fitness_center",
                DayCompletionStatus = Enumerable.Range(1, 30).Select(i => i <= 10).ToList()
            },
            new Challenge
            {
                Id = 2,
                Title = "Phoenix Rising",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuA_tAhmMg_CrLYpV6Lkg2Z39HznWkBIhzUJiqTFaLIG-ai-z5ZKZ4EZnYIrBJi0NFGIFMyjJCbQk2mHqlaPFBp4170Mx9PAnIAHDO-9meVWF4kG9f4IwaJ3MgofogQPIRtxQ4P5m6tsz3cDjgVjCf5tP_9CejycVcNcS260yc6eV031kZP8R6QaW4H2o2GKaorc4oAGfWBI9doPMoUOpgrf-1l-0kQppweAgE9IpA0EsVDQTdYNIYnYJS7Vst0WFXMSzkR97DgS7peL",
                TotalDays = 7,
                CompletedDays = 5,
                IsJoined = true,
                TodayTask = "30 min Rowing",
                IconName = "rowing"
            },
            new Challenge
            {
                Id = 3,
                Title = "Cardio Blast",
                ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBCIKdpDRa3uNgdr9Qtgj8gc_2CUH1QBowro-tqEDlcM9ils27iBvTwi4miWXkxy1c0Os2v7M-Dabc0XCDCi2ENS0aDsqZVj5Le2c1d508DT7wmBm_VjTGx3_hqxPqmm6r9lu96y3RhYkaXnMeEVaHKN7LNzwmqGGXcM-p57OhgvApWSR-ickFua_6GJK_s3iWkVXq5-IT0RkJsBy2BBc-C982USr64wH8PFH6nLa1KMTZuqv1hFATiTKWSSZqMlPMLzI7dkUyOQe2u",
                TotalDays = 14,
                CompletedDays = 0,
                IsJoined = false,
                TodayTask = "20 min Run",
                IconName = "directions_run"
            }
        };

        _currentUser = new User
        {
            Id = 1,
            FirstName = "Thomas",
            LastName = "Anderson",
            Email = "thomas.anderson@example.com",
            Phone = "+33 6 12 34 56 78",
            Address = "15 Rue de la Paix, 75002 Paris",
            ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuAybvQxmSQuXivTHv21xeh35fRUsdZQO5YhYuIGnEFn2T0yfV1kUDli6rwsnHR6qB6x-MDQLlb1wk7v-j0zr9zjDuCwiBGGmba5gj3hd0xbwSiYPzgIDqfkPCgvBzBe5MqOCHTN1eGBTZ6V-O8oR92L9PaCCnwxa0bPyFcobtyQUTJfLGq9-MC1XLVQ7YCqFJRq3Q1rukSdJtM09ZVmBAG45WiCZe1-eD0onHoxM0uWjn6xkKZgigPDEfhNIv_mO2cjjwEwnj79E3T7",
            MembershipLevel = "Phoenix Elite",
            MemberSince = 2021,
            BirthDate = new DateTime(1995, 3, 22),
            Gender = "Homme",
            BloodType = "O+",
            FavoriteSport = "Crossfit & Powerlifting",
            Height = 180,
            Weight = 75,
            WeeklyVisits = 3,
            CaloriesBurned = 1240
        };

        _nextSession = new Session
        {
            Id = 1,
            Title = "Chest Day",
            Category = "Force",
            Date = DateTime.Today,
            Time = new TimeSpan(17, 30, 0),
            ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuAnIGVS58sxYo_FKXHXKtyEgb2kkO8XU25VsgQSiG_Y4HM0jzAPAZjWlsscSm1nc1izJJGVT6eALKE2jmjNDWUqujqycDGpDe-QWZT4gQD9-lALkDGKJJrb2hgGenlHDp9BR6-QPC1s2I6aQTIZC41p4lYSLdI9VIWJwMfQ0hVOIX1ANpO1B0LnqhuqiZLbtUnfVWFki0Jonyn69R7wNgOLLGZamh6yYV58KtsfTr9sjzcln-wx5bwwtJA02rvSv6Oi7fcs2PqaKyPZ",
            Coach = _coaches[5]
        };
    }

    public Task<List<Event>> GetEventsAsync() => Task.FromResult(_events);
    
    public Task<Event?> GetEventByIdAsync(int id) => Task.FromResult(_events.FirstOrDefault(e => e.Id == id));
    
    public Task<List<Event>> GetEventsByCategoryAsync(string category) =>
        Task.FromResult(string.IsNullOrEmpty(category) || category == "Tous" 
            ? _events 
            : _events.Where(e => e.Category == category || e.Location == category).ToList());

    public Task<List<Coach>> GetCoachesAsync() => Task.FromResult(_coaches);
    
    public Task<Coach?> GetCoachByIdAsync(int id) => Task.FromResult(_coaches.FirstOrDefault(c => c.Id == id));

    public Task<List<Challenge>> GetChallengesAsync() => Task.FromResult(_challenges);
    
    public Task<Challenge?> GetChallengeByIdAsync(int id) => Task.FromResult(_challenges.FirstOrDefault(c => c.Id == id));

    public Task<User> GetCurrentUserAsync() => Task.FromResult(_currentUser);
    
    public Task<Session> GetNextSessionAsync() => Task.FromResult(_nextSession);

    public Task<bool> LoginAsync(string email, string password)
    {
        return Task.FromResult(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password));
    }

    public Task<bool> CreateAccountAsync(User user)
    {
        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            return Task.FromResult(false);
        var exists = _registeredUsers.Any(u => u.Email == user.Email);
        if (exists) return Task.FromResult(false);
        user.Id = _registeredUsers.Count + 100;
        user.MembershipLevel = "Phoenix Member";
        user.MemberSince = DateTime.Today.Year;
        _registeredUsers.Add(user);
        return Task.FromResult(true);
    }

    public Task<bool> RegisterForEventAsync(int eventId)
    {
        var evt = _events.FirstOrDefault(e => e.Id == eventId);
        if (evt != null && evt.CurrentParticipants < evt.MaxParticipants)
        {
            evt.CurrentParticipants++;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> JoinChallengeAsync(int challengeId)
    {
        var challenge = _challenges.FirstOrDefault(c => c.Id == challengeId);
        if (challenge != null)
        {
            challenge.IsJoined = true;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<WorkoutSession> GetTodayWorkoutSessionAsync()
    {
        var session = new WorkoutSession
        {
            Id = 1,
            Title = "Séance Musculation",
            ProgramName = "Programme Force Erik",
            WeekLabel = "Semaine 1",
            SessionLabel = "Séance 1",
            Type = "MUSCU",
            Date = DateTime.Today,
            Time = new TimeSpan(17, 30, 0),
            Coach = _coaches[5],
            Blocks = new List<WorkoutBlock>
            {
                new WorkoutBlock
                {
                    Name = "Échauffement Prophylactique",
                    Exercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Id = 1,
                            Name = "Rotation épaules + mobilité hanches",
                            SeriesCount = 2,
                            RepsPerSerie = 10,
                            RecupSeconds = 0,
                            Note = "Mouvement lent et contrôlé",
                            Sets = new List<WorkoutSet>
                            {
                                new WorkoutSet { SetNumber = 1, Reps = 10 },
                                new WorkoutSet { SetNumber = 2, Reps = 10 }
                            }
                        }
                    }
                },
                new WorkoutBlock
                {
                    Name = "Core",
                    Exercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Id = 2,
                            Name = "Gainage planche",
                            SeriesCount = 3,
                            RepsPerSerie = 1,
                            RecupSeconds = 60,
                            Note = "Tenir 30 secondes par série",
                            Sets = new List<WorkoutSet>
                            {
                                new WorkoutSet { SetNumber = 1, Duration = 30 },
                                new WorkoutSet { SetNumber = 2, Duration = 30 },
                                new WorkoutSet { SetNumber = 3, Duration = 30 }
                            }
                        },
                        new WorkoutExercise
                        {
                            Id = 3,
                            Name = "Crunch bicycle",
                            SeriesCount = 3,
                            RepsPerSerie = 15,
                            RecupSeconds = 45,
                            Sets = new List<WorkoutSet>
                            {
                                new WorkoutSet { SetNumber = 1, Reps = 15 },
                                new WorkoutSet { SetNumber = 2, Reps = 15 },
                                new WorkoutSet { SetNumber = 3, Reps = 15 }
                            }
                        }
                    }
                },
                new WorkoutBlock
                {
                    Name = "Force - Poussée",
                    Exercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Id = 4,
                            Name = "Développé couché barre",
                            SeriesCount = 4,
                            RepsPerSerie = 8,
                            RecupSeconds = 120,
                            Note = "CTI > 1 alors charge indispensable",
                            Sets = new List<WorkoutSet>
                            {
                                new WorkoutSet { SetNumber = 1, Reps = 8, Weight = 80 },
                                new WorkoutSet { SetNumber = 2, Reps = 8, Weight = 82.5 },
                                new WorkoutSet { SetNumber = 3, Reps = 8, Weight = 85 },
                                new WorkoutSet { SetNumber = 4, Reps = 8, Weight = 85 }
                            }
                        },
                        new WorkoutExercise
                        {
                            Id = 5,
                            Name = "Développé incliné haltères",
                            SeriesCount = 3,
                            RepsPerSerie = 10,
                            RecupSeconds = 90,
                            Sets = new List<WorkoutSet>
                            {
                                new WorkoutSet { SetNumber = 1, Reps = 10, Weight = 28 },
                                new WorkoutSet { SetNumber = 2, Reps = 10, Weight = 30 },
                                new WorkoutSet { SetNumber = 3, Reps = 10, Weight = 30 }
                            }
                        },
                        new WorkoutExercise
                        {
                            Id = 6,
                            Name = "Dips lestés",
                            SeriesCount = 3,
                            RepsPerSerie = 10,
                            RecupSeconds = 90,
                            Sets = new List<WorkoutSet>
                            {
                                new WorkoutSet { SetNumber = 1, Reps = 10, Weight = 10 },
                                new WorkoutSet { SetNumber = 2, Reps = 10, Weight = 10 },
                                new WorkoutSet { SetNumber = 3, Reps = 10, Weight = 10 }
                            }
                        }
                    }
                },
                new WorkoutBlock
                {
                    Name = "Accessoire - Épaules / Triceps",
                    Exercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Id = 7,
                            Name = "Élévations latérales haltères",
                            SeriesCount = 3,
                            RepsPerSerie = 15,
                            RecupSeconds = 60,
                            Sets = new List<WorkoutSet>
                            {
                                new WorkoutSet { SetNumber = 1, Reps = 15, Weight = 10 },
                                new WorkoutSet { SetNumber = 2, Reps = 15, Weight = 10 },
                                new WorkoutSet { SetNumber = 3, Reps = 15, Weight = 10 }
                            }
                        },
                        new WorkoutExercise
                        {
                            Id = 8,
                            Name = "Triceps poulie haute",
                            SeriesCount = 3,
                            RepsPerSerie = 12,
                            RecupSeconds = 60,
                            Sets = new List<WorkoutSet>
                            {
                                new WorkoutSet { SetNumber = 1, Reps = 12, Weight = 25 },
                                new WorkoutSet { SetNumber = 2, Reps = 12, Weight = 25 },
                                new WorkoutSet { SetNumber = 3, Reps = 12, Weight = 27.5 }
                            }
                        }
                    }
                }
            }
        };
        return Task.FromResult(session);
    }

    public Task<List<WorkoutSession>> GetSessionsForDateAsync(DateTime date)
    {
        var dayOfWeek = date.DayOfWeek;
        var sessions = new List<WorkoutSession>();

        if (dayOfWeek == DayOfWeek.Monday || dayOfWeek == DayOfWeek.Wednesday || dayOfWeek == DayOfWeek.Friday)
        {
            sessions.Add(new WorkoutSession
            {
                Id = 1,
                Title = "Séance Musculation",
                ProgramName = "Programme Force Erik",
                WeekLabel = "Semaine 1",
                SessionLabel = "Séance 1",
                Type = "MUSCU",
                Date = date,
                Time = new TimeSpan(17, 30, 0),
                Coach = _coaches[5],
                Blocks = new List<WorkoutBlock>
                {
                    new WorkoutBlock
                    {
                        Name = "Échauffement Prophylactique",
                        Exercises = new List<WorkoutExercise>
                        {
                            new WorkoutExercise { Id = 1, Name = "Rotation épaules + mobilité hanches", SeriesCount = 2, RepsPerSerie = 10, RecupSeconds = 0, Note = "Mouvement lent et contrôlé", Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Reps = 10 }, new WorkoutSet { SetNumber = 2, Reps = 10 } } }
                        }
                    },
                    new WorkoutBlock
                    {
                        Name = "Force - Poussée",
                        Exercises = new List<WorkoutExercise>
                        {
                            new WorkoutExercise { Id = 2, Name = "Développé couché barre", SeriesCount = 4, RepsPerSerie = 8, RecupSeconds = 120, Note = "Charge indispensable", Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Reps = 8, Weight = 80 }, new WorkoutSet { SetNumber = 2, Reps = 8, Weight = 82.5 }, new WorkoutSet { SetNumber = 3, Reps = 8, Weight = 85 }, new WorkoutSet { SetNumber = 4, Reps = 8, Weight = 85 } } },
                            new WorkoutExercise { Id = 3, Name = "Développé incliné haltères", SeriesCount = 3, RepsPerSerie = 10, RecupSeconds = 90, Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Reps = 10, Weight = 28 }, new WorkoutSet { SetNumber = 2, Reps = 10, Weight = 30 }, new WorkoutSet { SetNumber = 3, Reps = 10, Weight = 30 } } },
                            new WorkoutExercise { Id = 4, Name = "Dips lestés", SeriesCount = 3, RepsPerSerie = 10, RecupSeconds = 90, Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Reps = 10, Weight = 10 }, new WorkoutSet { SetNumber = 2, Reps = 10, Weight = 10 }, new WorkoutSet { SetNumber = 3, Reps = 10, Weight = 10 } } }
                        }
                    },
                    new WorkoutBlock
                    {
                        Name = "Accessoire - Épaules / Triceps",
                        Exercises = new List<WorkoutExercise>
                        {
                            new WorkoutExercise { Id = 5, Name = "Élévations latérales haltères", SeriesCount = 3, RepsPerSerie = 15, RecupSeconds = 60, Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Reps = 15, Weight = 10 }, new WorkoutSet { SetNumber = 2, Reps = 15, Weight = 10 }, new WorkoutSet { SetNumber = 3, Reps = 15, Weight = 10 } } },
                            new WorkoutExercise { Id = 6, Name = "Triceps poulie haute", SeriesCount = 3, RepsPerSerie = 12, RecupSeconds = 60, Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Reps = 12, Weight = 25 }, new WorkoutSet { SetNumber = 2, Reps = 12, Weight = 25 }, new WorkoutSet { SetNumber = 3, Reps = 12, Weight = 27.5 } } }
                        }
                    }
                }
            });
        }
        else if (dayOfWeek == DayOfWeek.Tuesday || dayOfWeek == DayOfWeek.Thursday)
        {
            sessions.Add(new WorkoutSession
            {
                Id = 2,
                Title = "Séance Cardio",
                ProgramName = "Programme Force Erik",
                WeekLabel = "Semaine 1",
                SessionLabel = "Séance Cardio",
                Type = "CARDIO",
                Date = date,
                Time = new TimeSpan(7, 0, 0),
                Coach = _coaches[2],
                Blocks = new List<WorkoutBlock>
                {
                    new WorkoutBlock
                    {
                        Name = "Échauffement",
                        Exercises = new List<WorkoutExercise>
                        {
                            new WorkoutExercise { Id = 1, Name = "Gainage planche", SeriesCount = 3, RepsPerSerie = 1, RecupSeconds = 60, Note = "Tenir 30 secondes", Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Duration = 30 }, new WorkoutSet { SetNumber = 2, Duration = 30 }, new WorkoutSet { SetNumber = 3, Duration = 30 } } },
                            new WorkoutExercise { Id = 2, Name = "Crunch bicycle", SeriesCount = 3, RepsPerSerie = 15, RecupSeconds = 45, Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Reps = 15 }, new WorkoutSet { SetNumber = 2, Reps = 15 }, new WorkoutSet { SetNumber = 3, Reps = 15 } } }
                        }
                    },
                    new WorkoutBlock
                    {
                        Name = "Cardio Principal",
                        Exercises = new List<WorkoutExercise>
                        {
                            new WorkoutExercise { Id = 3, Name = "Course à pied", SeriesCount = 1, RepsPerSerie = 1, RecupSeconds = 0, Note = "Allure modérée", Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Duration = 1800 } } },
                            new WorkoutExercise { Id = 4, Name = "Vélo elliptique", SeriesCount = 3, RepsPerSerie = 1, RecupSeconds = 30, Sets = new List<WorkoutSet> { new WorkoutSet { SetNumber = 1, Duration = 300 }, new WorkoutSet { SetNumber = 2, Duration = 300 }, new WorkoutSet { SetNumber = 3, Duration = 300 } } }
                        }
                    }
                }
            });
        }

        return Task.FromResult(sessions);
    }
}
