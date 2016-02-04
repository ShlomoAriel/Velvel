using System.Collections.Generic;
using System.Web.Mvc;
using Velvel.Models.Project.Attribute;

namespace Velvel.Models.Project.Table.Plumbing
{
    public class PlumbingViewModel:TableViewModel
    {
        public int AccessoryId { get; set; }
        public virtual AccessoryViewModel Accessory { get; set; }
        public IEnumerable<SelectListItem> Accessories { get; set; }
    }
    public class PlumbingsViewModel
    {
        public IEnumerable<PlumbingViewModel> Enteries { get; set; }
        public int ProjectId { get; set; }

        public int ?CustomerId { get; set; }
    }
}