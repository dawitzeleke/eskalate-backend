using MediatR;

public class DeleteJobCommand : IRequest<DeleteJobResponse>
{
    public string JobId { get; set; }
}
