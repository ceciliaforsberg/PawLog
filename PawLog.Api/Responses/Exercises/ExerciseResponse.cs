namespace PawLog.Api.Responses.Exercises
{
    public record ExerciseResponse(int Id, string Name, string? Description, bool IsArchived);
}
