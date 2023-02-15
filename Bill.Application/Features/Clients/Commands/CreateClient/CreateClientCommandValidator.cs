using Bill.Application.Common;
using FluentValidation;

namespace Bill.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientDto>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(model => model.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Please ensure you have entered 'FirstName' field")
                .NotEmpty().WithMessage("Content field shouldn't be empty")
                .MaximumLength(255);

            RuleFor(model => model.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Please ensure you have entered 'LastName' field")
                .NotEmpty().WithMessage("Content field shouldn't be empty");

            RuleFor(request => request.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Matches(Constants.EmailRegexValidation)
                .MaximumLength(255)
                .When(request => request.Email is not null);

            RuleFor(model => model.PersonalIdentificationNumber)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Please ensure you have entered 'PersonalIdentificationNumber' field")
                .NotEmpty().WithMessage("Content field shouldn't be empty")
                .MaximumLength(255);
        }
    }
}