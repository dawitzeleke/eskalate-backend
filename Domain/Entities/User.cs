using backend.Domain.Common;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public RoleEnum Role { get; set; } 

}