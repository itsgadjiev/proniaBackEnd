using FluentValidation;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services.Common;
using ProniaBackEnd.ViewModels.admin.emailMesagges;
using System.ComponentModel.DataAnnotations;

namespace ProniaBackEnd.Validations
{
    public class EmailMessageValidator : AbstractValidator<EmailMessageAddViewModel>
    {
        public EmailMessageValidator()
        {
            RuleFor(em => em.Title).NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(em => em.Title).MinimumLength(5);
            RuleFor(em => em.Title).MaximumLength(50);

            RuleFor(em => em.Content).NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(em => em.Content).MinimumLength(5);
            RuleFor(em => em.Content).MaximumLength(1000);

            RuleFor(em => em.Recievers)
                .NotEmpty()
                .Must(BeValidEmailList).WithMessage("Invalid email address .");
        }

        public bool BeValidEmailList(string emails)
        {
            var emailArray = RecieversEmailGetter.Handle(emails);

            foreach (var email in emailArray)
            {
                if (!string.IsNullOrWhiteSpace(email) && !IsValidEmail(email))
                {
                    return false;
                }
            }

            return true;
        }

        bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

    }
}

