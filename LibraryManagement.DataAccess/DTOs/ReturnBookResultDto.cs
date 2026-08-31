using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.DTOs
{
    public class ReturnBookResultDto
    {
        public bool Success { get; set; }
        public bool IsLate { get; set; }
        public int LateDays { get; set; }
    }
}
