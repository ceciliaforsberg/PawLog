namespace PawLog.Services.Responses.Dogs
{
    public record DogResponse(int Id, string Name, string? Breed, DateOnly? DateOfBirth);
}
