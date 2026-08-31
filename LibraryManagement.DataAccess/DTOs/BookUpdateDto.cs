using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.DTOs
{
    public class BookUpdateDto
    {
       
        public string Title { get; set; } = string.Empty;
        public string Writer { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateOnly PublishDate { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public int Pages { get; set; }

    }
}
