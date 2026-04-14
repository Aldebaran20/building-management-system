using FluentValidation;
using BMS.Application.DTOs.WorkOrders;

namespace BMS.Application.Validators;

public class WorkOrderValidator : AbstractValidator<SaveWorkOrderDTO>
{
    public WorkOrderValidator()
    {
        RuleFor(wo => wo.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 50).WithMessage("Title must be between 3 and 50 characters.");

        RuleFor(wo => wo.Description)
            .Length(3, 200).WithMessage("Description must be between 3 and 200 characters.");

        RuleFor(wo => wo.Priority)
            .IsInEnum().WithMessage("Invalid priority.");

        RuleFor(wo => wo.Deadline)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Deadline cannot be in the past.");

        RuleFor(wo => wo.BuildingId)
            .GreaterThan(0).WithMessage("Building is required.");

        RuleFor(wo => wo.ContractorId)
            .GreaterThan(0).WithMessage("Contractor is required.");
    }
}