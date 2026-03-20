using Microsoft.AspNetCore.Mvc;
using TowelBox.Application.Interfaces;
using TowelBox.Domain.Entities;

namespace TowelBox.Api.Controllers;

[ApiController]
[Route("api/towels")]
public class TowelsController : ControllerBase
{
    private readonly ITowelService _towelService;

    public TowelsController(ITowelService towelService)
    {
        _towelService = towelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var towels = await _towelService.GetAllAsync();
        return Ok(towels);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Towel towel)
    {
        try
        {
            var result = await _towelService.CreateAsync(towel);
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
            await _towelService.DisableAsync(id);
            return Ok("Towel deshabilitado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}