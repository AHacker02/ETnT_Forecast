using System.Linq;
using Common.Commands;
using DataAccess;
using DataAccess.DbSets;
using FluentValidation;

namespace Service
{
    public class ForecastCommandValidator : AbstractValidator<ForecastCommand>
    {
        public ForecastCommandValidator()
        {
            RuleFor(x => x.Org)
                .NotEmpty()
                .Must(Exists<Org>).WithMessage("{PropertyName} not found");

            RuleFor(x => x.Project)
                .NotEmpty()
                .Must(Exists<Project>).WithMessage("{PropertyName} not found");

            RuleFor(x => x.SkillGroup)
                .NotEmpty()
                .Must(Exists<Skill>).WithMessage("{PropertyName} not found");

            RuleFor(x => x.Business)
                .NotEmpty()
                .Must(Exists<Business>).WithMessage("{PropertyName} not found");

            RuleFor(x => x.Capability)
                .NotEmpty()
                .Must(Exists<Capability>).WithMessage("{PropertyName} not found");

            RuleFor(x => x.ForecastConfidence)
                .NotEmpty()
                .Must(Exists<Category>).WithMessage("{PropertyName} not found");

            RuleFor(x => x.Year)
                .NotEqual(0);

            RuleFor(x => x.Project)
                .NotEmpty()
                .Must(Exists<Project>).WithMessage("{PropertyName} not found");

            RuleFor(x => x.Manager)
                .NotEmpty()
                .Must(Exists).WithMessage("{PropertyName} not found");

            RuleFor(x => x.USFocal)
                .NotEmpty()
                .Must(Exists).WithMessage("{PropertyName} not found");
        }

        private static bool Exists<T>(string name) where T : Lookup
        {
            using var context = new ForecastContext();
            return context.Set<T>().Any(x => x.Value == name);
        }

        private static bool Exists(string name)
        {
            using var context = new ForecastContext();
            return context.Set<User>().Any(x => x.FullName == name);
        }
    }
}