using System.Collections.Generic;
using System.Linq;
using Velvel.Domain.Data;

namespace Velvel.Domain.Users.Customers
{
    public interface ICustomerService
    {

        IEnumerable<Customer> GetCustomers();

        void CreateCustomer(Customer customer);
        Customer GetCustomerById(int id);
        Customer GetCustomerByEmail(string email);
        void Delete(int id);
        void UpdateCustomer(Customer customer);

    }
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public IEnumerable<Customer> GetCustomers()
        {
            var result = _customerRepository.TableNoTracking.ToList();
            return result;
        }


        public void CreateCustomer(Customer customer)
        {
            _customerRepository.Insert(customer);
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _customerRepository.Table.FirstOrDefault(x => x.Email.Equals(email));
        }
        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public void Delete(int id)
        {
            var customer = GetCustomerById(id);
            if (customer != null)
                _customerRepository.Delete(customer);
        }
    }
}
