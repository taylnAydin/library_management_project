using LibraryManagement.Core.DTOs;

namespace LibraryManagement.API.Services.Abstract
{
    public interface IJwtTokenService
    {
        public string GenerateToken(UserLoginResultDto userLoginResultDto);
    }
}
