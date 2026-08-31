using LibraryManagement.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.DTOs
{
    public class RentalListDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;

        public string UserIdentityCardNo { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }
        public DateOnly DueDate { get; set; }

        public DateOnly? ReturnDate { get; set; }

        public RentalStatus Status { get; set; }
    }
}
