using PasswordManager.Models;
using PasswordManager.DTOs;

namespace PasswordManager.Services;

public interface IPasswordService
{
    Task<List<PasswordDto>> GetAllPasswordsAsync();
    Task<PasswordDto?> GetPasswordByIdAsync(int id);
    Task<string?> GetDecryptedPasswordAsync(int id);
    Task<PasswordDto> CreatePasswordAsync(CreatePasswordDto passwordDto);
    Task<PasswordDto?> UpdatePasswordAsync(UpdatePasswordDto passwordDto);
    Task<bool> DeletePasswordAsync(int id);
}