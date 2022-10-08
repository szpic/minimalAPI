using minimalAPI.Contracts.Requests;
using FluentValidation;
using FastEndpoints;

namespace minimalAPI.Validation;

public class PlayerValidator : Validator<PlayerReq>
{
    public PlayerValidator()
    {
        RuleFor(x => x.name).NotEmpty().WithMessage("Player name cannot be empty");
    }
}

