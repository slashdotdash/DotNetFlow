using FluentValidation;

namespace DotNetFlow.Core.Commands.Validators
{
    public sealed class SubmitNewItemValidator : AbstractValidator<SubmitNewItemCommand>
    {
        public SubmitNewItemValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(100);
            RuleFor(x => x.Content).NotEmpty().Length(1000);
        }
    }
}