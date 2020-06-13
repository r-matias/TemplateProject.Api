using TemplateProject.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace TemplateProject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterAutoMapperProfiles();
            services.AddControllers();

            services.RegisterIoC();
            services.ConfigureDbContext(Configuration.GetValue<string>("TemplateProjectConnectionString"));

            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Indicadores Econ�micos",
                        Version = "v1",
                        Description = "Exemplo de API REST criada com o ASP.NET Core 3.0 para consulta a indicadores econ�micos",
                        Contact = new OpenApiContact
                        {
                            Name = "Renato Groffe",
                            Url = new Uri("https://github.com/renatogroffe")
                        }
                    });
            });

            //services.AddAuthentication()
            //        .AddGoogle(options =>
            //        {
            //            options.ClientId = Configuration.GetValue<string>("ExternalLogin:Google:ClientId");
            //            options.ClientSecret = Configuration.GetValue<string>("ExternalLogin:Google:ClientSecret");
            //        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Indicadores Econ�micos V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
