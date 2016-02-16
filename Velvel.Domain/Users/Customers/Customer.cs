using System.Collections.Generic;
using Velvel.Domain.Managers;
using Velvel.Domain.Projects;
using Velvel.Domain.Users.Managers;

namespace Velvel.Domain.Users.Customers
{
    public class Customer : User
    {
        public virtual ICollection<Project> Projects { get; set; }
    }
}
