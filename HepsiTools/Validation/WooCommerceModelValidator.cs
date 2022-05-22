using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class WooCommerceModelValidator : AbstractValidator<WooCommerceModel>
    {
        public WooCommerceModelValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.StoreURL).NotEmpty();
            RuleFor(x => x.Consumer_key).NotEmpty();
            RuleFor(x => x.Consumer_secret).NotEmpty();
        }
    }
}
