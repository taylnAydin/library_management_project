using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Core.Entities.Abstract
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
