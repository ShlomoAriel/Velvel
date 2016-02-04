using System.Collections.Generic;
using System.Linq;
using Velvel.Domain.Data;
using Velvel.Domain.Users.Managers;

namespace Velvel.Domain.Managers
{
    public interface IManagerService
    {

        IEnumerable<Manager> GetManagers();

        void CreateManager(Manager manager);
        Manager GetManagerById(int id);
        Manager GetManagerByEmail(string email);
        void Delete(int id);
        void UpdateManager(Manager manager);

    }
    public class ManagerService : IManagerService
    {
        private readonly IRepository<Manager> _managerRepository;
        public ManagerService(IRepository<Manager> customerRepository)
        {
            _managerRepository = customerRepository;
        }
        public IEnumerable<Manager> GetManagers()
        {
            var result = _managerRepository.TableNoTracking.ToList();
            return result;
        }


        public void CreateManager(Manager manager)
        {
            _managerRepository.Insert(manager);
        }

        public Manager GetManagerById(int id)
        {
            return _managerRepository.GetById(id);
        }

        public Manager GetManagerByEmail(string email)
        {
            return _managerRepository.Table.FirstOrDefault(x => x.Email.Equals(email));
        }
        public void UpdateManager(Manager manager)
        {
            _managerRepository.Update(manager);
        }

        public void Delete(int id)
        {
            var manager = GetManagerById(id);
            if (manager != null)
                _managerRepository.Delete(manager);
        }
    }
}
