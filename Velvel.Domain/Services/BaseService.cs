using System.Collections.Generic;
using System.Linq;
using Velvel.Domain.Data;

namespace Velvel.Domain.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        void Create(T changes);
        IEnumerable<T> Get();
        T GetById(int id);
        void Update(T changes);
        T Update(int id);
        void Delete(int id);
    }
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _baseRepository;

        public BaseService(IRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public void Update(T changes)
        {
            _baseRepository.Update(changes);
        }

        public T Update(int id)
        {
            return GetById(id);
        }

        public void Delete(int id)
        {
            var changes = GetById(id);
            if (changes != null)
                _baseRepository.Delete(changes);
        }
        public IEnumerable<T> Get()
        {
            return _baseRepository.TableNoTracking.ToList();
        }
        public void Create(T entry)
        {
            _baseRepository.Insert(entry);
        }


        public T GetById(int id)
        {
            return _baseRepository.GetById(id); ;
        }
    }

}
