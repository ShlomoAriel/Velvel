using System.Web.Mvc;
using Velvel.Domain.Services;
using Velvel.Domain.Tables.Defects;
using Velvel.Models.Project.Table;

namespace Velvel.Controllers
{
    [Authorize]
    public class DefectController : TableController<Defect,TableViewModel>
    {
        public DefectController(ITableService<Defect> tableService, IProjectService projectService)
            : base(tableService, projectService)
        {
        }
    }
}