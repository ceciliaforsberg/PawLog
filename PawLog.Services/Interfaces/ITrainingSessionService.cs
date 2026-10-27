using PawLog.Services.Requests.TrainingSessions;
using PawLog.Services.Responses.TrainingSessions;
using PawLog.Services.Results;

namespace PawLog.Services.Interfaces
{
    public interface ITrainingSessionService
    {
        Task<List<TrainingSessionResponse>> GetAllAsync(int? dogId);
        Task<TrainingSessionResponse?> GetByIdAsync(int id);
        Task<ServiceResult<TrainingSessionResponse>> CreateAsync(CreateTrainingSessionRequest request);
        Task<ServiceResult<TrainingSessionResponse>> UpdateAsync(int id, UpdateTrainingSessionRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
