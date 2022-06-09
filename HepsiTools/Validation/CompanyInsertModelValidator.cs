using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class CompanyInsertModelValidator : AbstractValidator<CompanyInsertModel>
    {
        public CompanyInsertModelValidator()
        {
            RuleFor(x => x.CompanyType).NotEmpty();
            RuleFor(x => x.SupplierId).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }  
    }
}
