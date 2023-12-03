namespace PasswordManager.Domain;

public class Password
{
    public int Id { get; set; }
    public string? Website { get; set; }
    public string? UserName { get; set; }
    public string? UserPassword { get; set; }
}