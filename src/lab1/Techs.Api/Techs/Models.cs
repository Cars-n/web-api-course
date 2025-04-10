using FluentValidation;
using Techs.Api.Shared;

namespace Techs.Api.Techs;


public record TechCreateModel(string FirstName, string LastName, string Sub, string Email, string Phone);

public record TechResponseModel(Guid Id, string FirstName, string LastName, string Sub, string Email, string Phone);

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
        RuleFor(x => x.Email)
            .Matches(ValidationGenerators.ValidEmailRegularExpression())
            .WithMessage("Email looks wrong.");
        RuleFor(t => t.Sub)
            .Must(t => t.StartsWith('x') || t.StartsWith('a'))
            .WithMessage("Sub must start with an x or a")
            .When(t => string.IsNullOrEmpty(t.Sub) == false);
    }
}