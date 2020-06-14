using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var typesRepositoriesInterface = typeof(IBaseRepository<,>).Assembly.GetTypes().ToList();
            var typesRepositoriesClass = typeof(BaseRepository<,>).Assembly.GetTypes().ToList();

            RegisterDynamic(services, typesRepositoriesInterface, typesRepositoriesClass);
        }

        private void RegisterService(IServiceCollection services)
        {
            var typesServicesInterface = typeof(IServiceBase<,>).Assembly.GetTypes().ToList();
            var typesServicesClass = typeof(ServiceBase<,>).Assembly.GetTypes().ToList();

            RegisterDynamic(services, typesServicesInterface, typesServicesClass);
        }

        private void RegisterDynamic(IServiceCollection services, List<Type> interfacesTypes, List<Type> classesTypes)
        {
            foreach (Type typeClass in classesTypes)
            {
                Type typeInterface = interfacesTypes.FirstOrDefault(x => x.Name.Contains(typeClass.Name));

                if (typeInterface != null)
                {
                    services.AddScoped(typeInterface, typeClass);
                }
            }
        }
    }
}
