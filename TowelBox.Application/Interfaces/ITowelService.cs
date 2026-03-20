using TowelBox.Domain.Entities;

namespace TowelBox.Application.Interfaces;

public interface ITowelService
{
    Task<List<Towel>> GetAllAsync();

    Task<Towel> CreateAsync(Towel towel);

    Task DisableAsync(Guid towelId);
}