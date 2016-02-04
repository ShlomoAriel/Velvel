using System.Collections.Generic;

namespace Velvel.Models.Project.Table.Changes
{
    public class ChangeViewModel : TableViewModel
    {

    }
    public class ChangesViewModel 
    {
        public IEnumerable<ChangeViewModel> Changes { get; set; }
        public int ProjectId { get; set; }
    }
}