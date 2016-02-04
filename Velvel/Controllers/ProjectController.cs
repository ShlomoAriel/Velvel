using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Velvel.Domain.Data;
using Velvel.Domain.Managers;
using Velvel.Domain.Projects;
using Velvel.Domain.Services;
using Velvel.Domain.Users.Customers;
using Velvel.Models.Project;

namespace Velvel.Controllers
{
    [Authorize]
    public class ProjectController : AuthorizeController
    {
        private readonly IProjectService _projectService;
        private readonly Lazy<IManagerService> _managerService;
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IProjectEntityService<ProjectEntity>> _projectEntityService;
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ProjectController(IProjectService projectService, Lazy<IManagerService> managerService, Lazy<ICustomerService> customerService, Lazy<IProjectEntityService<ProjectEntity>> projectEntityService)
            : base(projectService)
        {
            _projectService = projectService;
            _managerService = managerService;
            _customerService = customerService;
            _projectEntityService = projectEntityService;
        }
        [Authorize]
        public ActionResult CustomerProjects(int customerId)
        {
            var customer = _customerService.Value.GetCustomerById(customerId);
            var model = new CustomerProjectsViewModel()
            {
                Projects = Mapper.Map<IEnumerable<ProjectViewModel>>(customer.Projects),
                CustomerId = customerId
            };
            return View(model);
        }
        // GET: Project
        public ActionResult Index()
        {
            object projects = null;
            var username = ((ClaimsIdentity)User.Identity).GetUserName();
            var roles = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (roles.Contains("Admin"))
            {
                projects = Mapper.Map<IEnumerable<ProjectViewModel>>(_projectService.GetProjects());
            }
            else if (roles.Contains("Manager"))
            {
                var manager = _managerService.Value.GetManagerByEmail(username);
                projects = Mapper.Map<IEnumerable<ProjectViewModel>>(manager.Projects);
            }
            else if (roles.Contains("Customer"))
            {
                return RedirectToAction("CustomerProjects", new {customerId=_customerService.Value.GetCustomerByEmail(username).Id});
            }

            return View(projects);
        }
        public ActionResult Create()
        {
            var model = new ProjectViewModel();
            return View(model);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        public ActionResult Create(ProjectViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                //var availableCustomers = Mapper.Map<IEnumerable<SelectListItem>>(_customerService.GetCustomers());
                //model.AvailableCustomers = availableCustomers;
                //return View(model);
            }
            _projectService.CreateProject(Mapper.Map<Project>(model));
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _projectService.Delete(id);
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Details(int projectId, int? customerId)
        {
            if (!ProjectAuthorized(projectId,customerId))
                return RedirectToAction("Index");

            var project = Mapper.Map<ProjectViewModel>(_projectService.GetProjectById(projectId));
            if (customerId != null)
            {
                project.CustomerId = customerId.Value;
                project.Customer = (_customerService.Value.GetCustomerById(customerId.Value));
            }
                

            return View(project);
        }
        public ActionResult Settings(int projectId, int? customerId)
        {
            if (!ProjectAuthorized(projectId, customerId))
                return RedirectToAction("Index");
            var project = Mapper.Map<ProjectViewModel>(_projectService.GetProjectById(projectId));
            if (customerId != null)
                project.CustomerId = customerId.Value;

            return View(project);
        }
        [Authorize]
        public ActionResult AddManager(int projectId)
        {
            if (!ProjectAuthorized(projectId))
                return RedirectToAction("Index");

            var model = new ProjectManagersViewModel
            {
                ProjectManagers =
                    Mapper.Map<IEnumerable<SelectListItem>>(_projectService.GetProjectManagers(projectId)),
                AllManagers = Mapper.Map<IEnumerable<SelectListItem>>(_managerService.Value.GetManagers()),
                ProjectId = projectId,
                AllCustomers = Mapper.Map<IEnumerable<SelectListItem>>(_customerService.Value.GetCustomers()),
                ProjectCustomers = Mapper.Map<IEnumerable<SelectListItem>>(_projectService.GetProjectCustomers(projectId))
            };
            return View(model);
        }

        public ActionResult AddManagerSubmit(ProjectManagersViewModel model)
        {
            _projectService.AddManagerToProject(model.ProjectId, model.ManagerId);

            return RedirectToAction("AddManager", new {model.ProjectId });
        }
        public ActionResult AddCustomerSubmit(ProjectManagersViewModel model)
        {
            _projectService.AddCustomerToProject(model.ProjectId, model.CustomerId);

            return RedirectToAction("AddManager", new {model.ProjectId });
        }
    }
}

