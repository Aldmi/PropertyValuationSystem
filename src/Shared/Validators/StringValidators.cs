using FluentValidation;

namespace Shared.Kernel.Validators
{
   
    public class StringNotNullNotEmptyValidator : AbstractValidator<string>
    {
        public StringNotNullNotEmptyValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x).NotEmpty();
        }
    }


    public class StringNotNullNotEmptyLenghtValidator : AbstractValidator<string>
    {
        public StringNotNullNotEmptyLenghtValidator(int min, int max)
        {
            RuleFor(x => x).SetValidator(new StringNotNullNotEmptyValidator());
            RuleFor(x => x).Length(min, max);
        }
    }

}