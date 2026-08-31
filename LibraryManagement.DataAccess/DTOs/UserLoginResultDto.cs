using LibraryManagement.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.DTOs
{
    public class UserLoginResultDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        


        public UserRole Role { get; set; }
    }
}
