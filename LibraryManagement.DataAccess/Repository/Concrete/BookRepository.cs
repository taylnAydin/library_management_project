using LibraryManagement.DataAccess.Entities.Concrete;
using LibraryManagement.DataAccess.Repository.Abstract;
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
           

            return await _dbSet
                .Where(b => b.Category.ToLower().Contains(category.ToLower()))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Book>> SearchByTitleAsync(string title)
        {
            

            return await _dbSet
                .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();
        }
    }
}