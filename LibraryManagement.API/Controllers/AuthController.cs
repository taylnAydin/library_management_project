using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.API.Services.Abstract;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]//bunlar ne
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _tokenService;

        public AuthController(IUserService userService, IJwtTokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        // niye post niye dto yazdik login icin
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var result = await _userService.LoginAsync(dto); // niye detail dondum cunki ui ya yetismedi
            string JWT = _tokenService.GenerateToken(result); // niye token dondurduk cunki ui ya yetismedi
            return Ok(new { User = result, Token = JWT }); // new niye new 
        }

        [HttpPost("register")] // NIYE REGISTER YAZDIK CUNKU REGISTER ICIN AYRI BIR POST METODU OLUSTURDUK
        public async Task<IActionResult> Register(UserRegisterDto dto) // kayit basarili diyip tekrar login sayfasina yonlendiricek yada dogrudan ana sayfaya planlicaz
        {
            var result = await _userService.RegisterAsync(dto);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
