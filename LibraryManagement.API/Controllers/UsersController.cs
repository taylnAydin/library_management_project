using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;

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
        public async Task<IActionResult> GetAll() {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")] // niye burada
        public async Task<IActionResult> GetById(int id) {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }


     

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            var result = await _userService.UpdateAsync(id, dto);
            return Ok(result); // niye reutn oik result
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var result = await _userService.DeleteAsync(id);
             return Ok(result);
        }

        [HttpGet("search")]

        public async Task<IActionResult> SearchByName(string fullName)
        {
            var users = await _userService.SearchByNameAsync(fullName);

            return Ok(users);
        }

    }
}
