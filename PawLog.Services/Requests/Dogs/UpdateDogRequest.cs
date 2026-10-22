namespace PawLog.Services.Requests.Dogs
{
    public record UpdateDogRequest(string Name, string? Breed, DateOnly? DateOfBirth);
}
