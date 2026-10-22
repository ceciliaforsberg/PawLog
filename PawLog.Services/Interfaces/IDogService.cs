using PawLog.Services.Requests.Dogs;
using PawLog.Services.Responses.Dogs;

namespace PawLog.Services.Interfaces
{
    public interface IDogService
    {
        Task<List<DogResponse>> GetAllAsync(bool includeArchived = false);
        Task<DogResponse?> GetByIdAsync(int id);
        Task<DogResponse> CreateAsync(CreateDogRequest request);
        Task<DogResponse?> UpdateAsync(int id, UpdateDogRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
