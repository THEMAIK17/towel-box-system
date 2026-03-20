using TowelBox.Application.Interfaces;
using TowelBox.Domain.Entities;

namespace TowelBox.Infrastructure.Services;

public class BoxService : IBoxService
    {
        private readonly IBoxRepository _boxRepository;
        private readonly ITowelRepository _towelRepository;

        public BoxService(IBoxRepository boxRepository, ITowelRepository towelRepository)
        {
            _boxRepository = boxRepository;
            _towelRepository = towelRepository;
        }

        public async Task<List<Box>> GetAllAsync()
        {
            return await _boxRepository.GetAllAsync();
        }

        public async Task<Box> CreateAsync(Box box)
        {
            if (string.IsNullOrWhiteSpace(box.BoxCode))
                throw new Exception("BoxCode es requerido");

            if (string.IsNullOrWhiteSpace(box.ProductCode))
                throw new Exception("ProductCode es requerido");

            if (box.Capacity <= 0)
                throw new Exception("Capacity debe ser mayor a 0");

            var exists = await _boxRepository.ExistsByBoxCodeAsync(box.BoxCode);

            if (exists)
                throw new Exception("El BoxCode ya existe");

            box.Status = "OPEN";
            box.IsActive = true;

            await _boxRepository.AddAsync(box);

            return box;
        }

        public async Task DisableAsync(Guid boxId)
        {
            var box = await _boxRepository.GetByIdAsync(boxId);

            if (box == null)
                throw new Exception("Box no encontrado");

            var count = await _boxRepository.GetCurrentCountAsync(boxId);

            if (count > 0)
                throw new Exception("No se puede deshabilitar una caja con items");

            box.IsActive = false;

            await _boxRepository.UpdateAsync(box);
        }

        public async Task PackAsync(Guid boxId, Guid towelId)
        {
            var box = await _boxRepository.GetByIdAsync(boxId);
            if (box == null)
                throw new Exception("Caja no encontrada");

            if (box.Status != "OPEN")
                throw new Exception("La caja está cerrada");

            var towel = await _towelRepository.GetByIdAsync(towelId);
            if (towel == null)
                throw new Exception("Item no encontrado");

            if (towel.Status != "LOOSE")
                throw new Exception("El item ya está empacado");

            if (towel.ProductCode != box.ProductCode)
                throw new Exception("El producto no coincide con la caja");

            var count = await _boxRepository.GetCurrentCountAsync(boxId);

            if (count >= box.Capacity)
                throw new Exception("Capacidad completa");

            towel.Status = "PACKED";
            towel.BoxId = boxId;

            await _towelRepository.UpdateAsync(towel);
        }

        public async Task UnpackAsync(Guid boxId, Guid towelId)
        {
            var box = await _boxRepository.GetByIdAsync(boxId);
            if (box == null)
                throw new Exception("Caja no encontrada");

            if (box.Status != "OPEN")
                throw new Exception("La caja está cerrada");

            var towel = await _towelRepository.GetByIdAsync(towelId);
            if (towel == null)
                throw new Exception("Item no encontrado");

            if (towel.BoxId != boxId)
                throw new Exception("El item no pertenece a esta caja");

            towel.Status = "LOOSE";
            towel.BoxId = null;

            await _towelRepository.UpdateAsync(towel);
        }

        public async Task CloseAsync(Guid boxId)
        {
            var box = await _boxRepository.GetByIdAsync(boxId);

            if (box == null)
                throw new Exception("Caja no encontrada");

            if (box.Status != "OPEN")
                throw new Exception("La caja ya está cerrada");

            box.Status = "CLOSED";

            await _boxRepository.UpdateAsync(box);
        }
    }