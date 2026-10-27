using Microsoft.EntityFrameworkCore;
using PawLog.Data;
using PawLog.Models;
using PawLog.Services.Interfaces;
using PawLog.Services.Results;
using PawLog.Services.Requests.TrainingSessions;
using PawLog.Services.Responses.TrainingSessions;

namespace PawLog.Services.Core;

public class TrainingSessionService: ITrainingSessionService
{
    private readonly PawLogDbContext _context;

    public TrainingSessionService(PawLogDbContext context) => _context = context;

    public async Task<List<TrainingSessionResponse>> GetAllAsync(int? dogId)
    {
        var query = _context.TrainingSessions.AsQueryable();

        if (dogId is not null) query = query.Where(s => s.DogId == dogId);

        return await query
            .OrderByDescending(s => s.Date)
            .Select(s => new TrainingSessionResponse(
                s.Id,
                s.Date,
                s.Repetitions,
                s.Reward,
                s.Rating,
                s.Notes,
                s.DogId,
                s.Dog.Name,
                s.ExerciseId,
                s.Exercise.Name))
            .ToListAsync();
    }

    public async Task<TrainingSessionResponse?> GetByIdAsync(int id)
    {
        var session = await _context.TrainingSessions
            .Include(s => s.Dog)
            .Include(s => s.Exercise)
            .FirstOrDefaultAsync(s => s.Id == id);
        return session is null ? null : ToResponse(session); 
    }

    public async Task<ServiceResult<TrainingSessionResponse>> CreateAsync (CreateTrainingSessionRequest request)
    {
        var dog = await _context.Dogs.FindAsync(request.DogId);
        if (dog is null) return ServiceResult<TrainingSessionResponse>.Invalid($"Dog with {request.DogId} not found");

        var exercise = await _context.Exercises.FindAsync(request.ExerciseId);
        if (exercise is null || exercise.IsArchived) return ServiceResult<TrainingSessionResponse>.Invalid($"Exercise with {request.ExerciseId} not found");
        
        var session = new TrainingSession
        {
            Date = request.Date,
            Repetitions = request.Repetitions,
            Reward = request.Reward,
            Rating = request.Rating,
            Notes = request.Notes,
            Dog = dog,
            Exercise = exercise
        };

        _context.TrainingSessions.Add(session);
        await _context.SaveChangesAsync();

        return ServiceResult<TrainingSessionResponse>.Success(ToResponse(session));
    }

    public async Task<ServiceResult<TrainingSessionResponse>> UpdateAsync (int id, UpdateTrainingSessionRequest request)
    {
        var session = await _context.TrainingSessions.FindAsync(id);
        if (session is null) return ServiceResult<TrainingSessionResponse>.NotFound();

        var dog = await _context.Dogs.FindAsync(request.DogId);
        if (dog is null) return ServiceResult<TrainingSessionResponse>.Invalid($"Dog with {request.DogId} not found");

        var exercise = await _context.Exercises.FindAsync(request.ExerciseId);
        if (exercise is null) return ServiceResult<TrainingSessionResponse>.Invalid($"Exercise with {request.ExerciseId} not found");

        var isChangingExercise = exercise.Id != session.ExerciseId;
        if (isChangingExercise || exercise.IsArchived) return ServiceResult<TrainingSessionResponse>.Invalid($"Cannot change to archived exercise with ID: {request.ExerciseId}");

        session.Date = request.Date;
        session.Repetitions = request.Repetitions;
        session.Reward = request.Reward;
        session.Rating = request.Rating;
        session.Notes = request.Notes;
        session.Dog = dog;
        session.Exercise = exercise;

        await _context.SaveChangesAsync();

        return ServiceResult<TrainingSessionResponse>.Success(ToResponse(session));
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var session = await _context.TrainingSessions.FindAsync(id);
        if (session is null) return false;

        _context.TrainingSessions.Remove(session);
        await _context.SaveChangesAsync();
        return true;
    }

    private static TrainingSessionResponse ToResponse(TrainingSession session) =>
        new (session.Id, session.Date, session.Repetitions, session.Reward,session.Rating, session.Notes, session.DogId, session.Dog.Name, session.ExerciseId, session.Exercise.Name);
}
