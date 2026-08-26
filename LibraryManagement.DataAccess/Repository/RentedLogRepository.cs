using LibraryManagement.Core.Entities.Concrete;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.Repository
{
    public class RentedLogRepository : GenericRepository<RentedLog>, IRentalRepository
    {
        public RentedLogRepository(AppDbContext context):base(context)
        { }
        public async Task<IReadOnlyList<RentedLog>> GetAllWithDetailsAsync()
        {
            return await _dbSet.ToListAsync();
        }

      
    }
}
