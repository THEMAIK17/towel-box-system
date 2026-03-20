using Microsoft.EntityFrameworkCore;
using TowelBox.Application.Interfaces;
using TowelBox.Domain.Entities;
using TowelBox.Infrastructure.Data;

namespace TowelBox.Infrastructure.Repositories;

public class TowelRepository : ITowelRepository
{
    private readonly AppDbContext _context;

    public TowelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Towel>> GetAllAsync()
    {
        return await _context.Towels
            .Where(t => t.IsActive)
            .ToListAsync();
    }

    public async Task<Towel?> GetByIdAsync(Guid id)
    {
        return await _context.Towels
            .FirstOrDefaultAsync(t => t.Id == id && t.IsActive);
    }

    public async Task<Towel?> GetByItemCodeAsync(string itemCode)
    {
        return await _context.Towels
            .FirstOrDefaultAsync(t => t.ItemCode == itemCode && t.IsActive);
    }

    public async Task<bool> ExistsByItemCodeAsync(string itemCode)
    {
        return await _context.Towels
            .AnyAsync(t => t.ItemCode == itemCode);
    }

    public async Task AddAsync(Towel towel)
    {
        await _context.Towels.AddAsync(towel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Towel towel)
    {
        _context.Towels.Update(towel);
        await _context.SaveChangesAsync();
    }
}