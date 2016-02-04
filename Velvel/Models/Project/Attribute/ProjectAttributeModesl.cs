using System.Collections.Generic;
using Velvel.Domain.Data;

namespace Velvel.Models.Project.Attribute
{
    public class ProjectEntitiesViewModel
    {
        public int ProjectId { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<ProjectEntityViewModel> Enteries { get; set; }
    }
    public class ProjectEntityViewModel : ProjectEntity
    {
    }
    public class RoomViewModel : ProjectEntityViewModel
    {
    }
}