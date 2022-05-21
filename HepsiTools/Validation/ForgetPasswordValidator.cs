using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordModel>
    {
        public ForgetPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen e-posta adresinizi girin.")
               .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi girin.");
        }
    }
}
