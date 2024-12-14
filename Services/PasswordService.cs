using System.Runtime.Caching;
using System.Text;
using PasswordManager.Models;
using PasswordManager.DTOs;

namespace PasswordManager.Services;

public class PasswordService : IPasswordService
{
    private readonly MemoryCache _cache;
    private const string CACHE_KEY = "passwords";
    private int _nextId = 1;

    public PasswordService()
    {
        _cache = MemoryCache.Default;
        _cache.Add(CACHE_KEY, new List<Password>(), new CacheItemPolicy());
    }

    private List<Password> GetPasswordsFromCache()
    {
        return (List<Password>)_cache.Get(CACHE_KEY);
    }

    private void SavePasswordsToCache(List<Password> passwords)
    {
        _cache.Set(CACHE_KEY, passwords, new CacheItemPolicy());
    }

    public async Task<List<PasswordDto>> GetAllPasswordsAsync()
    {
        var passwords = GetPasswordsFromCache();
        return passwords.Select(p => new PasswordDto
        {
            Id = p.Id,
            Category = p.Category,
            App = p.App,
            UserName = p.UserName
        }).ToList();
    }

    public async Task<PasswordDto?> GetPasswordByIdAsync(int id)
    {
        var passwords = GetPasswordsFromCache();
        var password = passwords.FirstOrDefault(p => p.Id == id);
        
        if (password == null)
            return null;

        return new PasswordDto
        {
            Id = password.Id,
            Category = password.Category,
            App = password.App,
            UserName = password.UserName
        };
    }

    public async Task<string?> GetDecryptedPasswordAsync(int id)
    {
        var passwords = GetPasswordsFromCache();
        var password = passwords.FirstOrDefault(p => p.Id == id);
        
        if (password == null)
            return null;

        return DecryptPassword(password.EncryptedPassword);
    }

    public async Task<PasswordDto> CreatePasswordAsync(CreatePasswordDto passwordDto)
    {
        var passwords = GetPasswordsFromCache();
        
        var password = new Password
        {
            Id = _nextId++,
            Category = passwordDto.Category,
            App = passwordDto.App,
            UserName = passwordDto.UserName,
            EncryptedPassword = EncryptPassword(passwordDto.Password)
        };

        passwords.Add(password);
        SavePasswordsToCache(passwords);

        return new PasswordDto
        {
            Id = password.Id,
            Category = password.Category,
            App = password.App,
            UserName = password.UserName
        };
    }

    public async Task<PasswordDto?> UpdatePasswordAsync(UpdatePasswordDto passwordDto)
    {
        var passwords = GetPasswordsFromCache();
        var existingPassword = passwords.FirstOrDefault(p => p.Id == passwordDto.Id);
        
        if (existingPassword == null)
            return null;

        existingPassword.Category = passwordDto.Category;
        existingPassword.App = passwordDto.App;
        existingPassword.UserName = passwordDto.UserName;
        existingPassword.EncryptedPassword = EncryptPassword(passwordDto.Password);

        SavePasswordsToCache(passwords);

        return new PasswordDto
        {
            Id = existingPassword.Id,
            Category = existingPassword.Category,
            App = existingPassword.App,
            UserName = existingPassword.UserName
        };
    }

    public async Task<bool> DeletePasswordAsync(int id)
    {
        var passwords = GetPasswordsFromCache();
        var password = passwords.FirstOrDefault(p => p.Id == id);
        
        if (password == null)
            return false;

        passwords.Remove(password);
        SavePasswordsToCache(passwords);
        return true;
    }

    private string EncryptPassword(string password)
    {
        byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
        return Convert.ToBase64String(passwordBytes);
    }

    private string DecryptPassword(string encryptedPassword)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
        return Encoding.ASCII.GetString(encryptedBytes);
    }
}