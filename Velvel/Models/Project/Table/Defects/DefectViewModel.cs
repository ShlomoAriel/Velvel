using System.Collections.Generic;

namespace Velvel.Models.Project.Table.Defects
{
    public class DefectViewModel:TableViewModel
    {
    }
    public class DefectsViewModel
    {
        public IEnumerable<DefectViewModel> Defects { get; set; }
        public int ProjectId { get; set; }
    }
}