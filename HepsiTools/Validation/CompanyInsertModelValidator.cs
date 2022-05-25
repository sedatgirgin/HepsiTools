using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class CompanyInsertModelValidator : AbstractValidator<CompanyInsertModel>
    {
        public CompanyInsertModelValidator()
        {
            RuleFor(x => x.CompanyType).NotEmpty();
            RuleFor(x => x.SupplierId).NotEmpty().MinimumLength(5);
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5);
            RuleFor(x => x.CargoCompanyId).NotEmpty().MinimumLength(5);
            RuleFor(x => x.CustomResourceName).NotEmpty().MinimumLength(5);
        }  
    }
}
