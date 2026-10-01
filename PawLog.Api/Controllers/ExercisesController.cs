using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawLog.Api.Requests.Exercises;
using PawLog.Api.Responses.Exercises;
using PawLog.Data;
using PawLog.Models;

namespace PawLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController: ControllerBase
{
    private readonly PawLogDbContext _context;

    public ExercisesController(PawLogDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<List<ExerciseResponse>>> GetAll()
    {
        var exercises = await _context.Exercises
            .Select(e => new ExerciseResponse(e.Id, e.Name, e.Description))
            .ToListAsync();
        return Ok(exercises);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseResponse>> GetById(int id)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        return Ok(new ExerciseResponse(exercise.Id, exercise.Name, exercise.Description));
    }

    [HttpPost]
    public async Task<ActionResult<ExerciseResponse>> Create(CreateExerciseRequest request)
    {
        var exercise = new Exercise
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();

        var exerciseResponse = new ExerciseResponse(exercise.Id, exercise.Name, exercise.Description);
        return CreatedAtAction(nameof(GetById), new { id = exercise.Id }, exerciseResponse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExerciseResponse>> Update(int id, UpdateExerciseRequest request)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        exercise.Name = request.Name;
        exercise.Description = request.Description;

        await _context.SaveChangesAsync();
        var updatedExerciseResponse = new ExerciseResponse(exercise.Id, exercise.Name, exercise.Description);
        return Ok(updatedExerciseResponse);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}   
