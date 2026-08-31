using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.DTOs
{
    public class RentalCreateDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateOnly StartDate { get; set; }
        
    }
}
