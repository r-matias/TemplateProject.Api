using AutoMapper;
using System;
using System.Threading.Tasks;
using TemplateProject.Domain.Entities.Model;
using TemplateProject.Domain.Interfaces;
using TemplateProject.Models.ViewModel.UserViewModel;

namespace TemplateProject.Service.Services
{
    public class UserService : BaseService<User, Guid>, IUserService
    {
        public UserService(IUnitOfWork<User, Guid> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<dynamic> Authenticate(UserAuthViewModel userAuthViewModel)
        {
            User user = await _unitOfWork.Repository
                                         .FirstOrDefaultOnCondition(x => userAuthViewModel.Email == x.Email &&
                                                                         userAuthViewModel.Password == x.Password);

            if (user == null)
                return new
                {
                    message = "Usuário não encontrado."
                };

            string token = TokenService.GenerateToken(user);

            UserAuthenticatedViewModel userViewModel = _mapper.Map<User, UserAuthenticatedViewModel>(user);

            return new
            {
                user = userViewModel,
                token = token
            };
        }
    }
}
