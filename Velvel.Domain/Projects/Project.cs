using System.Collections.Generic;
using Velvel.Domain.Data;
using Velvel.Domain.Managers;
using Velvel.Domain.Users.Customers;
using Velvel.Domain.Users.Managers;

namespace Velvel.Domain.Projects
{
    public class Project : BaseEntity
    {
        public bool Active { get; set; }
        public virtual ICollection<Manager> Managers { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
