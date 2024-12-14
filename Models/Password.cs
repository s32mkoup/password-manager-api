namespace PasswordManager.Models;

public class Password
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string App { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string EncryptedPassword { get; set; } = string.Empty;
}