namespace PawLog.Api.Requests.TrainingSessions
{
    public record UpdateTrainingSessionRequest(
        DateTime Date,
        int Repetitions,
        string? Reward,
        int Rating,
        string? Notes,
        int DogId,
        int ExerciseId);
}
