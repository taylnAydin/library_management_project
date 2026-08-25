using LibraryManagement.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Core.Entities.Concrete
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateOnly BirthdayDate { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string IdentityCardNo { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
