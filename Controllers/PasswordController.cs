using Microsoft.AspNetCore.Mvc;
using PasswordManager.Services;
using PasswordManager.DTOs;

namespace PasswordManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PasswordController : ControllerBase
{
    private readonly IPasswordService _passwordService;

    public PasswordController(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PasswordDto>>> GetAllPasswords()
    {
        return await _passwordService.GetAllPasswordsAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PasswordDto>> GetPassword(int id)
    {
        var password = await _passwordService.GetPasswordByIdAsync(id);
        if (password == null)
            return NotFound();

        return password;
    }

    [HttpGet("{id}/decrypt")]
    public async Task<ActionResult<string>> GetDecryptedPassword(int id)
    {
        var password = await _passwordService.GetDecryptedPasswordAsync(id);
        if (password == null)
            return NotFound();

        return password;
    }

    [HttpPost]
    public async Task<ActionResult<PasswordDto>> CreatePassword(CreatePasswordDto passwordDto)
    {
        var created = await _passwordService.CreatePasswordAsync(passwordDto);
        return CreatedAtAction(nameof(GetPassword), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PasswordDto>> UpdatePassword(int id, UpdatePasswordDto passwordDto)
    {
        if (id != passwordDto.Id)
            return BadRequest();

        var updated = await _passwordService.UpdatePasswordAsync(passwordDto);
        if (updated == null)
            return NotFound();

        return updated;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePassword(int id)
    {
        var result = await _passwordService.DeletePasswordAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}