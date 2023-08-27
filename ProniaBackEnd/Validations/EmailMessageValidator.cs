using FluentValidation;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services.Common;
using ProniaBackEnd.ViewModels.admin.emailMesagges;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            foreach (var email in emailArray)
            {
                if (!string.IsNullOrWhiteSpace(email) && !emailRegex.IsMatch(email))
                {
                    return false;
                }
            }

            return true;
        }

        

    }
}

