using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities.Concrete;

namespace LibraryManagement.Core.Interfaces
{
    public interface IRentalRepository : IGenericRepository<RentedLog>
    {

        Task<IReadOnlyList<RentedLog>> GetAllWithDetailsAsync();


        Task<RentedLog?> GetByIdWithDetailsAsync(int id);
    }
}