using LibraryManagement.DataAccess.Entities.Concrete;
using LibraryManagement.DataAccess.Repository.Abstract;
using LibraryManagement.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.Repository.Concrete
{
    public class RentedLogRepository : GenericRepository<RentedLog>, IRentedLogRepository
    {
        public RentedLogRepository(AppDbContext context):base(context)
        { }
        public async Task<IReadOnlyList<RentedLog>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(r => r.User)
                .Include(r => r.Book)
                .ToListAsync();
        }
         
        //bak
        public async Task<IReadOnlyList<RentedLog>> GetLogsByUserIdWithDetailsAsync(int userId)
        {
            return await _dbSet
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .Include(r => r.User)
                .Include(r => r.Book)
                .ToListAsync();
        }


    }
}
