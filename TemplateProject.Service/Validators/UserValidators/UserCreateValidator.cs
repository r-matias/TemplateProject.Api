using FluentValidation;
using System;
using TemplateProject.Domain.Entities.Model;
using TemplateProject.Domain.Interfaces;

namespace TemplateProject.Service.Validators.UserValidators
{
    public class UserCreateValidator : UserCommonValidator
    {
        public UserCreateValidator(IUnitOfWork<User, Guid> unitOfWork)
        {
            RequiredFields();

            RuleFor(c => c.Email).Custom((email, context) =>
            {
                bool hasEmailInDatabase = unitOfWork.User.AnyEntityOnCondition(x => x.Email == email).Result;

                if (hasEmailInDatabase)
                    context.AddFailure($"Já existe um usuário com esse e-mail {email}.");
            });
        }
    }
}
