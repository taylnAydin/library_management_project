using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities.Concrete;

namespace LibraryManagement.Core.Interfaces
{
    public interface IRentedLogRepository : IGenericRepository<RentedLog>
    {

        Task<IReadOnlyList<RentedLog>> GetAllWithDetailsAsync();
        Task<IReadOnlyList<RentedLog>> GetLogsByUserIdWithDetailsAsync(int userId);



    }
}