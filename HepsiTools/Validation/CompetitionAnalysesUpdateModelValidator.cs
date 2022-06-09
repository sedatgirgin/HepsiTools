using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class CompetitionAnalysesInsertModelValidator : AbstractValidator<CompetitionAnalysesInsertModel>
    {
        public CompetitionAnalysesInsertModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.HighestPrice).NotEmpty().NotEqual(0).NotEqual(0.0);
            RuleFor(x => x.LowestPrice).NotEmpty().NotEqual(0).NotEqual(0.0);
            RuleFor(x => x.Multiple).NotEmpty().NotEqual(0).NotEqual(0.0);
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.Product).NotEmpty();
            RuleFor(x => x.ProductLink).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty().NotEqual(0);
        }  
    }
}
