using LibraryManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Core.DTOs
{
    public class UserLoginResultDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        


        public UserRole Role { get; set; }
    }
}
