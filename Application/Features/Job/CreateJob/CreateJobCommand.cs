using MediatR;

public class CreateJobCommand : IRequest<CreateJobResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string CreatedBy { get; set; }
}