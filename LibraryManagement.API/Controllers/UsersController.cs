using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.DataAccess.DTOs;
using Microsoft.AspNetCore.Authorization;
using LibraryManagement.DataAccess.Enums;
using System.Security.Claims;


namespace LibraryManagement.API.Controllers

    //niye users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // niye public niye private niye readonly
        public UsersController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet] // attirubte ne demek etiket  c++ @ denk mi ??? Iactionresult http response döndürür hazir c# tipi return ok ne demek
        [Authorize(Roles = "LIBRARIAN")] // niye burada
        public async Task<IActionResult> GetAll() {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")] // niye burada
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> GetById(int id) {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }


        [HttpGet("me")]
        [Authorize(Roles =  "MEMBER")]

        public async Task<IActionResult> GetMe()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            int userId = int.Parse(userIdClaim!.Value); //niye soru isareti var useridclaim donen ne ki value dedik

            var user = await _userService.GetByIdAsync(userId);
            return Ok(user);
        }


     

        [HttpPut("{id}")]
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            var result = await _userService.UpdateAsync(id, dto);
            return Ok(result); // niye reutn oik result
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> Delete(int id) {
            var result = await _userService.DeleteAsync(id);
             return Ok(result);
        }

        [HttpGet("search")]
        [Authorize(Roles = "LIBRARIAN")]

        public async Task<IActionResult> SearchByName(string fullName)
        {
            var users = await _userService.SearchByNameAsync(fullName);

            return Ok(users);
        }

    }
}
