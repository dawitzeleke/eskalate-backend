using FluentValidation;

public class GetApplicationsForJobQueryValidator : AbstractValidator<GetApplicationsForJobQuery>
{
    public GetApplicationsForJobQueryValidator()
    {
        RuleFor(x => x.JobId)
            .NotEmpty().WithMessage("JobId is required.");
    }
}
