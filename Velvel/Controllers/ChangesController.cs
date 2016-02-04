using System.Web.Mvc;
using Velvel.Domain.Tables.ChangesAndAdditions;
using Velvel.Domain.Data;
using Velvel.Domain.Services;
using Velvel.Domain.Tables;
using Velvel.Models.Project.Table;

namespace Velvel.Controllers
{
    [Authorize]
    public class ChangesController : TableController<Changes,TableViewModel>
    {
        private readonly ITableService<Changes> _tableService;

        public ChangesController(ITableService<Changes> tableService, IProjectService projectService)
            : base(tableService, projectService)
        {
            _tableService = tableService;
        }

        // GET: Changes
        
        //[HttpPost]
        //public ActionResult Create(ChangeViewModel model)
        //{
        //    model.Identity = ((ClaimsIdentity) User.Identity);
        //    if (User.IsInRole("Admin") || User.IsInRole("Manager"))
        //    {
        //        model.StatusId = 2;
        //    }
        //    else if (User.IsInRole("Customer"))
        //    {
        //        model.StatusId = 1;
        //    }
        //    model.Date = DateTime.Now;
        //    _projectEntityService.Create(Mapper.Map<Changes>(model));
        //    //_changesService.Value.CreateChanges(Mapper.Map<Changes>(model));
        //    return RedirectToAction("Details","Project",new{@id=model.ProjectId});
        //}

        //[HttpPost]
        //public ActionResult Edit(ChangesViewModel model)
        //{
        //    _projectEntityService.Create(Mapper.Map<Changes>(model));
        //    //_changesService.Value.CreateChanges(Mapper.Map<Changes>(model));
        //    return RedirectToAction("Index");
        //}
        //public ActionResult Delete(int id, int projectId)
        //{
        //    _projectEntityService.Delete(id);
        //    //_changesService.Value.Delete(id);
        //    return RedirectToAction("Details","Project",new{id=projectId});
        //}
    }
}