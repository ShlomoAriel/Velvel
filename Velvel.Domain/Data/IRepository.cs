using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Velvel.Domain.Managers;
using Velvel.Domain.Projects;
using Velvel.Domain.Users;
using Velvel.Domain.Users.Customers;
using Velvel.Domain.Users.Managers;

namespace Velvel.Domain.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }
    public interface IProjectRepository : IRepository<Project>
    {
        //IEnumerable<Project> GetRecentCustomers();
        //Project GetByName(string customerName);
        void AddManager(int projectId, Manager manager);
        void AddCustomer(int projectId, Customer customer);
    }
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddManager(int projectId, Manager manager)
        {
            var project = GetById(projectId);
            if (!project.Managers.Any())
            {
                project.Managers = new List<Manager>();
            }
            project.Managers.Add(manager);
            _dbContext.SaveChanges();
        }
        public void AddCustomer(int projectId, Customer customer)
        {
            var project = GetById(projectId);
            if (!project.Customers.Any())
            {
                project.Customers = new List<Customer>();
            }
            project.Customers.Add(customer);
            _dbContext.SaveChanges();
        }
    }
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(object id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            if (entity != null)
            {
                if (entity.Id > 0)
                {
                    Update(entity);
                }
                else
                {
                    _dbContext.Set<T>().Add(entity);
                    _dbContext.SaveChanges();
                }
                
            }
        }

        public void Insert(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var original = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(original).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _dbContext.Set<T>().Remove(entity);

                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    _dbContext.Set<T>().Remove(entity);

                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        //public IQueryable<Manager> Table { get; private set; }
        //public IQueryable<Manager> TableNoTracking { get; private set; }

        public IEnumerable<T> FindAll()
        {
            return _dbContext.Set<T>().ToList();
        }
        public T FindById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public virtual IQueryable<T> Table
        {
            get
            {
                return _dbContext.Set<T>();
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return _dbContext.Set<T>().AsNoTracking();
            }
        }
    }
}
