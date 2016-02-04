using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using AutoMapper;
using Velvel.Domain.Data;
using Velvel.Domain.Services;
using Velvel.Domain.Tables.Plumbings;
using Velvel.Models.Project.Table;
using Velvel.Models.Project.Table.Plumbing;

namespace Velvel.Controllers
{
    [Authorize]
    public class PlumbingController : TableController<Plumbing, PlumbingViewModel>
    {
        private readonly IPlumbingService _plumbingService;

        public PlumbingController(IPlumbingService plumbingService, IProjectService projectService)
            : base(plumbingService, projectService)
        {
            _plumbingService = plumbingService;
        }

        public override ActionResult Index(ProjectEntity entity)
        {
            if (entity.ProjectId == null)
                return null;
            var model = new PlumbingsViewModel()
            {
                ProjectId = entity.ProjectId.Value,
                Enteries = Mapper.Map<IEnumerable<PlumbingViewModel>>(_plumbingService.GetProjectEntities(entity))
            };
            if (entity.CustomerId != null)
                model.CustomerId = entity.CustomerId.Value;
            return View(model);
        }
        public override ActionResult Create(ProjectEntity entity)
        {
            return View(Mapper.Map<PlumbingViewModel>(_plumbingService.Create(entity)));
        }
        [HttpPost]
        public override ActionResult Create(PlumbingViewModel model)
        {
            _plumbingService.Create(Mapper.Map<Plumbing>(model));
            return RedirectToAction("Details", "Project", new { @projectId = model.ProjectId, customerId=model.CustomerId });
        }
        public override ActionResult Edit(ProjectEntity entity)
        {
            var model = Mapper.Map<PlumbingViewModel>(_plumbingService.Update(entity));
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