using LibraryManagement.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Core.Entities.Concrete
{
    public class Book : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Writer { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateOnly PublishDate { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public DateOnly AddedDate { get; set; }
        public int Pages { get; set; }
    }
}
