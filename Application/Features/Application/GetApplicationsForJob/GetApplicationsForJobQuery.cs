using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class GetApplicationsForJobQuery : IRequest<PaginatedResponse<ApplicationForJobDto>>
{
    public string JobId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
