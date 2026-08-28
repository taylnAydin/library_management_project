using LibraryManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Core.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateOnly BirthdayDate { get; set; }


        public Gender Gender { get; set; }

        public string Country { get; set; } = string.Empty;

        public string IdentityCardNo { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

    }
}
