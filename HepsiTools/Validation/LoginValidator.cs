using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen e-posta adresinizi girin.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi girin.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lütfen şifrenizi girin.")
                .MinimumLength(8).WithMessage("Şifrenizin bir büyük harf ve bir rakam içermesi ve en az 8 karakter olması gerekmektedir.")
                .Matches(@"^(?=.*\d)(?=.*[A-Z]).{8,50}$").WithMessage("Şifrenizin bir büyük harf ve bir rakam içermesi ve en az 8 karakter olması gerekmektedir.")
                .MaximumLength(50);
        }
    }
}
