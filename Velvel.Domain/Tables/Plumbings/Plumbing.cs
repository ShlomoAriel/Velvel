using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Velvel.Domain.Attributes;
using Velvel.Domain.Data;

namespace Velvel.Domain.Tables.Plumbings
{
    public class Plumbing : Table
    {
        public int AccessoryId { get; set; }
        public virtual Accessory Accessory { get; set; }
        [NotMapped]
        public IEnumerable<Accessory> Accessories { get; set; }

    }

    public class Accessory : AttributeEntity
    {
    }
}
