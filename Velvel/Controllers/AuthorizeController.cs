using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.VisualBasic.ApplicationServices;
using Velvel.Domain.Data;
using Velvel.Domain.Projects;
using Velvel.Domain.Services;

namespace Velvel.Controllers
{
    public enum AuthorityLevel { Admin = 3, Manager = 2, Customer = 1, None = 0 };
    public class AuthorizeController : Controller
    {
        private readonly IProjectService _projectService;

        public AuthorizeController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public bool IsProjectAuthorized(int projectId)
        {
            return false;
        }
        public AuthorityLevel ProjectAuthorizeLevel(int projectId)
        {
            var currentUser =User;
            var authorityLevel = AuthorityLevel.None;

            var project = _projectService.GetProjectById(projectId);

            if (project == null) return authorityLevel;

            var username = ((ClaimsIdentity)currentUser.Identity).GetUserName();


            if (currentUser.IsInRole("Admin"))
            {
                authorityLevel = AuthorityLevel.Admin;
            }
            else if (currentUser.IsInRole("Manager"))
            {
                authorityLevel = project.Managers.Any(manager => manager.Email.Equals(username))
                    ? AuthorityLevel.Manager : AuthorityLevel.None;
            }
            else if (currentUser.IsInRole("Customer"))
            {
                authorityLevel = project.Customers.Any(customer => customer.Email.Equals(username))
                    ? AuthorityLevel.Customer : AuthorityLevel.None;
            }

            return authorityLevel;
        }

        public bool ManagerAuthorized(int projectId)
        {
            return ProjectAuthorizeLevel(projectId) == AuthorityLevel.Manager || ProjectAuthorizeLevel(projectId) == AuthorityLevel.Admin;
        }

        public bool CustomerAuthorized(int projectId)
        {
            return ProjectAuthorizeLevel(projectId) == AuthorityLevel.Customer;
        }
        public bool ProjectAuthorized(int projectId)
        {
            return ProjectAuthorizeLevel(projectId) != AuthorityLevel.None;
        }

        public bool ProjectAuthorized(int projectId,int ?customerId)
        {
            if (customerId != null && !CustomerValid(projectId, customerId.Value))
                return false;
            return ProjectAuthorizeLevel(projectId) != AuthorityLevel.None;
        }

        public bool CustomerValid(int projectId, int customerId)
        {
            var project = _projectService.GetProjectById(projectId);
            return project != null && project.Customers.Any(x => x.Id == customerId);
        }
    }
}