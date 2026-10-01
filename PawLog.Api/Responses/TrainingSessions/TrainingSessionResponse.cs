namespace PawLog.Api.Responses.TrainingSessions
{
    public record TrainingSessionResponse(
        int Id,
        DateTime Date,
        int Repetitions,
        string? Reward,
        int Rating,
        string? Notes,
        int DogId,
        string DogName,
        int ExerciseId,
        string ExerciseName);
}
