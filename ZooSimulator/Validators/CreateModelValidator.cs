using FluentValidation;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Validators
{
    public class CreateModelValidator : AbstractValidator<CreateModel>
    {
        public CreateModelValidator()
        {
            Include(new FieldsModelValidator());
        }
    }
}