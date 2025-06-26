using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class GetMyPostedJobsQuery : IRequest<PaginatedResponse<JobListItemDto>>
{
    public string CompanyId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
