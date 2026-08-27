using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Business.Services.Concrete
{
    public class UserService : IUserService
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailDto?> GetUserWithRentalsByIdentityCardNoAsync(string identityCardNo)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailDto?> LoginAsync(UserLoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(UserRegisterDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserListDto>> SearchByNameAsync(string fullName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, UserUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
