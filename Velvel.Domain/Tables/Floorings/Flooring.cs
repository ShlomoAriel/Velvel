using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Velvel.Domain.Attributes;
using Velvel.Domain.Attributes.Common;
using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Data;

namespace Velvel.Domain.Tables.Floorings
{
    public class Flooring : Table
    {
        //public int MeasurementId { get; set; }
        //public virtual Measurement Measurement { get; set; }
        public int ModelId { get; set; }
        public virtual Model Model { get; set; }

        public int GroutId { get; set; }
        public virtual Grout Grout { get; set; }

        [NotMapped]
        public IEnumerable<Grout> Grouts { get; set; }
        [NotMapped]
        public IEnumerable<Model> Models { get; set; }
        
    }

    public class Grout : AttributeEntity
    {
    }
    public class Model : AttributeEntity
    {
    }
    //public class Measurement : AttributeEntity
    //{
    //}
}
