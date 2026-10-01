using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawLog.Api.Requests.Dogs;
using PawLog.Api.Responses.Dogs;
using PawLog.Data;
using PawLog.Models;

namespace PawLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DogsController: ControllerBase
{
    private readonly PawLogDbContext _context;

    public DogsController(PawLogDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<List<DogResponse>>> GetAll()
    {
        var dogs = await _context.Dogs
            .Select(d => new DogResponse(d.Id, d.Name, d.Breed, d.DateOfBirth))
            .ToListAsync();
        return Ok(dogs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DogResponse>> GetById(int id)
    {
        var dog = await _context.Dogs.FindAsync(id);
        if (dog == null)
        {
            return NotFound();
        }
        var dogResponse = new DogResponse(dog.Id, dog.Name, dog.Breed, dog.DateOfBirth);
        return Ok(dogResponse);
    }

    [HttpPost]
    public async Task<ActionResult<DogResponse>> Create(CreateDogRequest request)
    {
        var dog = new Dog
        {
            Name = request.Name,
            Breed = request.Breed,
            DateOfBirth = request.DateOfBirth
        };

        _context.Dogs.Add(dog);
        await _context.SaveChangesAsync();

        var dogResponse = new DogResponse(dog.Id, dog.Name, dog.Breed, dog.DateOfBirth);
        return CreatedAtAction(nameof(GetById), new { id = dog.Id }, dogResponse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DogResponse>> Update(int id, UpdateDogRequest request)
    {
        var dog = await _context.Dogs.FindAsync(id);
        if (dog == null)
        {
            return NotFound();
        }

        dog.Name = request.Name;
        dog.Breed = request.Breed;
        dog.DateOfBirth = request.DateOfBirth;

        await _context.SaveChangesAsync();
        var updatedDog = new DogResponse(dog.Id, dog.Name, dog.Breed, dog.DateOfBirth);
        return Ok(updatedDog);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var dog = await _context.Dogs.FindAsync(id);
        if (dog == null)
        {
            return NotFound();
        }

        _context.Dogs.Remove(dog);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
