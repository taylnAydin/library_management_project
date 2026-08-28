using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Business.Services.Abstract;

namespace LibraryManagement.API.Controllers

    //niye users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        
        public UsersController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet] // attirubte ne demek etiket  c++ @ denk mi ??? Iactionresult http response döndürür hazir c# tipi
        public async Task<IActionResult> GetAll() { 
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
    }
}
