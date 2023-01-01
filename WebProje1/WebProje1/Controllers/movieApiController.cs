using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WebProje1.Models;
using WebProje1.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebProje1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class movieApiController : ControllerBase
    {
        
        private readonly DatabaseContex _databaseContex;
        public movieApiController(DatabaseContex databaseContex)
        {
            _databaseContex = databaseContex;
        }

        [HttpGet]
        public IEnumerable<MovieDB> Get()
        {
            return _databaseContex.Movie.ToList();
        }

        

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var movie1 = _databaseContex.Movie.FirstOrDefault(a => a.Id ==Id);
            if (movie1 != null)
            {
                _databaseContex.Movie.Remove(movie1);
                _databaseContex.SaveChanges();
                return Ok(movie1);

            }
            return BadRequest();
        }
       
    }
}
