using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.DataAccess.Entities.Abstract
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; init; }
        public bool IsDeleted { get; set; }
    }
}
