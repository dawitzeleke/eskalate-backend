using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class UpdateApplicationStatusCommand : IRequest<BaseResponse<ApplicationForJobDto>>
{
    public string ApplicationId { get; set; }
    public string NewStatus { get; set; }
    public string CompanyId { get; set; } 
}
