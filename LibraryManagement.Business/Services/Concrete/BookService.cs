using LibraryManagement.Business.Services.Abstract;
using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Entities.Concrete;
using LibraryManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace LibraryManagement.Business.Services.Concrete
{
    public class BookService : IBookService
    {   // niye private
        private readonly IBookRepository _bookRepository;

        // consturcor injection niye interfaceden türettik
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //dto, business logic, ??
        public async Task<bool> AddAsync(BookCreateDto dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException("dto cannot be null");
            }
            //null ve whitespace farki
            if (string.IsNullOrWhiteSpace(dto.Title)) throw new ArgumentException("Title cannot be null");

            if (dto.Title.Length > 150) throw new ArgumentException("Title length cannot be greater than 150 characters");

            if (string.IsNullOrWhiteSpace(dto.Writer)) throw new ArgumentException("Writer cannot be null");

            if (dto.Writer.Length > 100) throw new ArgumentException("Writer length cannot be greater than 100 characters");

            if (string.IsNullOrWhiteSpace(dto.Category)) throw new ArgumentException("Category cannot be null");

            if (dto.Category.Length > 50) throw new ArgumentException("Category length cannot be greater than 50 characters");

            if (string.IsNullOrWhiteSpace(dto.Publisher)) throw new ArgumentException("Publisher cannot be null");

            if (dto.Publisher.Length > 100) throw new ArgumentException("Publisher cannot be greater than 100 characters");

            if (dto.Pages <= 0) throw new ArgumentException("Page number cannot be negative");

            if (dto.Stock < 0) throw new ArgumentException("Stock cannot be negative");

            //dateonly kismina bak
            var book = new Book {  Title = dto.Title, Writer = dto.Writer, Category = dto.Category,Stock =dto.Stock, PublishDate=dto.PublishDate, Publisher=dto.Publisher, AddedDate=DateOnly.FromDateTime(DateTime.UtcNow), Pages=dto.Pages};


                await _bookRepository.AddAsync(book);
                await _bookRepository.SaveChangesAsync();
                return true;
          
            
            
            

        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} was not found.");
            }

            if (book.IsDeleted)
            {
                throw new KeyNotFoundException($"Book with ID {id} was not found.");
            }

            
                // Soft delete: Veriyi kaybetmiyoruz, pasife çekiyoruz
                book.IsDeleted = true;
                

                _bookRepository.Update(book);
                await _bookRepository.SaveChangesAsync();
                return true;
           
          
        }

        public async Task<List<BookDetailDto>> GetAllAsync()
        {
            // niye try catch alamadim
           var books = await _bookRepository.GetAllAsync();

            if (books == null || !books.Any())
            {
                return new List<BookDetailDto>();
            }
            // buraya bak
            var bookDtos = books
                .Where(b => !b.IsDeleted)
                .Select(b => new BookDetailDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Writer = b.Writer,
                    Category = b.Category,
                    Publisher = b.Publisher,
                    Pages = b.Pages,
                    Stock = b.Stock,
                    IsAvailable = b.IsAvailable,
                    PublishDate = b.PublishDate,
                    AddedDate = b.AddedDate
                })
                .ToList();

            return bookDtos;

        }

        
            public async Task<BookDetailDto> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than 0.");
            }

            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} was not found.");
            }

            if (book.IsDeleted)
            {
                throw new KeyNotFoundException($"Book with ID {id} was not found.");
            }

            return new BookDetailDto
            {
                Id = book.Id,
                Title = book.Title,
                Writer = book.Writer,
                Category = book.Category,
                Publisher = book.Publisher,
                Pages = book.Pages,
                Stock = book.Stock,
                IsAvailable = book.IsAvailable,
                PublishDate = book.PublishDate,
                AddedDate = book.AddedDate
            };
        }


        public async Task<bool> UpdateAsync(int id, BookUpdateDto dto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than 0.");
            }

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "DTO cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(dto.Title)) throw new ArgumentException("Title cannot be null or empty.");
            if (dto.Title.Length > 150) throw new ArgumentException("Title length cannot be greater than 150 characters.");

            if (string.IsNullOrWhiteSpace(dto.Writer)) throw new ArgumentException("Writer cannot be null or empty.");
            if (dto.Writer.Length > 100) throw new ArgumentException("Writer length cannot be greater than 100 characters.");

            if (string.IsNullOrWhiteSpace(dto.Category)) throw new ArgumentException("Category cannot be null or empty.");
            if (dto.Category.Length > 50) throw new ArgumentException("Category length cannot be greater than 50 characters.");

            if (string.IsNullOrWhiteSpace(dto.Publisher)) throw new ArgumentException("Publisher cannot be null or empty.");
            if (dto.Publisher.Length > 100) throw new ArgumentException("Publisher length cannot be greater than 100 characters.");

            if (dto.Pages <= 0) throw new ArgumentException("Page number must be greater than 0.");
            if (dto.Stock < 0) throw new ArgumentException("Stock cannot be negative.");

            var book = await _bookRepository.GetByIdAsync(id);


         
           

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} was not found.");
            }

            if (book.IsDeleted)
            {
                throw new KeyNotFoundException($"Book with ID {id} was not found.");
            }

           
            
                book.Title = dto.Title;
                book.Writer = dto.Writer;
                book.Category = dto.Category;
                book.Publisher = dto.Publisher;
                book.Pages = dto.Pages;
                book.Stock = dto.Stock;
                book.PublishDate = dto.PublishDate;


                _bookRepository.Update(book);
                await _bookRepository.SaveChangesAsync();
                return true;
          
        }
    }
}
