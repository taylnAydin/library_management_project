using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.DataAccess.Entities.Concrete;

namespace LibraryManagement.DataAccess.Repository.Abstract
{
    public interface IRentedLogRepository : IGenericRepository<RentedLog>
    {

        Task<IReadOnlyList<RentedLog>> GetAllWithDetailsAsync();
        Task<IReadOnlyList<RentedLog>> GetLogsByUserIdWithDetailsAsync(int userId);



    }
}