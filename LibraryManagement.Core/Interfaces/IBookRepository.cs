using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities.Concrete;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IReadOnlyList<Book>> SearchByTitleAsync(string title);

        Task<IReadOnlyList<Book>> GetBooksByAuthorAsync(string auth);

        Task<IReadOnlyList<Book>> GetBooksByCategoryAsync(string category);

        Task<IReadOnlyList<Book>> GetBooksByAvailibityAsync(bool availibility);
    }
}