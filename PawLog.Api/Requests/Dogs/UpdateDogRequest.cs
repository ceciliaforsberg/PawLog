namespace PawLog.Api.Requests.Dogs
{
    public record UpdateDogRequest(string Name, string? Breed, DateOnly? DateOfBirth);
}
