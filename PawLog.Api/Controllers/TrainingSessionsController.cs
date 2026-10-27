using Microsoft.AspNetCore.Mvc;
using PawLog.Services.Interfaces;
using PawLog.Services.Results;
using PawLog.Services.Requests.TrainingSessions;
using PawLog.Services.Responses.TrainingSessions;

namespace PawLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingSessionsController: ControllerBase
{
    private readonly ITrainingSessionService _trainingSessionService;
    public TrainingSessionsController(ITrainingSessionService trainingSessionService) => _trainingSessionService = trainingSessionService;

    [HttpGet]
    public async Task<ActionResult<TrainingSessionResponse>> GetAll([FromQuery] int? dogId)
    {
        return Ok(await _trainingSessionService.GetAllAsync(dogId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingSessionResponse>> GetById(int id)
    {
        var session = await _trainingSessionService.GetByIdAsync(id);
        return session is null ? NotFound() : Ok(session);
    }

    [HttpPost]
    public async Task<ActionResult<TrainingSessionResponse>> Create([FromBody] CreateTrainingSessionRequest request)
    {
        var created = await _trainingSessionService.CreateAsync(request);
        if (!created.IsSuccess) return BadRequest(created.ErrorMessage);

        return CreatedAtAction(nameof(GetById), new { id = created.Value!.Id }, created.Value);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TrainingSessionResponse>> Update(int id, UpdateTrainingSessionRequest request)
    {
        var updated = await _trainingSessionService.UpdateAsync(id, request);
        return updated.Error switch
        {
            ServiceError.None => Ok(updated.Value),
            ServiceError.NotFound => NotFound(updated.ErrorMessage),
            _ => BadRequest(updated.ErrorMessage)
        };
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _trainingSessionService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();

    }
}
