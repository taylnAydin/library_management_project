using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Entities.Concrete;
using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Business.Services.Concrete
{
    // niye interface var her servisin 
    public class RentedLogService : IRentedLogService
    {
        private readonly IRentedLogRepository _rentedLogRepository;
        private readonly IBookRepository _bookRepository;

        private readonly IUserRepository _userRepository;

        public RentedLogService(IRentedLogRepository repository,IBookRepository bookRepository,IUserRepository userRepository)
        {
            _rentedLogRepository = repository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;

        }
        public async Task<List<RentalListDto>> GetAllAsync()
        {
            var rentedLogs = await _rentedLogRepository.GetAllWithDetailsAsync();

            if (rentedLogs == null || !rentedLogs.Any())
            {
                return new List<RentalListDto>();
            }

            // bak buraya
            var rentedLogsDtos = rentedLogs
                .Where(r => !r.IsDeleted)
                .Select(r => new RentalListDto
                {
                    Id = r.Id,
                    BookTitle = r.Book != null ? r.Book.Title : string.Empty,
                    UserFullName = r.User != null ? $"{r.User.Name} {r.User.Surname}".Trim() : string.Empty,
                    UserIdentityCardNo = r.User != null ? r.User.IdentityCardNo : string.Empty,
                    StartDate = r.StartDate,
                    DueDate = r.DueDate,
                    ReturnDate = r.ReturnDate,
                    Status = r.Status
                })
                .ToList();

            return rentedLogsDtos;
        }
         // bak
        public async Task<List<RentalDetailDto>> GetByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than 0.");
            }

            var logs = await _rentedLogRepository.GetLogsByUserIdWithDetailsAsync(userId);

            if (logs == null || !logs.Any())
            {
                return new List<RentalDetailDto>();
            }

            var result = logs.Select(r => new RentalDetailDto
            {
                Id = r.Id,
                UserId = r.UserId,
                BookId = r.BookId,
                BookTitle = r.Book != null ? r.Book.Title : string.Empty,
                UserFullName = r.User != null ? $"{r.User.Name} {r.User.Surname}".Trim() : string.Empty,
                StartDate = r.StartDate,
                DueDate = r.DueDate,
                ReturnDate = r.ReturnDate,
                Status = r.Status
            }).ToList();

            return result;
        }
          
         public async Task<bool> RentBookAsync(RentalCreateDto dto) //bak
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Rental data cannot be null.");
            }

            if (dto.UserId <= 0)
            {
                throw new ArgumentException("Invalid User ID.");
            }

            if (dto.BookId <= 0)
            {
                throw new ArgumentException("Invalid Book ID.");
            }

            // 1. Kullanıcı Kontrolü
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null || user.IsDeleted)
            {
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");
            }

            // 2. Kitap ve Stok Kontrolü
            var book = await _bookRepository.GetByIdAsync(dto.BookId);
            if (book == null || book.IsDeleted)
            {
                throw new KeyNotFoundException($"Book with ID {dto.BookId} not found.");
            }

            if (!book.IsAvailable || book.Stock <= 0)
            {
                throw new InvalidOperationException($"Book '{book.Title}' is out of stock or not available for rent.");
            }

            try
            {
                // 3. Kitap Stoğunu Güncelle
                book.Stock -= 1;
               
                _bookRepository.Update(book);

                // 4. Yeni Kiralama Kaydı (RentedLog) Oluştur
                var rentedLog = new RentedLog
                {
                    UserId = dto.UserId,
                    BookId = dto.BookId,
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(15)), // Standart 15 gün kiralama süresi
                    ReturnDate = null,
                    Status = RentalStatus.RENTED
                };

                await _rentedLogRepository.AddAsync(rentedLog);
                await _rentedLogRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while renting the book: {ex.Message}", ex);
            }
        }
        public async Task<bool> ReturnBookAsync(int rentedLogId)
        {
            if (rentedLogId <= 0)
            {
                throw new ArgumentException("Rented Log ID must be greater than 0.");
            }

            // 1. Kiralama Kaydını Getir
            var rentedLog = await _rentedLogRepository.GetByIdAsync(rentedLogId);
            if (rentedLog == null || rentedLog.IsDeleted)
            {
                throw new KeyNotFoundException($"Rental record with ID {rentedLogId} not found.");
            }

            // 2. Kitap Zaten İade Edilmiş mi Kontrolü
            if (rentedLog.ReturnDate.HasValue || rentedLog.Status == RentalStatus.RETURNED)
            {
                throw new InvalidOperationException("This book has already been returned.");
            }

            // 3. İlgili Kitabı Getir ve Stoğunu Artır
            var book = await _bookRepository.GetByIdAsync(rentedLog.BookId);
            if (book == null || book.IsDeleted)
            {
                throw new KeyNotFoundException($"Associated book with ID {rentedLog.BookId} not found.");
            }

            try
            {
                // Kitap stoğunu 1 artırıyoruz (IsAvailable otomatik true olacaktır)
                book.Stock += 1;
                _bookRepository.Update(book);

                // Kiralama kaydını kapatıyoruz
                rentedLog.ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow);
                rentedLog.Status = RentalStatus.RETURNED;
                _rentedLogRepository.Update(rentedLog);
                await _rentedLogRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while returning the book: {ex.Message}", ex);
            }
        }
    }
}
