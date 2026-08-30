using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedLogsController : ControllerBase
    {
        private readonly IRentedLogService _logService;

        public RentedLogsController(IRentedLogService logService) {

            _logService = logService;
        }

        [HttpGet]
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> GetAll()
        {
            var rentedLogs = await _logService.GetAllAsync();
            return Ok(rentedLogs);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var rentedLogs = await _logService.GetByUserIdAsync(userId);
            return Ok(rentedLogs);
        }

        [HttpPost]
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> RentBook(RentalCreateDto dto)

        {
            var  result = await _logService.RentBookAsync(dto);
            return StatusCode(StatusCodes.Status201Created, result); // class parametre anlamadim
        }

        //niye return pathi
        [HttpPut("return/{rentedLogId}")]
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> ReturnBook(int rentedLogId) {
              var result = await _logService.ReturnBookAsync(rentedLogId);
            return Ok(result);

        }

        [HttpGet("my")]
        [Authorize(Roles = "MEMBER")]
        public async Task<IActionResult> GetMyRentals()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim!.Value);

            var renterLogs = await _logService.GetByUserIdAsync(userId);
            return Ok(renterLogs);
        }

    }
}
