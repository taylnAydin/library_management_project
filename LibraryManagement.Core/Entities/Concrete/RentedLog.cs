using LibraryManagement.Core.Entities.Abstract;
using LibraryManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Core.Entities.Concrete
{
    public class RentedLog:BaseEntity
    {
        public int UserId { get; init; }
        public int BookId { get; init; }
        public DateOnly StartDate { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public RentalStatus Status { get; set; }

        
        public User User { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
