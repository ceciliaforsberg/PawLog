namespace PawLog.Services.Responses.Exercises
{
    public record ExerciseResponse(int Id, string Name, string? Description, bool IsArchived);
}
