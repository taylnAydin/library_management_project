using LibraryManagement.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserRegisterDto dto); 
        Task<UserLoginResultDto> LoginAsync(UserLoginDto dto);
        Task<List<UserListDto>> GetAllAsync();
        Task<UserDetailDto> GetByIdAsync(int id); //niye soru isareti
        Task<bool> UpdateAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteAsync(int id);

  
        Task<List<UserListDto>> SearchByNameAsync(string fullName);
        
    }
}