using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

        public static void ConfigureJWTAuthenticate(this IServiceCollection services, string clientSecret)
        {
            byte[] key = Encoding.ASCII.GetBytes(clientSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
