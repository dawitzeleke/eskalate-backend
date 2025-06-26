using MediatR;

public class UpdateJobCommand : IRequest<UpdateJobResponse>
{
    public string JobId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string CompanyId { get; set; }
}
