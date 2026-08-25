using LibraryManagement.Core.Enums;
using System;

namespace LibraryManagement.Core.DTOs
{
    public class RentalDetailDto
    {
        public int Id { get; set; }

    
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookWriter { get; set; } = string.Empty;
        public string BookCategory { get; set; } = string.Empty;

        public int UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserIdentityCardNo { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public RentalStatus Status { get; set; }
    }
}