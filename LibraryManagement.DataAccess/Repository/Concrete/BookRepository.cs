using LibraryManagement.Core.Entities.Concrete;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DataAccess.Repository.Concrete
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Book>> GetBooksByAuthorAsync(string writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer), "Author cannot be null");

            return await _dbSet
                .Where(b => b.Writer.ToLower().Contains(writer.ToLower()))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Book>> GetBooksByAvailibityAsync(bool availibility)
        {
            return await _dbSet
                .Where(b => b.IsAvailable == availibility)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Book>> GetBooksByCategoryAsync(string category)
        {
            if (category == null)
                throw new ArgumentException("Category cannot be null");

            return await _dbSet
                .Where(b => b.Category.ToLower().Contains(category.ToLower()))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Book>> SearchByTitleAsync(string title)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title), "Title cannot be null.");

            return await _dbSet
                .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();
        }
    }
}