using TowelBox.Domain.Entities;

namespace TowelBox.Application.Interfaces;

public interface IBoxRepository
{
    Task<List<Box>> GetAllAsync();

    Task<Box?> GetByIdAsync(Guid id);

    Task<Box?> GetByBoxCodeAsync(string boxCode);
    Task<bool> ExistsByBoxCodeAsync(string boxCode);

    Task AddAsync(Box box);

    Task UpdateAsync(Box box);

    Task<int> GetCurrentCountAsync(Guid boxId);
}