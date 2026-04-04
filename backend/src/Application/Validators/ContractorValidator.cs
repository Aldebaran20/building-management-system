using FluentValidation;
using BMS.Application.DTOs.Contractors;

namespace BMS.Application.Validators;

public class ContractorValidator : AbstractValidator<SaveContractorDTO>
{
    public ContractorValidator()
    {
        RuleFor(c => c.BusinessName)
            .NotEmpty().When(c => string.IsNullOrEmpty(c.ContactName))
                .WithMessage("Either business name or contact name is required.")
            .Length(3, 50).WithMessage("Business name must be between 3 and 50 characters.");

        RuleFor(c => c.ContactName)
            .NotEmpty().When(c => string.IsNullOrEmpty(c.BusinessName))
                .WithMessage("Either business name or contact name is required.")
            .Length(3, 50).WithMessage("Contact name must be between 3 and 50 characters.");

        RuleFor(c => c.ContactEmail)
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(c => c.ContactPhone)
            .NotEmpty().WithMessage("Contact phone is required.")
            .Length(5, 15).WithMessage("Contact phone must be between 5 and 15 characters.");

        RuleFor(c => c.AreaOfOperations)
            .Length(3, 50).WithMessage("Area of operations must be between 3 and 50 characters.");

        RuleFor(c => c.ContractorType)
            .NotEmpty().WithMessage("Contractor type is required.")
            .IsInEnum().WithMessage("Invalid contractor type.");

        RuleFor(c => c.ContractorStatus)
            .NotEmpty().WithMessage("Contractor status is required.")
            .IsInEnum().WithMessage("Invalid contractor status.");
    }
}