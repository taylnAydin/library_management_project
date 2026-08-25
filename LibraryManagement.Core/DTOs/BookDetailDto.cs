using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Core.DTOs
{
    public class BookDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Writer { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateOnly PublishDate { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public DateOnly AddedDate { get; set; }
        public int Pages { get; set; }
        public bool IsAvailable { get; set; }

    }
}
