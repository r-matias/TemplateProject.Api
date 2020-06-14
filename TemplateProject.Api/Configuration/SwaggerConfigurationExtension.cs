using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace TemplateProject.Api.Configuration
{
    public static class SwaggerConfigurationExtension
    {
        public static void ConfigureSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TemplateProject.Api V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "TemplateProject.Api V2");
                c.RoutePrefix = string.Empty;
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning();

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
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

                c.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Title = "TemplateProject.Api",
                        Version = "v2",
                        Description = "TemplateProject.Api [incluir descrição]",
                        Contact = new OpenApiContact
                        {
                            Name = "Rafael Soarde Matias",
                            Url = new Uri("https://github.com/r-matias")
                        }
                    });


                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                c.DocInclusionPredicate((version, desc) =>
                {
                    var versions = desc.ActionDescriptor
                                       .EndpointMetadata
                                       .OfType<ApiVersionAttribute>()
                                       .SelectMany(attr => attr.Versions);

                    var maps = desc.ActionDescriptor
                                   .EndpointMetadata
                                   .OfType<MapToApiVersionAttribute>()
                                   .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == version)
                          && (!maps.Any() || maps.Any(v => $"v{v.ToString()}" == version));
                });
            });
        }
    }
}
