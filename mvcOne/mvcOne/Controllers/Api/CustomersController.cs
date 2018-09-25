using mvcOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mvcOne.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context=new ApplicationDbContext();
        }
        // Get api/customers
        public IEnumerable<Customer> GetCustomers() {
            return _context.Customer.ToList();        
        }
        // Get api/customers/1
        public Customer GetCustomer(int id)
        {
            var customer= _context.Customer.SingleOrDefault(c=>c.Id==id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return customer;
        }

        //Post api/customers
        [HttpPost]
        public Customer CreatCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                _context.Customer.Add(customer);
                _context.SaveChanges();
                return customer;
            }
        }

        //Put api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id,Customer customer) 
        {
            if (!ModelState.IsValid)
            
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = _context.Customer.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Name = customer.Name;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsLatter = customer.IsSubscribedToNewsLatter;
            customerInDb.BirthDate = customer.BirthDate;
            _context.SaveChanges();
        }

        //Delet api/customers/1
        [HttpDelete]
        public void DeletCustomer(int id) 
        {
            var customerInDb = _context.Customer.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customer.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
