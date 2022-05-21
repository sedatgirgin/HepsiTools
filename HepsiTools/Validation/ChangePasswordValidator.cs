using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().MaximumLength(50);
            RuleFor(x => x.NewPassword)
             .NotEmpty().WithMessage("Lütfen şifrenizi girin.")
             .MinimumLength(8).WithMessage("Şifrenizin bir büyük harf ve bir rakam içermesi ve en az 8 karakter olması gerekmektedir.")
             .Matches(@"^(?=.*\d)(?=.*[A-Z]).{8,50}$").WithMessage("Şifrenizin bir büyük harf ve bir rakam içermesi ve en az 8 karakter olması gerekmektedir.")
             .MaximumLength(50);
        }
    }
}
