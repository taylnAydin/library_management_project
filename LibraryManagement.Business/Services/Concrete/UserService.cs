using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Entities.Concrete;
using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace LibraryManagement.Business.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // niye private niye public niye readonly
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id must be greater than zero.");
            }

            var user = await _userRepository.GetByIdAsync(id);

            if(user == null) throw new KeyNotFoundException($"User with id {id} not found.");


            if (user.IsDeleted)
            {
                    throw new KeyNotFoundException($"User with id {id} not found.");
             }

           
                user.IsDeleted = true;
                


                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
                return true;
            
        }

        public async Task<List<UserListDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            if (users == null || users.Count == 0)
            {
                return new List<UserListDto>(); // new ve reference kullanimina bi bak
            }

            var userDtos = users.Where(u => !u.IsDeleted)
                .Select(u => new UserListDto
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                IdentityCardNo = u.IdentityCardNo
            }).ToList();

            return userDtos;

        }

        public async Task<UserDetailDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than 0.");
            }

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} was not found.");
            }

            if (user.IsDeleted)
            {

                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            return new UserDetailDto{
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                IdentityCardNo = user.IdentityCardNo,
                BirthdayDate = user.BirthdayDate,
                Country = user.Country,
                Gender = user.Gender,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }



        public async Task<bool> LoginAsync(UserLoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (user.IsDeleted || !user.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.Password
            );

            if (!isPasswordCorrect)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

           

            return true;
        }

        public async Task<bool> RegisterAsync(UserRegisterDto dto)

            // niye try catch kullanmadık çünkü exception fır
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if(user != null) {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var userByIdentityCard = await _userRepository.GetByIdentificationNumberAsync(dto.IdentityCardNo);
            if (userByIdentityCard != null) {
                throw new InvalidOperationException("User with this identity card number already exists.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

     
            var newUser = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Phone = dto.Phone,
                IdentityCardNo = dto.IdentityCardNo,
                BirthdayDate = dto.BirthdayDate,
                Country = dto.Country,
                Gender = dto.Gender,
                Password = hashedPassword,
                Role = UserRole.MEMBER, 
                IsActive = true
            };

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserListDto>> SearchByNameAsync(string fullName)
        {
            if(string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Full name cannot be null.");
            }

            var users = await _userRepository.SearchByFullNameAsync(fullName);
            return users.Where(u => !u.IsDeleted).Select(u => new UserListDto
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                IdentityCardNo = u.IdentityCardNo

            }).ToList();
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than zero.");
            }

            if (dto == null) { 
                throw new ArgumentNullException("UserUpdateDto cannot be null."); //arguemnnull argumen extreipn turler
            }

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) 
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            if(user.IsDeleted)
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

          
                user.Name = dto.Name;
                user.Surname = dto.Surname;
                user.Email = dto.Email;
                user.BirthdayDate = dto.BirthdayDate;
                user.Gender = dto.Gender;
                user.Country = dto.Country;
                user.IdentityCardNo = dto.IdentityCardNo;
                user.Phone = dto.Phone;

                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
                return true;
            
           
        }
    }
}
