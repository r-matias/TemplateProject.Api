using FluentValidation;
using TemplateProject.Domain.Interfaces;
using TemplateProject.Entities.Model;
using TemplateProject.Service.Validators.UserValidators;
using System;

namespace TemplateProject.Service.Validators.WorldValidators
{
    public class UserCreateValidator : UserCommonValidator
    {
        public UserCreateValidator(IUnitOfWork<User, Guid> unitOfWork)
        {
            RequiredFields();

            RuleFor(c => c.Email).Custom((email, context) =>
            {
                bool hasWorldName = unitOfWork.User.AnyEntityOnCondition(x => x.Email == email).Result;

                if (hasWorldName)
                    context.AddFailure($"Já existe um usuário com esse e-mail {email}.");
            });
        }
    }
}
