using System.Collections.Generic;
using System.Linq;
using Velvel.Domain.Data;
using Velvel.Domain.Managers;
using Velvel.Domain.Projects;
using Velvel.Domain.Users.Customers;
using Velvel.Domain.Users.Managers;

namespace Velvel.Domain.Services
{
    public interface IProjectService
    {

        IEnumerable<Project> GetProjects();
        IEnumerable<Project> GetActiveProjects();
        void CreateProject(Project project);

        void AddManagerToProject(int projectId, int managerId);

        void AddCustomerToProject(int projectId, int customerId);

        Project GetProjectById(int id);
        void Delete(int id);
        void UpdateProject(Project project);

        IEnumerable<Project> GetAvaliableProjects();
        //object GetCustomerProjects(int projectId, int customerId);

        bool ManagerAuthorized(int ?managerId , int projectId);

        object GetProjectManagers(int projectId);

        object GetProjectCustomers(int projectId);
    }
    public class ProjectService : IProjectService
    {
        private readonly ProjectRepository _projectRepository;
        private readonly IRepository<Manager> _managerRepository;
        private readonly IRepository<Customer> _customerRepository;

        //public object GetCustomerProjects(int projectId, int customerId)
        //{
        //    return _projectRepository.TableNoTracking.Where(x => x.CustomerId.Equals(customerId) || x.ProjectId.Equals(projectId)).ToList();
        //}

        public ProjectService(ProjectRepository projectRepository, IRepository<Manager> managerRepository, IRepository<Customer> customerRepository)
        {
            _projectRepository = projectRepository;
            _managerRepository = managerRepository;
            _customerRepository = customerRepository;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _projectRepository.TableNoTracking.ToList();
        }

        //public object GetProjectProjects(int id)
        //{
        //    return _projectRepository.TableNoTracking.Where(x => x.ProjectId.Equals(id)).ToList();
        //}
        public IEnumerable<Project> GetActiveProjects()
        {
            return _projectRepository.Table.ToList();
        }

        public void CreateProject(Project project)
        {
            _projectRepository.Insert(project);
        }

        public void AddManagerToProject(int projectId, int managerId)
        {
            var manager = _managerRepository.GetById(managerId);
            if (manager != null)
            {
                _projectRepository.AddManager(projectId, manager);
            }
            
        }

        public void AddCustomerToProject(int projectId, int customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer != null)
            {
                _projectRepository.AddCustomer(projectId, customer);
            }
            
        }


        public Project GetProjectById(int id)
        {
            return _projectRepository.GetById(id);
        }

        public void UpdateProject(Project project)
        {
            _projectRepository.Update(project);
        }


        public IEnumerable<Project> GetAvaliableProjects()
        {
            //TODO:get only related
            return _projectRepository.TableNoTracking.ToList();
        }

        public bool ManagerAuthorized(int ?managerId, int projectId)
        {
            var project = _projectRepository.GetById(projectId);
            return project.Managers.Any(manager => manager.Id.Equals(managerId));
        }

        public object GetProjectManagers(int projectId)
        {
            return _projectRepository.GetById(projectId).Managers;
        }

        public object GetProjectCustomers(int projectId)
        {
            return _projectRepository.GetById(projectId).Customers;
        }

        public void Delete(int id)
        {
            var project = GetProjectById(id);
            if (project != null)
                _projectRepository.Delete(project);
        }
    }
}
