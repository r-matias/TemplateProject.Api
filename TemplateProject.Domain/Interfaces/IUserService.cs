using System;
using System.Threading.Tasks;
using TemplateProject.Domain.Entities.Model;
using TemplateProject.Models.ViewModel.UserViewModel;

namespace TemplateProject.Domain.Interfaces
{
    public interface IUserService : IServiceBase<User, Guid>
    {
        Task<dynamic> Authenticate(UserAuthViewModel userAuthViewModel);
    }
}
