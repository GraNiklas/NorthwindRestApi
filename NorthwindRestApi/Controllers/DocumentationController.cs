using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        NorthwindOriginalContext db = new();

        [HttpGet("{id}")]
        public IActionResult Documentation(int id)
        {
            var doc = db.Documentations.Find(id);
            if(doc != null)
            {
                return Ok(doc);
            }
            else
            {
                return NotFound("Dokumentaatiota ei löytynyt id:llä:" + id);
            }
        }

    }
}
