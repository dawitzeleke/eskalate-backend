using MediatR;
using Microsoft.AspNetCore.Http;

using System.Threading;
using System.Threading.Tasks;

public class ApplyForJobCommand : IRequest<BaseResponse<ApplicationListItemDto>>
{
    public string JobId { get; set; }
    public string CoverLetter { get; set; }
    public IFormFile Resume { get; set; }
    public string ApplicantId { get; set; }
}
