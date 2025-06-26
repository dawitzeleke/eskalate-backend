using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class GetJobDetailsQuery : IRequest<BaseResponse<JobDetailsDto>>
{
    public string JobId { get; set; }
}
