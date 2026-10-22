using Microsoft.AspNetCore.Mvc;
using PawLog.Services.Interfaces;
using PawLog.Services.Requests.Dogs;
using PawLog.Services.Responses.Dogs;

namespace PawLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DogsController: ControllerBase
{
    private readonly IDogService _dogService;

    public DogsController(IDogService dogService) => _dogService = dogService;

    [HttpGet]
    public async Task<ActionResult<List<DogResponse>>> GetAll()
    {
        return await _dogService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DogResponse>> GetById(int id)
    {
        var dog = await _dogService.GetByIdAsync(id);
        return dog is null ? NotFound(): Ok(dog);
    }

    [HttpPost]
    public async Task<ActionResult<DogResponse>> Create(CreateDogRequest request)
    {
        var created = await _dogService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new {id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DogResponse>> Update(int id, UpdateDogRequest request)
    {
        var updated = await _dogService.UpdateAsync(id, request);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _dogService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
