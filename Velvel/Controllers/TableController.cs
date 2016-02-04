using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Velvel.Domain.Data;
using Velvel.Domain.Services;
using Velvel.Models.Project.Table;

namespace Velvel.Controllers
{
    [Authorize]
    public class TableController<T, TModel> : AuthorizeController
        where T : Table
        where TModel : TableViewModel
    {
        private readonly ITableService<T> _tableService;
        protected TableController(ITableService<T> tableService, IProjectService projectService)
            : base(projectService)
        {
            _tableService = tableService;
        }
        public virtual ActionResult Index(ProjectEntity entity)
        {
            if (entity.ProjectId == null)
                return null;
            
            var model = new TableEnteriesViewModel()
            {
                ProjectId = entity.ProjectId.Value,
                Enteries = Mapper.Map<IEnumerable<TModel>>(_tableService.GetProjectEntities(entity))
            };
            if (entity.CustomerId != null)
                model.CustomerId = entity.CustomerId.Value;
            return View(model);
        }
        public virtual ActionResult Create(ProjectEntity entity)
        {
            if (entity.ProjectId == null || !ProjectAuthorized(entity.ProjectId.Value,entity.CustomerId))
            {
                return RedirectToAction("Index", "Project");
            }
            return View(Mapper.Map<TableViewModel>(_tableService.Create(entity)));
        }
        [HttpPost]
        public virtual ActionResult Create(TModel model)
        {
            if (model.ProjectId!=null&&ProjectAuthorized(model.ProjectId.Value,model.CustomerId))
                _tableService.Create(Mapper.Map<T>(model));

            return RedirectToAction("Details","Project",
                                    new{@projectId=model.ProjectId,
                                        @customerId=model.CustomerId});
        }
        public virtual ActionResult Edit(ProjectEntity entity)
        {
            //TODO RENAME METHOD
            if (entity.ProjectId == null || !ManagerAuthorized(entity.ProjectId.Value))
                return RedirectToAction("Details", "Project", new
                {
                    @projectId = entity.ProjectId,
                    @customerId = entity.CustomerId
                });

            var model = Mapper.Map<TModel>(_tableService.Update(entity));
            if (model == null)
                return RedirectToAction("Details", "Project", new
                {
                    @projectId = entity.ProjectId,
                    @customerId = entity.CustomerId
                });
            return View("Create", model);
        }
        public ActionResult Delete(ProjectEntity entity)
        {
            _tableService.Delete(entity);
            return RedirectToAction("Details", "Project",
                                   new
                                   {
                                       @projectId = entity.ProjectId,
                                       @customerId = entity.CustomerId
                                   });
        }

        public ActionResult AddComment(Comment comment)
        {
            //comment.UserId = int.Parse(User.Identity.GetUserId());
            _tableService.AddComment(comment);
            return Json(comment);
        }
    }
}