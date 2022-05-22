using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            RuleFor(x => x.WooCommerceId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
