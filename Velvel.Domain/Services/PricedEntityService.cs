using System.Collections.Generic;
using System.Linq;
using Velvel.Domain.Attributes;
using Velvel.Domain.Data;
using Velvel.Domain.Projects;

namespace Velvel.Domain.Services
{
    public interface IPricedEntityService<T> : IProjectEntityService<T> where T : AttributeEntity
    {
        new IEnumerable<T> GetProjectEntities(int id);
        new T Update(int id);
        new IEnumerable<T> GetProjectEntities(ProjectEntity entity);
    }
    public class PricedEntityService<T> : ProjectEntityService<T>, IPricedEntityService<T> where T : AttributeEntity, new()
    {
        private readonly IRepository<T> _pricedEntityRepository;
        public PricedEntityService(IRepository<T> pricedEntityRepository, IRepository<Project> projectRepository)
            : base(pricedEntityRepository, projectRepository)
        {
            _pricedEntityRepository = pricedEntityRepository;
        }
        public new IEnumerable<T> GetProjectEntities(int id)
        {
            return _pricedEntityRepository.TableNoTracking.Where(x => x.ProjectId == id || x.ProjectId == null).ToList();
        }

        public new T Update(int id)
        {
            var item = _pricedEntityRepository.GetById(id);

            if (item.ProjectId == null)
                return null;

            return item;
        }

        public new IEnumerable<T> GetProjectEntities(ProjectEntity entity)
        {
            var enteries = _pricedEntityRepository.TableNoTracking.Where(x => (x.ProjectId == entity.ProjectId && x.CustomerId==null) || x.ProjectId == null && x.ProjectId == null).ToList();
            if(entity.ProjectId != null && entity.CustomerId != null)
            {
                return enteries.Concat(_pricedEntityRepository.TableNoTracking.Where(x => x.CustomerId == entity.CustomerId).ToList());
            }

            return enteries;
        }
    }
}
