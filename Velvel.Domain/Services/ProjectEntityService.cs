using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Velvel.Domain.Data;
using Velvel.Domain.Projects;

namespace Velvel.Domain.Services
{
    public enum AuthorityLevel { Admin = 3, Manager = 2, Customer = 1, None = 0 };
    public interface IProjectEntityService<T> : IBaseService<T> where T : ProjectEntity
    {
        new void Create(T entry);
        T Create(ProjectEntity entity);
        void Delete(ProjectEntity entity);
        T Update(ProjectEntity entity);
        IEnumerable<T> GetProjectEntities(ProjectEntity entity);
    }
    public class ProjectEntityService<T> : BaseService<T>, IProjectEntityService<T> where T : ProjectEntity, new()
    {
        private readonly IRepository<T> _projectEntityRepository;
        private readonly IRepository<Project> _projectRepository;
        public ProjectEntityService(IRepository<T> projectEntityRepository, IRepository<Project> projectRepository)
            : base(projectEntityRepository)
        {
            _projectEntityRepository = projectEntityRepository;
            _projectRepository = projectRepository;
        }
        public void Delete(ProjectEntity entity)
        {
            if (entity.ProjectId != null)
            {
                //TODO deletion history
                base.Delete(entity.Id);
            }
        }
        public new void Create(T entry)
        {
            if(entry.ProjectId != null)
                _projectEntityRepository.Insert(entry);
        }
        public T Create(ProjectEntity entity)
        {
            if (entity.ProjectId == null )
                return null;
            var project = new T
            {
                ProjectId = entity.ProjectId,
                CustomerId = entity.CustomerId
            };
            return project;
        }

        public new T Update(int id)
        {
            var item = _projectEntityRepository.GetById(id);

            if (item.ProjectId == null)
                return null;

            return item;
        }

        public T Update(ProjectEntity entity)
        {
            return entity.ProjectId != null ? null : GetById(entity.Id);
        }

        public IEnumerable<T> GetProjectEntities(ProjectEntity entity)
        {
            if (entity.ProjectId == null) 
                return null;

            var enteries = _projectEntityRepository.TableNoTracking.Where(x => (x.ProjectId == entity.ProjectId || x.ProjectId == null) && x.CustomerId == null).ToList();
            if (entity.CustomerId != null)
            {
                return enteries.Concat(_projectEntityRepository.TableNoTracking.Where(x => x.CustomerId == entity.CustomerId&&x.ProjectId==entity.ProjectId).ToList());
            }

            return enteries;
        }
    }
}
