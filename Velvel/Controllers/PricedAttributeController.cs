using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Velvel.Domain.Attributes;
using Velvel.Domain.Data;
using Velvel.Domain.Services;
using Velvel.Models.Project.Attribute;

namespace Velvel.Controllers
{
    public class PricedAttributeController<T, TModel> : AttributeController<T, TModel>
        where T : AttributeEntity
        where TModel : PricedViewModel
    {
        private readonly IPricedEntityService<T> _pricedService;
        public PricedAttributeController(IPricedEntityService<T> pricedService, IProjectService projectService)
            : base(pricedService, projectService)
        {
            _pricedService = pricedService;
        }

        public override ActionResult Index(ProjectEntity entity)
        {
            if (entity.ProjectId == null)
                return null;

            var model = new PricedViewModels{
                ProjectId = entity.ProjectId.Value,
                Enteries = Mapper.Map<IEnumerable<PricedViewModel>>(_pricedService.GetProjectEntities(entity))
            };
            if (entity.CustomerId != null)
                model.CustomerId = entity.CustomerId.Value;
            return View(model);
        }

        public override ActionResult Create(ProjectEntity entity)
        {
            return View(Mapper.Map<PricedViewModel>(_pricedService.Create(entity)));
        }
        [HttpPost]
        public override ActionResult Create(TModel model)
        {
            _pricedService.Create(Mapper.Map<T>(model));
            return RedirectToAction("Settings", "Project",
                                    new
                                    {
                                        @projectId = model.ProjectId,
                                        @customerId = model.CustomerId
                                    });
        }
        public override ActionResult Edit(ProjectEntity entity)
        {
            if (entity.ProjectId != null && ProjectAuthorized(entity.ProjectId.Value,entity.CustomerId))
            {
                var model = Mapper.Map<PricedViewModel>(_pricedService.Update(entity.Id));
                if (model != null)
                    return View("Create", model);
            }

            return RedirectToAction("Settings", "Project",
                                    new
                                    {
                                        @projectId = entity.ProjectId,
                                        @customerId = entity.CustomerId
                                    });


        }
    }
}