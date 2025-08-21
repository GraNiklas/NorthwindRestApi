using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private NorthwindOriginalContext db;
        public CustomersController(NorthwindOriginalContext _context)
        {
            db = _context;
        }

        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            try
            {
                var customers = db.Customers.ToList();
                return Ok(customers);

            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetOneCustomerById(string id)
        {
            try
            {
                var customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound($"Customer {id} not found.");
                }
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe: " + e);
            }
        }

        [HttpPost]
        public ActionResult CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Ok("Uusi asiakas lisätty: " + customer.CompanyName);
            }
            catch (Exception e)
            {
                return BadRequest("Errormessage: " + e.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(string id)
        {
            try
            {
                var customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound($"Asiakas id:llä {id} ei löytynyt.");
                }
                db.Customers.Remove(customer);
                db.SaveChanges();
                return Ok("Asiakas poistettu: " + customer.CompanyName);
            }
            catch (Exception e)
            {
                return BadRequest("Errormessage: " + e.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult EditCustomer(string id, [FromBody] Customer editedCustomer)
        {
            try
            {
                var customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound($"Asiakas id:llä {id} ei löytynyt.");
                }
                customer.Orders = editedCustomer.Orders;
                customer.Address = editedCustomer.Address;
                customer.Phone = editedCustomer.Phone;
                customer.ContactName = editedCustomer.ContactName;
                customer.City = editedCustomer.City;
                customer.CompanyName = editedCustomer.CompanyName;
                customer.ContactTitle = editedCustomer.ContactTitle;
                customer.Country = editedCustomer.Country;
                customer.Fax = editedCustomer.Fax;
                db.SaveChanges();
                return Ok("Asiakas muokattu id: " + customer.ContactName);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("companyname/{cname}")]
        public ActionResult GetByName(string cname)
        {
            try
            {
                var customers = db.Customers.Where(c => c.CompanyName.Contains(cname));
                return Ok(customers);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
