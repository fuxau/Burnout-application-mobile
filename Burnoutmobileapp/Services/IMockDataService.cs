using Burnoutmobileapp.Models;

namespace Burnoutmobileapp.Services;

public interface IMockDataService
{
    Task<List<Event>> GetEventsAsync();
    Task<Event?> GetEventByIdAsync(int id);
    Task<List<Event>> GetEventsByCategoryAsync(string category);
    Task<List<Coach>> GetCoachesAsync();
    Task<Coach?> GetCoachByIdAsync(int id);
    Task<List<Challenge>> GetChallengesAsync();
    Task<Challenge?> GetChallengeByIdAsync(int id);
    Task<User> GetCurrentUserAsync();
    Task<Session> GetNextSessionAsync();
    Task<WorkoutSession> GetTodayWorkoutSessionAsync();
    Task<List<WorkoutSession>> GetSessionsForDateAsync(DateTime date);
    Task<bool> LoginAsync(string email, string password);
    Task<bool> CreateAccountAsync(User user);
    Task<bool> RegisterForEventAsync(int eventId);
    Task<bool> JoinChallengeAsync(int challengeId);
}