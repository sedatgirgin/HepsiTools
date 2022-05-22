using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class UserLisansModelValidator : AbstractValidator<UserLisansModel>
    {
        public UserLisansModelValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Lütfen kullanıcı giriniz.");
            RuleFor(x => x.LisansId).NotEmpty().WithMessage("Lütfen lisans giriniz.");
        }
    }
}
