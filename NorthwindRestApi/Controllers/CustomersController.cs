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
            var customers = db.Customers.ToList();
            return Ok(customers);
        }
    }
}
