using System.Collections.Generic;
using Velvel.Domain.Data;
using Velvel.Domain.Projects;

namespace Velvel.Domain.Users.Managers
{
    public abstract class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public  ICollection<Project> Projects { get; set; }
    }
    public class Manager : User
    {
    }
}
