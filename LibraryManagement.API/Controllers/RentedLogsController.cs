using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll()
        {
            var rentedLogs = await _logService.GetAllAsync();
            return Ok(rentedLogs);
        }

        [HttpGet("user/{userId}")]
        
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var rentedLogs = await _logService.GetByUserIdAsync(userId);
            return Ok(rentedLogs);
        }

        [HttpPost]
        public async Task<IActionResult> RentBook(RentalCreateDto dto)
        {
            var  result = await _logService.RentBookAsync(dto);
            return StatusCode(StatusCodes.Status201Created, result); // class parametre anlamadim
        }

        //niye return pathi
        [HttpPut("return/{rentedLogId}")]
        public async Task<IActionResult> ReturnBook(int rentedLogId) {
              var result = await _logService.ReturnBookAsync(rentedLogId);
            return Ok(result);

        }

    }
}
