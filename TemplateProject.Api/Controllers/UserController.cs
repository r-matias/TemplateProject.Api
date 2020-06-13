using TemplateProject.Domain.Interfaces;
using TemplateProject.Entities.Model;
using TemplateProject.Models.ViewModel.UserViewModel;
using TemplateProject.Service.Validators.WorldValidators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TemplateProject.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IService<User, Guid> _baseService;

        public UserController(IService<User, Guid> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _baseService.Get<UserGetListViewModel>();

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _baseService.Get<UserGetIdViewModel>(id);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserCreateViewModel world)
        {
            var response = await _baseService.Post<UserCreateValidator, UserBaseViewModel>(world);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]UserUpdateViewModel world)
        {
            var response = await _baseService.Put<UserUpdateValiadtor, UserBaseViewModel>(id, world);

            return Ok(response);
        }
    }
}
