using LibraryManagement.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Business.Services.Abstract
{
    public interface IRentedLogService
    {
        Task<List<RentalListDto>> GetAllAsync();
        Task<RentalDetailDto?> GetByIdAsync(int id);
        Task<bool> RentBookAsync(RentalCreateDto dto);
        Task<bool> ReturnBookAsync(int rentedLogId);
    }
}