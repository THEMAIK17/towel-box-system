using TowelBox.Application.Interfaces;
using TowelBox.Domain.Entities;

namespace TowelBox.Infrastructure.Services;

public class TowelService : ITowelService
{
    private readonly ITowelRepository _towelRepository;

    public TowelService(ITowelRepository towelRepository)
    {
        _towelRepository = towelRepository;
    }

    public async Task<List<Towel>> GetAllAsync()
    {
        return await _towelRepository.GetAllAsync();
    }

    public async Task<Towel> CreateAsync(Towel towel)
    {
        if (string.IsNullOrWhiteSpace(towel.ItemCode))
            throw new Exception("ItemCode es requerido");

        if (string.IsNullOrWhiteSpace(towel.ProductCode))
            throw new Exception("ProductCode es requerido");

        var exists = await _towelRepository.ExistsByItemCodeAsync(towel.ItemCode);

        if (exists)
            throw new Exception("El ItemCode ya existe");

        towel.Status = "LOOSE";
        towel.BoxId = null;
        towel.IsActive = true;

        await _towelRepository.AddAsync(towel);

        return towel;
    }

    public async Task DisableAsync(Guid towelId)
    {
        var towel = await _towelRepository.GetByIdAsync(towelId);

        if (towel == null)
            throw new Exception("Towel no encontrado");

        if (towel.Status == "PACKED")
            throw new Exception("No se puede deshabilitar un item empacado");

        towel.IsActive = false;

        await _towelRepository.UpdateAsync(towel);
    }
}