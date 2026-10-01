namespace PawLog.Api.Requests.Dogs
{
    public record CreateDogRequest(string Name, string? Breed, DateOnly? DateOfBirth);
}
