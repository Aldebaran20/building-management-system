using FluentValidation;
using BMS.Application.DTOs.Buildings;

namespace BMS.Application.Validators;

public class BuildingValidator : AbstractValidator<SaveBuildingDTO>
{
    public BuildingValidator()
    {
        RuleFor(b => b.BuildingName)
            .NotEmpty().WithMessage("Building name is required.")
            .Length(3, 50).WithMessage("Building name must be between 3 and 50 characters.");

        RuleFor(b => b.BuildingAddress)
            .NotEmpty().WithMessage("Building address is required.")
            .Length(10, 100).WithMessage("Building address must be between 10 and 100 characters.");

        RuleFor(b => b.NumberOfUnits)
            .InclusiveBetween(1, 10000).WithMessage("Number of units must be between 1 and 10,000.");

        RuleFor(b => b.BuildingType)
            .NotEmpty().WithMessage("Building type is required.")
            .IsInEnum().WithMessage("Invalid building type.");

        RuleFor(b => b.BuildingStatus)
            .NotEmpty().WithMessage("Building status is required.")
            .IsInEnum().WithMessage("Invalid building status.");
    }
}