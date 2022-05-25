using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class WooCommerceInsertModelValidator : AbstractValidator<WooCommerceInsertModel>
    {
        public WooCommerceInsertModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5);
            RuleFor(x => x.StoreURL).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Consumer_key).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Consumer_secret).NotEmpty().MinimumLength(5);
        }
    }
}
