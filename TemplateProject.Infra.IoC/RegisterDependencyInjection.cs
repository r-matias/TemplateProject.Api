using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Domain.Interfaces;
using TemplateProject.Infra.Data.Repository;
using TemplateProject.Service.Services;

namespace TemplateProject.Infra.IoC
{
    public class RegisterDependencyInjection
    {
        public RegisterDependencyInjection(IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<,>), typeof(UnitOfWork<,>));
            RegisterService(services);
            RegisterRepository(services);
        }

        private void RegisterRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
        }

        private void RegisterService(IServiceCollection services)
        {
            services.AddScoped(typeof(IService<,>), typeof(BaseService<,>));

            services.AddScoped<IUserService, UserService>();
        }
    }
}
