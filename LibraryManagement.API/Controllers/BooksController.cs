using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll() {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id) {
            var book = await _bookService.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateDto dto)
        {
            var result = await _bookService.AddAsync(dto);

             if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookUpdateDto dto)
        {
            var result = await _bookService.UpdateAsync(id, dto);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteAsync(id);

            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
