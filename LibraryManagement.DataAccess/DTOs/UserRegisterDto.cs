using LibraryManagement.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.DTOs
{
    public class UserRegisterDto
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateOnly BirthdayDate { get; set; }
        public Gender Gender { get; set; }
        public string Country { get; set; } = string.Empty;
        public string IdentityCardNo { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }
}
