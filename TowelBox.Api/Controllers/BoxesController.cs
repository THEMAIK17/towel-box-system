using Microsoft.AspNetCore.Mvc;
using TowelBox.Application.Interfaces;
using TowelBox.Domain.Entities;

namespace TowelBox.Api.Controllers;

[ApiController]
[Route("api/boxes")]
public class BoxesController : ControllerBase
{
    private readonly IBoxService _boxService;

    public BoxesController(IBoxService boxService)
    {
        _boxService = boxService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var boxes = await _boxService.GetAllAsync();
        return Ok(boxes);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Box box)
    {
        try
        {
            var result = await _boxService.CreateAsync(box);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/disable")]
    public async Task<IActionResult> Disable(Guid id)
    {
        try
        {
            await _boxService.DisableAsync(id);
            return Ok("Box deshabilitado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{boxId}/pack")]
    public async Task<IActionResult> Pack(Guid boxId, [FromQuery] Guid towelId)
    {
        try
        {
            await _boxService.PackAsync(boxId, towelId);
            return Ok("Item empacado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{boxId}/unpack")]
    public async Task<IActionResult> Unpack(Guid boxId, [FromQuery] Guid towelId)
    {
        try
        {
            await _boxService.UnpackAsync(boxId, towelId);
            return Ok("Item sacado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{boxId}/close")]
    public async Task<IActionResult> Close(Guid boxId)
    {
        try
        {
            await _boxService.CloseAsync(boxId);
            return Ok("Caja cerrada");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}