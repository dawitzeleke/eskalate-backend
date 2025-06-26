using AutoMapper;
using MediatR;

public class BrowseJobsQueryHandler : IRequestHandler<BrowseJobsQuery, PaginatedResponse<JobDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IMapper _mapper;

    public BrowseJobsQueryHandler(IJobRepository jobRepository, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<JobDto>> Handle(BrowseJobsQuery request, CancellationToken cancellationToken)
    {
        var filter = new JobFilter
        {
            Title = request.Title,
            Location = request.Location,
            CompanyId = request.CompanyId
        };

        var (jobs, totalCount) = await _jobRepository.BrowseJobsAsync(filter, request.PageNumber, request.PageSize);

        var jobDtos = _mapper.Map<List<JobDto>>(jobs);
        return PaginatedResponse<JobDto>.CreateSuccess(jobDtos, request.PageNumber, request.PageSize, totalCount);
    }
}