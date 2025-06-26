using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator<SignInCommand> _validator;
    public SignInCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IValidator<SignInCommand> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
    }
    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new SignInResponse
            {
                Success = false,
                Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            };
        }

        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) != PasswordVerificationResult.Success)
        {
            return new SignInResponse
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Role.ToString());
        return new SignInResponse
        {
            Success = true,
            Token = token,
        };
    }
}