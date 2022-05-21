using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen e-posta adresinizi girin.")
               .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi girin.");

            RuleFor(x => x.NewPassword).Equal(x => x.ConfirmPassword).WithMessage("Girmiş olduğunuz şifreler uyuşmamaktadır.");

            RuleFor(x => x.NewPassword)
                 .NotEmpty().WithMessage("Lütfen şifrenizi girin.")
                 .MinimumLength(8).WithMessage("Şifrenizin bir büyük harf ve bir rakam içermesi ve en az 8 karakter olması gerekmektedir.")
                 .Matches(@"^(?=.*\d)(?=.*[A-Z]).{8,50}$").WithMessage("Şifrenizin bir büyük harf ve bir rakam içermesi ve en az 8 karakter olması gerekmektedir.")
                 .MaximumLength(50);

            RuleFor(x => x.ConfirmPassword)
                 .NotEmpty().WithMessage("Lütfen şifrenizi girin.")
                 .MinimumLength(8).WithMessage("Şifrenizin bir büyük harf ve bir rakam içermesi ve en az 8 karakter olması gerekmektedir.")
                 .Matches(@"^(?=.*\d)(?=.*[A-Z]).{8,50}$").WithMessage("Şifrenizin bir büyük harf ve bir rakam içermesi ve en az 8 karakter olması gerekmektedir.")
                 .MaximumLength(50);

            RuleFor(x => x.Token).NotEmpty().MinimumLength(100);
        }
    }
}
