using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.DataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;


        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id) {
            var book = await _bookService.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> Create(BookCreateDto dto)
        {
            var result = await _bookService.AddAsync(dto);



            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> Update(int id, BookUpdateDto dto)
        {
            var result = await _bookService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "LIBRARIAN")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
