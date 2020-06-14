using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TemplateProject.Domain.Interfaces;
using TemplateProject.Models.ViewModel.UserViewModel;
using TemplateProject.Service.Validators.UserValidators;

namespace TemplateProject.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _userService.Get<UserGetListViewModel>();

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _userService.Get<UserGetIdViewModel>(id);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserCreateViewModel user)
        {
            var response = await _userService.Post<UserCreateValidator, UserBaseViewModel>(user);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]UserUpdateViewModel user)
        {
            var response = await _userService.Put<UserUpdateValiadtor, UserBaseViewModel>(id, user);

            return Ok(response);
        }

        [HttpPost]
        [Route("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]UserAuthViewModel user)
        {
            var response = await _userService.Authenticate(user);

            return Ok(response);
        }
    }
}
