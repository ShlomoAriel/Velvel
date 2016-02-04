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
        public  ICollection<Manager> Managers { get; set; }
        public  ICollection<Customer> Customers { get; set; }
    }
}
