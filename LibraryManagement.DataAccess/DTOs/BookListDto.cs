using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.DTOs
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Writer { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }

        public bool IsAvailable { get; set; }
    }
}
