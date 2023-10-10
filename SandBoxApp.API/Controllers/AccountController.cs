using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SandBoxApp.Application.Contracts;
using SandBoxApp.Application.DTOs;
using SandBoxApp.Application.DTOs.Requests;

namespace SandBoxApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDTO newUserDTO)
        {
            _accountService.Register(newUserDTO);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest loginDTO)
        {
            var result = await _accountService.Login(loginDTO); 
            return Ok(result);
        }
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> TestAsync()
        {
            //var login = new LoginDTO() {Email = "test@test.com", Password = "Pass123" }; //{ Email:"test@test.com", Password: "Pass123"}
            //string result = await _accountService.GenerateJwtAsync(login);
            return Ok("You are authorized!");
        }
    }
}
