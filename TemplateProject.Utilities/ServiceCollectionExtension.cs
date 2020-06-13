using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Infra.Data.Context;
using TemplateProject.Infra.IoC;
using TemplateProject.Utilities.AutoMapper;

namespace TemplateProject.Utilities
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterIoC(this IServiceCollection services)
        {
            services.AddScoped<SqlContext>();

            new RegisterDependencyInjection(services);
        }

        public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SqlContext>(options => options.UseSqlServer(connectionString));
        }

        public static void RegisterAutoMapperProfiles(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
