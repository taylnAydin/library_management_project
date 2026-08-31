using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DataAccess.Entities.Concrete;

namespace LibraryManagement.DataAccess.Repository.Abstract
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IReadOnlyList<Book>> SearchByTitleAsync(string title);

        Task<IReadOnlyList<Book>> GetBooksByAuthorAsync(string writer);

        Task<IReadOnlyList<Book>> GetBooksByCategoryAsync(string category);

        Task<IReadOnlyList<Book>> GetBooksByAvailibityAsync(bool availibility);
    }
}