using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
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

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "TemplateProject.Api",
                        Version = "v1",
                        Description = "TemplateProject.Api [incluir descrição]",
                        Contact = new OpenApiContact
                        {
                            Name = "Rafael Soarde Matias",
                            Url = new Uri("https://github.com/r-matias")
                        }
                    });
            });

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
            
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TemplateProject.Api V1");
                c.RoutePrefix = string.Empty;
            });

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
