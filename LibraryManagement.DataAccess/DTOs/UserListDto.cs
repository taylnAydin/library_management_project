using LibraryManagement.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.DTOs
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string IdentityCardNo { get; set; } = string.Empty;

        public UserRole Role { get; set; }
    }
}