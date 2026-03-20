using Microsoft.EntityFrameworkCore;
using TowelBox.Application.Interfaces;
using TowelBox.Domain.Entities;
using TowelBox.Infrastructure.Data;

namespace TowelBox.Infrastructure.Repositories;

public class BoxRepository : IBoxRepository
{
    private readonly AppDbContext _context;

    public BoxRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Box>> GetAllAsync()
    {
        return await _context.Boxes
            .Where(b => b.IsActive)
            .ToListAsync();
    }

    public async Task<Box?> GetByIdAsync(Guid id)
    {
        return await _context.Boxes
            .FirstOrDefaultAsync(b => b.Id == id && b.IsActive);
    }

    public async Task<Box?> GetByBoxCodeAsync(string boxCode)
    {
        return await _context.Boxes
            .FirstOrDefaultAsync(b => b.BoxCode == boxCode && b.IsActive);
    }

    public async Task<bool> ExistsByBoxCodeAsync(string boxCode)
    {
        return await _context.Boxes
            .AnyAsync(b => b.BoxCode == boxCode);
    }

    public async Task AddAsync(Box box)
    {
        await _context.Boxes.AddAsync(box);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Box box)
    {
        _context.Boxes.Update(box);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetCurrentCountAsync(Guid boxId)
    {
        return await _context.Towels
            .CountAsync(t => t.BoxId == boxId && t.IsActive);
    }
}