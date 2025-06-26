using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;

public class GetApplicationsForJobQuery : IRequest<PaginatedResponse<ApplicationForJobDto>>
{
    public string JobId { get; set; }
    public string CompanyId { get; set; } 
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
