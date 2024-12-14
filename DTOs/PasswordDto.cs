namespace PasswordManager.DTOs
{
    public class PasswordDto
    {
        public int Id { get; set; }

        public string Category { get; set; } = string.Empty;
        public string App { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }

    public class CreatePasswordDto
    {
        public string Category { get; set; } = string.Empty;
        public string App { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UpdatePasswordDto: CreatePasswordDto 
    {
            public int Id { get; set; }

    }
}