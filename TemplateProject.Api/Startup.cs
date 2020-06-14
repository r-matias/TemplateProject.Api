using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TemplateProject.Api.Configuration;
using TemplateProject.Service.Services;
using TemplateProject.Utilities;

namespace TemplateProject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.ConfigureVersioning();
            services.ConfigureSwaggerGen();

            TokenService.SetSecretToken(Configuration.GetValue<string>("CustomLogin:ClientSecret"));

            services.RegisterAutoMapperProfiles();
            services.RegisterIoC();
            services.ConfigureDbContext(Configuration.GetValue<string>("TemplateProjectConnectionString"));
            services.ConfigureJWTAuthenticate(Configuration.GetValue<string>("CustomLogin:ClientSecret"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
