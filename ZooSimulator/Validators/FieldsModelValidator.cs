using FluentValidation;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Validators
{
    public class FieldsModelValidator : AbstractValidator<FieldsModel>
    {
        public FieldsModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Please enter the animal's name.")
                .MaximumLength(50)
                .WithMessage("Please enter a name less than or equal to 50 characters.");
            RuleFor(x => x.Age)
                .NotNull()
                .WithMessage("Please enter the animal's age.")
                .InclusiveBetween(0, 100)
                .WithMessage("Please enter an age between 0 and 100.");
            RuleFor(x => x.Gender)
                .NotNull()
                .WithMessage("Please enter the animal's gender.");
        }
    }
}