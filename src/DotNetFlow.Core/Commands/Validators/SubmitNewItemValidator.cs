using FluentValidation;

namespace DotNetFlow.Core.Commands.Validators
{
    public sealed class SubmitNewItemValidator : AbstractValidator<SubmitNewItemCommand>
    {
        public SubmitNewItemValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().Length(1, 1000).WithName("Your Name");
            RuleFor(x => x.Title).NotEmpty().Length(10, 140);
            RuleFor(x => x.Content).NotEmpty().Length(1, 2000);
        }
    }
}