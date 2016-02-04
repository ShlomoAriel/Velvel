using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Velvel.Models.Project
{
    public class ProjectManagersViewModel
    {
        public int ProjectId { get; set; }
        public int ManagerId { get; set; }
        public IEnumerable<SelectListItem> AllManagers { get; set; }
        public IEnumerable<SelectListItem> ProjectManagers { get; set; }

        public int CustomerId { get; set; }
        public IEnumerable<SelectListItem> AllCustomers { get; set; }
        public IEnumerable<SelectListItem> ProjectCustomers { get; set; }
    }
}