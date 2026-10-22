using PawLog.Services.Requests.Exercises;
using PawLog.Services.Responses.Exercises;

namespace PawLog.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<List<ExerciseResponse>> GetAllAsync(bool includeArchived = false);
        Task<ExerciseResponse?> GetByIdAsync(int id);
        Task<ExerciseResponse> CreateAsync(CreateExerciseRequest request);
        Task<ExerciseResponse?> UpdateAsync(int id, UpdateExerciseRequest request);
        Task<bool> ArchiveAsync(int id);
    }
}
