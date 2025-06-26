
using MediatR;

public class SignUpCommand : IRequest<SignUpResponse>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public RoleEnum Role { get; set; } 
}