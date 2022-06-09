using FluentValidation;
using HepsiTools.Models;

namespace HepsiTools.Validation
{
    public class CompetitionAnalysesUpdateModelValidator : AbstractValidator<CompetitionAnalysesUpdateModel>
    {
        public CompetitionAnalysesUpdateModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.StatusType).NotEmpty();

        }
    }
}
