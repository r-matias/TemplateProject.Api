using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TemplateProject.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("versao 2");
        }
    }
}
