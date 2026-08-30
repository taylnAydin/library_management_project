using LibraryManagement.Core.Entities.Concrete;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.Repository.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context) { 
        }

        public async Task<User?> GetByEmailAsync(string email)
        {

            return await _dbSet.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetByIdentificationNumberAsync(string identificationNumber)
        {
           

            return await _dbSet.FirstOrDefaultAsync(u => u.IdentityCardNo.ToLower() == identificationNumber.ToLower());
        }

        public async Task<User?> GetUserWithRentalsByIdentityCardNoAsync(string identityCardNo)
        {
           

            // bu kisim
            return await _dbSet
                .Include(u => u.RentedLogs)
                    .ThenInclude(r => r.Book)
                .FirstOrDefaultAsync(u => u.IdentityCardNo == identityCardNo.Trim());
        }

        public async Task<IReadOnlyList<User>> SearchByFullNameAsync(string fullName)
        {
            

            string term = fullName.Trim().ToLower();

            // bu kisim
            return await _dbSet
                .Where(u => (u.Name + " " + u.Surname).ToLower().Contains(term))
                .ToListAsync();
        }
    }
}
