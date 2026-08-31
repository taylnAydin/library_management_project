using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.DataAccess.Entities.Concrete;

namespace LibraryManagement.DataAccess.Repository.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
   
        Task<User?> GetByEmailAsync(string email);

     
        Task<User?> GetByIdentificationNumberAsync(string identificationNumber);


        Task<User?> GetUserWithRentalsByIdentityCardNoAsync(string identityCardNo); // niye soru isareti

        Task<IReadOnlyList<User>> SearchByFullNameAsync(string fullName);
    }
}