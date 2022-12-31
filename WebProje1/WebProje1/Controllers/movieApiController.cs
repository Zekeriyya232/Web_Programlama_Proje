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

        [HttpPut]
        public MovieDB Put([FromBody] MovieDB movie)
        {

            var movie1= _databaseContex.Movie.FirstOrDefault(a => a.Id == movie.Id);

            movie1.FilmAdi = movie.FilmAdi;
            movie1.Aciklama = movie.Aciklama;
            movie1.FilmSure= movie.FilmSure;
            movie1.FilmImg= movie.FilmImg;
            movie1.KategoriId= movie.KategoriId;
            movie1.Oyuncular= movie.Oyuncular;
            movie1.Yonetmen= movie.Yonetmen;

            _databaseContex.SaveChanges();


            return movie1;
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
        [HttpPost]
        public IActionResult post([FromBody]MovieDB movie)
        {
            var movie1 = new MovieDB();
            movie.FilmAdi = movie1.FilmAdi;
            movie.Aciklama = movie1.Aciklama;
            movie.FilmSure = movie1.FilmSure;
            movie.FilmImg = movie1.FilmImg;
            movie.KategoriId = movie1.KategoriId;
            movie.Oyuncular = movie1.Oyuncular;
            movie.Yonetmen = movie1.Yonetmen;
            if (movie1 != null)
            {
                _databaseContex.Movie.Add(movie1);
                _databaseContex.SaveChanges();
            }
            return Ok(movie1);
        }
    }
}
