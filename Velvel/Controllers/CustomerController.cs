using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Velvel.Domain.Users.Customers;
using Velvel.Models;

namespace Velvel.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Lazy<ICustomerService> _customerService;

        public CustomerController(Lazy<ICustomerService> customerService)
        {
            _customerService = customerService;
        }

        // GET: Customer
        public ActionResult Index()
        {
            var customers = _customerService.Value.GetCustomers().ToList();
            return View(customers);
        }
        public ActionResult Create()
        {
            var model = new CustomerViewModel();
            return View(model);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        public ActionResult Create(CustomerViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                //var availableCustomers = Mapper.Map<IEnumerable<SelectListItem>>(_customerService.GetCustomers());
                //model.AvailableCustomers = availableCustomers;
                //return View(model);
            }
            _customerService.Value.CreateCustomer(Mapper.Map<Customer>(model));
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _customerService.Value.Delete(id);
            return RedirectToAction("Index");
        }
    }
}