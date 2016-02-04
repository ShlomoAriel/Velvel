using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using AutoMapper;
using Velvel.Domain.Data;
using Velvel.Domain.Services;
using Velvel.Domain.Tables.Floorings;
using Velvel.Models.Project.Table.Floorings;

namespace Velvel.Controllers
{
    [Authorize]
    public class FlooringController : TableController<Flooring,FlooringViewModel>
    {
        private readonly IFlooringService _flooringService;

        public FlooringController(ITableService<Flooring> tableService, IFlooringService flooringService, IProjectService projectService)
            : base(tableService, projectService)
        {
            _flooringService = flooringService;
        }
        public override ActionResult Index(ProjectEntity entity)
        {
            if (entity.ProjectId == null)
                return null;

            var model = new FlooringsViewModel()
            {
                ProjectId = entity.ProjectId.Value,
                Enteries = Mapper.Map<IEnumerable<FlooringViewModel>>(_flooringService.GetProjectEntities(entity))
            };
            if (entity.CustomerId != null)
                model.CustomerId = entity.CustomerId.Value;

            return View(model);
        }
        //public override ActionResult Create(int id)
        //{
        //    return View(Mapper.Map<FlooringViewModel>(_flooringService.Create(id)));
        //}
        public override ActionResult Create(ProjectEntity entity)
        {
            return View(Mapper.Map<FlooringViewModel>(_flooringService.Create(entity)));
        }
        [HttpPost]
        public override ActionResult Create(FlooringViewModel model)
        {
            _flooringService.Create(Mapper.Map<Flooring>(model));
            return RedirectToAction("Details", "Project",
                                    new
                                    {
                                        @projectId = model.ProjectId,
                                        @customerId = model.CustomerId
                                    });
        }
        public override ActionResult Edit(ProjectEntity entity)
        {
            var model = Mapper.Map<FlooringViewModel>(_flooringService.Update(entity));
            if (model == null)
                return RedirectToAction("Details", "Project",
                                    new
                                    {
                                        @projectId = entity.ProjectId,
                                        @customerId = entity.CustomerId
                                    });

            return View("Create", model);
        }
    }
}