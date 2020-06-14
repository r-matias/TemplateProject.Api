using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace TemplateProject.Api
{
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var dictionary = swaggerDoc.Paths
                                       .ToDictionary(path => path.Key.Replace("v{version}", swaggerDoc.Info.Version),
                                                     path => path.Value);

            var json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

            OpenApiPaths openApiPathsUpdated = JsonConvert.DeserializeObject<OpenApiPaths>(json);

            swaggerDoc.Paths = openApiPathsUpdated;
        }
    }
}
