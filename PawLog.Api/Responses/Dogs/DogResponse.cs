namespace PawLog.Api.Responses.Dogs
{
    public record DogResponse(int Id, string Name, string? Breed, DateOnly? DateOfBirth);
}
