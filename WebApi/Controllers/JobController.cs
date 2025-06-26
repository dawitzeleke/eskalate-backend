using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Dtos;
using System.Threading.Tasks;
using Application.Features.Job;
using Application.Response;
using MediatR;
using System.Linq;

[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    // Inject repositories, services, etc. (pseudo-code, to be implemented)
    // private readonly JobRepository _jobRepo;
    // private readonly ApplicationRepository _applicationRepo;
    // private readonly UserRepository _userRepo;

    private readonly IMediator _mediator;
    public JobController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("my-posted-jobs")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> GetMyPostedJobs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var companyId = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type.EndsWith("nameidentifier"))?.Value;
        if (string.IsNullOrEmpty(companyId))
            return Unauthorized();
        var query = new GetMyPostedJobsQuery
        {
            CompanyId = companyId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{jobId}")]
    [Authorize]
    public async Task<IActionResult> GetJobDetails(string jobId)
    {
        var query = new GetJobDetailsQuery { JobId = jobId };
        var result = await _mediator.Send(query);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }
} 