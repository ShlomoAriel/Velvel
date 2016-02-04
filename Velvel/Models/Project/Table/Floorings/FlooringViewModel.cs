using System.Collections.Generic;
using System.Web.Mvc;
using Velvel.Models.Project.Attribute;

namespace Velvel.Models.Project.Table.Floorings
{
    public class FlooringViewModel:TableViewModel
    {
        public int GroutId { get; set; }
        public virtual  GroutViewModel Grout { get; set; }
        public IEnumerable<SelectListItem> Grouts { get; set; }

        //public int MeasurementId { get; set; }
        //public virtual MeasurementViewModel Measurement { get; set; }
        //public IEnumerable<SelectListItem> Measurements { get; set; }

        public int ModelId { get; set; }
        public virtual ModelViewModel Model { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }

    }
    public class FlooringsViewModel
    {
        public IEnumerable<FlooringViewModel> Enteries { get; set; }
        public int ProjectId { get; set; }

        public int ?CustomerId { get; set; }
    }
    
}