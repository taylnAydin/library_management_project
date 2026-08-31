using LibraryManagement.DataAccess.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.Entities.Concrete
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Writer { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateOnly PublishDate { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public DateOnly AddedDate { get; set; }
        public int Pages { get; set; }

        public bool IsAvailable => Stock > 0; // computed property
    }
}
