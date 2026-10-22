using Microsoft.EntityFrameworkCore;
using PawLog.Data;
using PawLog.Models;
using PawLog.Services.Interfaces;
using PawLog.Services.Requests.Dogs;
using PawLog.Services.Responses.Dogs;

namespace PawLog.Services.Core;

public class DogService : IDogService
{
    private readonly PawLogDbContext _context;
    public DogService(PawLogDbContext context) => _context = context;

    public async Task<List<DogResponse>> GetAllAsync(bool includeArchived)
    {
        var query = _context.Dogs.AsQueryable();
        
        return await query 
            .Select(d => new DogResponse(d.Id, d.Name, d.Breed, d.DateOfBirth))
            .ToListAsync();
    }

    public async Task<DogResponse?> GetByIdAsync(int id)
    {
        var dog = await _context.Dogs.FindAsync(id);
        return dog is null ? null : ToResponse(dog);
    }

    public async Task<DogResponse> CreateAsync(CreateDogRequest request)
    {
        var dog = new Dog{ Name = request.Name, Breed = request.Breed, DateOfBirth = request.DateOfBirth };

        _context.Dogs.Add(dog);
        await _context.SaveChangesAsync();
        return ToResponse(dog);
    }

    public async Task<DogResponse?> UpdateAsync(int id, UpdateDogRequest request)
    {
        var dog = await _context.Dogs.FindAsync(id);
        if (dog is null) return null;

        dog.Name = request.Name;
        dog.Breed = request.Breed;
        dog.DateOfBirth = request.DateOfBirth;

        _context.Dogs.Update(dog);
        await _context.SaveChangesAsync();
        return ToResponse(dog);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dog = await _context.Dogs.FindAsync(id);
        if (dog is null) return false;

        _context.Dogs.Remove(dog);
        await _context.SaveChangesAsync();
        return true;
    }

    private static DogResponse ToResponse(Dog dog) =>
        new(dog.Id, dog.Name, dog.Breed, dog.DateOfBirth);
}
