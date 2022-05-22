using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class LisansModelValidator : AbstractValidator<LisansModel>
    {
        public LisansModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen isim giriniz.");
        }
    }
}
