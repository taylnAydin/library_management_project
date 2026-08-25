using LibraryManagement.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Core.Entities.Concrete
{
    public class RentedLog:BaseEntity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
