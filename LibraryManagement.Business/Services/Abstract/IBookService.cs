using LibraryManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Business.Services.Abstract
{
    public interface IBookService
    {
        Task<List<BookDetailDto>> GetAllAsync();
        Task<BookDetailDto> GetByIdAsync(int id);
        Task<bool> AddAsync(BookCreateDto dto);

        Task<bool> UpdateAsync(int id ,BookUpdateDto dto);

        Task<bool> DeleteAsync(int id);


    }
}
