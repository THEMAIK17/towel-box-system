using TowelBox.Domain.Entities;

namespace TowelBox.Application.Interfaces;

public interface IBoxService
{
    Task<List<Box>> GetAllAsync();

    Task<Box> CreateAsync(Box box);

    Task DisableAsync(Guid boxId);

    Task PackAsync(Guid boxId, Guid towelId);

    Task UnpackAsync(Guid boxId, Guid towelId);

    Task CloseAsync(Guid boxId);
}