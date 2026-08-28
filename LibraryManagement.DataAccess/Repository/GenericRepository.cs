using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks; // add if not already available via global usings
using LibraryManagement.Core.Interfaces;
using LibraryManagement.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context; // neden protected read ve alttan cizgi
        protected readonly DbSet<T> _dbSet;


        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet   = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
             await _dbSet.AddAsync(entity); // niye return yok ?
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity); //nie async değil
        }

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // update delete niye değil
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        //niye context niye db degil
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
