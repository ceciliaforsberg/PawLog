using Microsoft.AspNetCore.Mvc;
using PawLog.Services.Interfaces;
using PawLog.Services.Requests.Exercises;
using PawLog.Services.Responses.Exercises;

namespace PawLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController: ControllerBase
{
    private readonly IExerciseService _exerciseService;
    public ExercisesController(IExerciseService exerciseService) => _exerciseService = exerciseService;

    [HttpGet]
    public async Task<ActionResult<List<ExerciseResponse>>> GetAll([FromQuery] bool includeArchived = false)
    {
        return Ok(await _exerciseService.GetAllAsync(includeArchived));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseResponse>> GetById(int id)
    {
        var exercise = await _exerciseService.GetByIdAsync(id);
        return exercise is null ? NotFound() : Ok(exercise);
    }

    [HttpPost]
    public async Task<ActionResult<ExerciseResponse>> Create(CreateExerciseRequest request)
    {
        var created = await _exerciseService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExerciseResponse>> Update(int id, UpdateExerciseRequest request)
    {
        var updated = await _exerciseService.UpdateAsync(id, request);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var archived = await _exerciseService.ArchiveAsync(id);
        return archived ? NoContent() : NotFound();
    }
}   
