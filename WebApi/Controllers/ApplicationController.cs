using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;

[ApiController]
[Route("api/applications")]
public class ApplicationController : ControllerBase
{
    private readonly IMediator _mediator;
    public ApplicationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("apply")]
    [Authorize(Roles = "Applicant")]
    public async Task<IActionResult> ApplyForJob([FromForm] ApplyForJobRequest request)
    {
        var applicantId = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type.EndsWith("nameidentifier"))?.Value;
        if (string.IsNullOrEmpty(applicantId))
            return Unauthorized();
        var cmd = new ApplyForJobCommand
        {
            JobId = request.JobId,
            CoverLetter = request.CoverLetter,
            Resume = request.Resume,
            ApplicantId = applicantId
        };
        var result = await _mediator.Send(cmd);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("my-applications")]
    [Authorize(Roles = "Applicant")]
    public async Task<IActionResult> GetMyApplications([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var applicantId = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type.EndsWith("nameidentifier"))?.Value;
        if (string.IsNullOrEmpty(applicantId))
            return Unauthorized();
        var query = new GetMyApplicationsQuery
        {
            ApplicantId = applicantId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("job/{jobId}/applications")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> GetApplicationsForJob(string jobId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var companyId = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type.EndsWith("nameidentifier"))?.Value;
        if (string.IsNullOrEmpty(companyId))
            return Unauthorized();
        var query = new GetApplicationsForJobQuery
        {
            JobId = jobId,
            CompanyId = companyId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        if (!result.Success)
            return Forbid(result.Message);
        return Ok(result);
    }

    [HttpPost("update-status")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> UpdateApplicationStatus([FromBody] ApplicationStatusUpdateRequest request)
    {
        var companyId = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type.EndsWith("nameidentifier"))?.Value;
        if (string.IsNullOrEmpty(companyId))
            return Unauthorized();
        var cmd = new UpdateApplicationStatusCommand
        {
            ApplicationId = request.ApplicationId,
            NewStatus = request.NewStatus,
            CompanyId = companyId
        };
        var result = await _mediator.Send(cmd);
        if (!result.Success)
        {
            if (result.Message == "Unauthorized")
                return Forbid(result.Message);
            return BadRequest(result);
        }
        return Ok(result);
    }
} 