using minimalAPI.Contracts.Requests;
using FluentValidation;
using FastEndpoints;

namespace minimalAPI.Validation;

public class UniverseValidator : Validator<UniverseReq>
{
    public UniverseValidator()
    {
        RuleFor(x => x.universe).NotEmpty().WithMessage("Universe name cannot be empty");
    }
}

