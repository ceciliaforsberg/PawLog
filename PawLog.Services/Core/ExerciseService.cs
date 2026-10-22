using Microsoft.EntityFrameworkCore;
using PawLog.Data;
using PawLog.Models;
using PawLog.Services.Interfaces;
using PawLog.Services.Requests.Exercises;
using PawLog.Services.Responses.Exercises;

namespace PawLog.Services.Core;

public class ExerciseService : IExerciseService
{
    private readonly PawLogDbContext _context;

    public ExerciseService(PawLogDbContext context) => _context = context;
 
    public async Task<List<ExerciseResponse>> GetAllAsync(bool includeArchived)
    {
        var query = _context.Exercises.AsQueryable();

        if (!includeArchived)
        {
            query = query.Where(e => !e.IsArchived);
        }

        return await query
            .Select(e => new ExerciseResponse(e.Id, e.Name, e.Description, e.IsArchived))
            .ToListAsync();
    }

    public async Task<ExerciseResponse?> GetByIdAsync(int id)
    {
        var excercise = await _context.Exercises.FindAsync(id);
        return excercise is null ? null : ToResponse(excercise);

    }

    public async Task<ExerciseResponse> CreateAsync(CreateExerciseRequest request)
    {
        var exercise = new Exercise { Name = request.Name, Description = request.Description };

        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();
        return ToResponse(exercise);
    }

    public async Task<ExerciseResponse?> UpdateAsync(int id, UpdateExerciseRequest request)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise is null) return null;

        exercise.Name = request.Name;
        exercise.Description = request.Description;
        await _context.SaveChangesAsync();
        return ToResponse(exercise);
    }

    public async Task<bool> ArchiveAsync(int id)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise is null) return false;

        exercise.IsArchived = true;
        await _context.SaveChangesAsync();
        return true;
    }

    private static ExerciseResponse ToResponse(Exercise exercise) => 
        new (exercise.Id, exercise.Name, exercise.Description, exercise.IsArchived);

}
