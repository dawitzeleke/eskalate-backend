using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class GetMyApplicationsQuery : IRequest<PaginatedResponse<ApplicationListItemDto>>
{
    public string ApplicantId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
