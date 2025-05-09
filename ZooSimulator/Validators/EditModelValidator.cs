using FluentValidation;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Validators
{
    public class EditModelValidator : AbstractValidator<EditModel>
    {
        public EditModelValidator()
        {
            Include(new FieldsModelValidator());
        }
    }
}