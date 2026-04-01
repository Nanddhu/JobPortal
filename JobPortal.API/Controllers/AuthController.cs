using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            return Ok(await _service.Register(user));
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
           var token=await _service.Login(user);
            return Ok(new { token });
        }
    }
}
