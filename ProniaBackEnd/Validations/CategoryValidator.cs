using FluentValidation;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Validations
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(c => c.Name).MinimumLength(2);
            RuleFor(c => c.Name).MaximumLength(50);
        }
    }
}
