using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]//bunlar ne
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        // niye post niye dto yazdik login icin
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var result = await _userService.LoginAsync(dto); // niye detail dondum cunki ui ya yetismedi
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto dto) // kayit basarili diyip tekrar login sayfasina yonlendiricek yada dogrudan ana sayfaya planlicaz
        {
            var result = await _userService.RegisterAsync(dto);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
