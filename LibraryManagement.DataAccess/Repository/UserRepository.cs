using LibraryManagement.Core.Entities.Concrete;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context) { 
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (email == null) throw new ArgumentNullException("email cannot be null");

            return await _dbSet.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetByIdentificationNumberAsync(string identificationNumber)
        {
            if (identificationNumber == null) throw new ArgumentNullException("identity card no cannot be null");

            return await _dbSet.FirstOrDefaultAsync(u => u.IdentityCardNo.ToLower() == identificationNumber.ToLower());
        }

        public async Task<User?> GetUserWithRentalsByIdentityCardNoAsync(string identityCardNo)
        {
            if (string.IsNullOrWhiteSpace(identityCardNo))
                throw new ArgumentException("Identity card number cannot be empty.", nameof(identityCardNo));

            // bu kisim
            return await _dbSet
                .Include(u => u.RentedLogs)
                    .ThenInclude(r => r.Book)
                .FirstOrDefaultAsync(u => u.IdentityCardNo == identityCardNo.Trim());
        }

        public async Task<IReadOnlyList<User>> SearchByFullNameAsync(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Search term cannot be empty.", nameof(fullName));

            string term = fullName.Trim().ToLower();

            // bu kisim
            return await _dbSet
                .Where(u => (u.Name + " " + u.Surname).ToLower().Contains(term))
                .ToListAsync();
        }
    }
}
