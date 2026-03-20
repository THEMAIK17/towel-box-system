using TowelBox.Domain.Entities;

namespace TowelBox.Application.Interfaces;

public interface ITowelRepository
{
    Task<List<Towel>> GetAllAsync();

    Task<Towel?> GetByIdAsync(Guid id);

    Task<Towel?> GetByItemCodeAsync(string itemCode);

    Task<bool> ExistsByItemCodeAsync(string itemCode);

    Task AddAsync(Towel towel);

    Task UpdateAsync(Towel towel);
}