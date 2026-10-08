using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawLog.Api.Requests.TrainingSessions;
using PawLog.Api.Responses.TrainingSessions;
using PawLog.Data;
using PawLog.Models;

namespace PawLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingSessionsController: ControllerBase
{
    private readonly PawLogDbContext _context;
    public TrainingSessionsController(PawLogDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<TrainingSessionResponse>> GetAll([FromQuery] int? dogId)
    {
        var query = _context.TrainingSessions.AsQueryable();

        if(dogId != null)
        {
            query = query.Where(t => t.DogId == dogId);
        }

        var sessions = await query
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
            .ToArrayAsync();
        return Ok(sessions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingSessionResponse>> GetById(int id)
    {
        var session = await _context.TrainingSessions
            .Where(s => s.Id == id)
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
            .FirstOrDefaultAsync();

        if (session == null)
        {
            return NotFound();
        }   

        return Ok(session);
    }

    [HttpPost]
    public async Task<ActionResult<TrainingSessionResponse>> Create([FromBody] CreateTrainingSessionRequest request)
    {
        var dog = await _context.Dogs.FindAsync(request.DogId);
        if (dog == null)
        {
            return BadRequest($"Dog with ID {request.DogId} not found");
        }

        var exercise = await _context.Exercises.FindAsync(request.ExerciseId);
        if (exercise == null || exercise.IsArchived)
        {
            return BadRequest($"Exercise with ID {request.ExerciseId} not found");
        }

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

        var response = new TrainingSessionResponse(
            session.Id,
            session.Date,
            session.Repetitions,
            session.Reward,
            session.Rating,
            session.Notes,
            session.DogId,
            session.Dog.Name,
            session.ExerciseId,
            session.Exercise.Name
        );

        return CreatedAtAction(nameof(GetById), new { id = session.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TrainingSessionResponse>> Update(int id, UpdateTrainingSessionRequest request)
    {
        var session = await _context.TrainingSessions.FindAsync(id);
        if (session == null)
        {
            return NotFound();
        }

        var dog = await _context.Dogs.FindAsync(request.DogId);
        if (dog == null)
        {
            return BadRequest($"Dog with ID {request.DogId} not found");
        }

        var exercise = await _context.Exercises.FindAsync(request.ExerciseId);
        if (exercise == null)
        {
            return BadRequest($"Exercise with ID {request.ExerciseId} not found");
        }

        var isChangingExercise = exercise.Id != session.ExerciseId;
        if (isChangingExercise && exercise.IsArchived)
        {
            return BadRequest($"Cannot change to archived exercise with ID {request.ExerciseId}");
        }

        session.Date = request.Date;
        session.Repetitions = request.Repetitions;
        session.Reward = request.Reward;
        session.Rating = request.Rating;
        session.Notes = request.Notes;
        session.Dog = dog;
        session.Exercise = exercise;

        await _context.SaveChangesAsync();

        var response = new TrainingSessionResponse(
            session.Id,
            session.Date,
            session.Repetitions,
            session.Reward,
            session.Rating,
            session.Notes,
            session.DogId,
            session.Dog.Name,
            session.ExerciseId,
            session.Exercise.Name
        );

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var session = await _context.TrainingSessions.FindAsync(id);
        if (session == null)
        {
            return NotFound();
        }
        _context.TrainingSessions.Remove(session);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
