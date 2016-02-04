using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Velvel.Domain.Attributes.Common;
using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Data;
using Velvel.Models.Project.Table.Changes;

namespace Velvel.Models.Project.Table
{
    public class TableViewModel:ProjectEntity
    {
        public DateTime? Date { get; set; }
        public int Approval { get; set; }
        public int Price { get; set; }
        
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int CommentGroupId { get; set; }
        public int MeasurementUnitId { get; set; }
        public virtual MeasurementUnit MeasurementUnit { get; set; }
        public string Description { get; set; }

        public int Quanitty { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<SelectListItem> Rooms { get; set; }
        public IEnumerable<SelectListItem> MeasurementUnits { get; set; }
    }
    public class TableEnteriesViewModel
    {
        public IEnumerable<TableViewModel> Enteries { get; set; }
        public int ProjectId { get; set; }
        public int ?CustomerId { get; set; }
    }
}