namespace LuckyRest.Database.DTOs.Models;

public class AuthUserDto
{
    public string? Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
}