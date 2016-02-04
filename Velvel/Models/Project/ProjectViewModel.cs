using System.Collections.Generic;
using System.Web.Mvc;
using Velvel.Domain.Users.Customers;

namespace Velvel.Models.Project
{
    public class CustomerProjectsViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }
        public int CustomerId { get; set; }
    }

    public class ProjectViewModel
    {
        public int Id { get; set; }
        public int ?CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string Name { get; set; }

        public bool Active { get; set; }

        public IEnumerable<SelectListItem> Managers { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }
    }
}