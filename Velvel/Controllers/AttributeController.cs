using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Velvel.Domain.Data;
using Velvel.Domain.Services;
using Velvel.Models.Project.Attribute;

namespace Velvel.Controllers
{
    public class AttributeController<T,TModel> : AuthorizeController where T:ProjectEntity where TModel : ProjectEntityViewModel
    {
         private readonly IProjectEntityService<T> _baseService;
         protected AttributeController(IProjectEntityService<T> baseService, IProjectService projectService)
             : base(projectService)
         {
            _baseService = baseService;
        }
         public virtual ActionResult Index(ProjectEntity entity)
         {
             if (entity.ProjectId == null || ProjectAuthorized(entity.ProjectId.Value,entity.CustomerId))
                 return null;

             var model = new ProjectEntitiesViewModel
             {
                 ProjectId = entity.ProjectId.Value,
                 Enteries = Mapper.Map<IEnumerable<ProjectEntityViewModel>>(_baseService.GetProjectEntities(entity))
             };
             if (entity.CustomerId != null)
                 model.CustomerId=entity.CustomerId.Value;


             //TODO this says RoomViewModel is not of type ProjectEntityViewModel
            //model.Enteries = Mapper.Map<IEnumerable<TModel>>(_baseService.GetProjectEntities(id));


            return View(model);
        }
         public virtual ActionResult Create(ProjectEntity entity)
         {
             if(entity.ProjectId == null || !ProjectAuthorized(entity.ProjectId.Value,entity.CustomerId))
             {
                 return RedirectToAction("Settings", "Project",
                                    new
                                    {
                                        @projectId = entity.ProjectId,
                                        @customerId = entity.CustomerId
                                    });
             }
             return View(Mapper.Map<ProjectEntityViewModel>(_baseService.Create(entity)));
        }
        [HttpPost]
        public virtual ActionResult Create(TModel model)
        {
            if (model.ProjectId == null || !ProjectAuthorized(model.ProjectId.Value))
            {
                _baseService.Create(Mapper.Map<T>(model));
            }
            return RedirectToAction("Settings", "Project",
                                    new
                                    {
                                        @projectId = model.ProjectId,
                                        @customerId = model.CustomerId
                                    });
        }
        public virtual ActionResult Edit(ProjectEntity entity)
        {
            if (entity.ProjectId != null && ProjectAuthorized(entity.ProjectId.Value,entity.CustomerId))
            {
                var model = Mapper.Map<ProjectEntityViewModel>(_baseService.Update(entity.Id));
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
        public ActionResult Delete(ProjectEntity entity)
        {
            if(entity.ProjectId != null && ProjectAuthorized(entity.ProjectId.Value,entity.CustomerId))
                _baseService.Delete(entity);

            return RedirectToAction("Settings", "Project",
                                   new
                                   {
                                       @projectId = entity.ProjectId,
                                       @customerId = entity.CustomerId
                                   });
        }
    }
}