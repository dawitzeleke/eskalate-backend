using MediatR;

public class SignInCommand : IRequest<SignInResponse>

{
    public string Email { get; set; }
    public string Password { get; set; }
}