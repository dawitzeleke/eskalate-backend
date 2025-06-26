using MediatR;
using Microsoft.AspNetCore.Identity;
using FluentValidation;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IValidator<SignUpCommand> _validator;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public SignUpCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IValidator<SignUpCommand> validator,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _validator = validator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new SignUpResponse
            {
                Success = false,
                Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            };
        }

        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return new SignUpResponse
            {
                Success = false,
                Message = "Email already exists."
            };
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Role = request.Role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var hashedPassword = _passwordHasher.HashPassword(user, request.Password);

        await _userRepository.CreateAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Role.ToString());
        return new SignUpResponse
        {
            Success = true,
            Message = "User registered successfully.",
            Token = token,
        };
    }
}