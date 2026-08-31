using LibraryManagement.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Business.Services.Abstract
{
    public interface IRentedLogService
    {
        Task<List<RentalListDto>> GetAllAsync();
        Task<List<RentalDetailDto>> GetByUserIdAsync(int userId);
        Task<bool> RentBookAsync(RentalCreateDto dto);
        Task<ReturnBookResultDto> ReturnBookAsync(int rentedLogId);

        Task<bool> DeleteAsync(int rentedLogId);
    }
}