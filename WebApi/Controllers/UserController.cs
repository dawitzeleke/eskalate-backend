

using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromForm] SignUpCommand command)
    {
        var response = await _mediator.Send(command);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
    {
        var response = await _mediator.Send(command);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}