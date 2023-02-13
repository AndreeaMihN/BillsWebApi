using FluentValidation;

namespace Bill.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(model => model.createClientDto.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Please ensure you have entered 'FirstName' field")
                .NotEmpty().WithMessage("Content field shouldn't be empty");

            RuleFor(model => model.createClientDto.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Please ensure you have entered 'LastName' field")
                .NotEmpty().WithMessage("Content field shouldn't be empty");

            RuleFor(model => model.createClientDto.PersonalIdentificationNumber)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Please ensure you have entered 'PersonalIdentificationNumber' field")
                .NotEmpty().WithMessage("Content field shouldn't be empty");
        }
    }
}