using FluentValidation;
using TemplateProject.Models.ViewModel.UserViewModel;

namespace TemplateProject.Service.Validators.UserValidators
{
    public class UserCommonValidator : AbstractValidator<UserBaseViewModel>
    {
        public void RequiredFields()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("É necessário informar o E-mail.")
                .NotNull().WithMessage("É necessário informar o E-mail.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("É necessário informar a Senha.")
                .NotNull().WithMessage("É necessário informar a Senha.");

            RuleFor(c => c.Role)
                .NotEmpty().WithMessage("É necessário informar uma Role.")
                .NotNull().WithMessage("É necessário informar uma Role.");
        }
    }
}
