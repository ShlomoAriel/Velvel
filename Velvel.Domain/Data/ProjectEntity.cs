using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Velvel.Domain.Attributes.Common;
using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Tables.Defects;

namespace Velvel.Domain.Data
{
    public class ProjectEntity : BaseEntity
    {
        public int ?CustomerId { get; set; }
        public int ?ProjectId { get; set; }
    }
    public class Table : ProjectEntity
    {
        public DateTime? Date { get; set; }
        public int Approval { get; set; }
        public int Price { get; set; }
        
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public string Description { get; set; }
        public int MeasurementUnitId { get; set; }
        public virtual MeasurementUnit MeasurementUnit { get; set; }
        public int Quanitty { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public int CommentGroupId { get; set; }
        public virtual CommentGroup CommentGroup { get; set; }

        [NotMapped]
        public IEnumerable<Comment> Comments { get; set; }
        [NotMapped]
        public IEnumerable<Room> Rooms { get; set; }
        [NotMapped]
        public IEnumerable<MeasurementUnit> MeasurementUnits { get; set; }


    }
}