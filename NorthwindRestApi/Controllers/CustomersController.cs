using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        NorthwindOriginalContext db = new();

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
                if(customer == null)
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
                throw;
            }
        }
    }
}
