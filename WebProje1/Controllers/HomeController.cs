using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using WebProje1.Entity;
using WebProje1.Models;

namespace WebProje1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContex _databaseContex;
        public HomeController(DatabaseContex databaseContex, ILogger<HomeController> logger)
        {
            _databaseContex = databaseContex;
            _logger = logger;
        }

       

        public IActionResult Index()
        {
            List<MovieDB> filmler = _databaseContex.Movie.ToList();
            List<MovieVM> model = new List<MovieVM>();

            foreach (MovieDB item in filmler )
            {
                model.Add(new MovieVM
                {
                    Id = item.Id,
                    FilmAdi = item.FilmAdi,
                    Aciklama = item.Aciklama,
                    KategoriId = item.KategoriId,
                    FilmSure = item.FilmSure,
                    Yonetmen = item.Yonetmen,
                    filmImg = item.FilmImg,
                    oyuncular=item.Oyuncular
                    
                });
            }
            return View(model);
        }

        public IActionResult GenreList(int genreId)
        {

            List<MovieVM> movies;
            movies = (from x in _databaseContex.Movie
                      where genreId == x.KategoriId
                      select new MovieVM() { Id = x.Id, FilmAdi = x.FilmAdi, Aciklama = x.Aciklama, KategoriId = x.KategoriId, FilmSure = x.FilmSure, Yonetmen = x.Yonetmen, filmImg = x.FilmImg }).ToList();
            return View("Index", movies);

        }

        public IActionResult MovieDetail(int id)
        {
            MovieDB movie = _databaseContex.Movie.FirstOrDefault(x => x.Id == id);
            if(movie != null)
            {
                List<CommentVM> comments;
                MovieVM movie1 = new MovieVM();
                movie1.Id = movie.Id;
                movie1.KategoriId = movie.KategoriId;
                movie1.FilmAdi = movie.FilmAdi;
                movie1.Aciklama = movie.Aciklama;
                movie1.FilmSure = movie.FilmSure;
                movie1.Yonetmen = movie.Yonetmen;
                movie1.filmImg = movie.FilmImg;
                movie1.oyuncular = movie.Oyuncular;

                comments = (from y in _databaseContex.comments where id == y.filmId select new CommentVM() { Id = y.Id, comment = y.comment, filmId = y.filmId, kullaniciId = y.kullaniciId }).ToList();

                string id2 = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int intId = Convert.ToInt32(id2);


                if (User.Identity.IsAuthenticated)
                {
                    KullaniciDB kullaniciDB = _databaseContex.Kullanici.FirstOrDefault(z => z.Id == intId);
                    KullaniciVM kullanici = new KullaniciVM();

                    kullanici.Id = kullaniciDB.Id;
                    kullanici.kullaniciAdi = kullaniciDB.kullaniciAdi;
                    kullanici.KullaniciSoyadi = kullaniciDB.KullaniciSoyadi;
                    kullanici.kullaniciEmail = kullaniciDB.kullaniciEmail;
                    kullanici.KayitTarih = kullaniciDB.KayitTarih;
                    //kullanici. = kullaniciDB.ProfilImg;
                    kullanici.Phone = kullaniciDB.Phone;

                    ViewBag.kullanici = kullanici;
                }
                

                ViewBag.comments = comments;
                ViewBag.movies = movie1;
               
                

                return View(new CommentVM());

            }
            else
            {
                return  RedirectToAction("Index");

            }
           
        }

       
        public IActionResult AddComment(CommentVM commentVM)
        {
            if (ModelState.IsValid)
            {
                CommentDB commentDB = new CommentDB();
                commentDB.comment = commentVM.comment;
                commentDB.Id = commentVM.Id;
                commentDB.filmId = commentVM.filmId;
                commentDB.kullaniciId = commentVM.kullaniciId;

                _databaseContex.Add(commentDB);
                _databaseContex.SaveChanges();

                return RedirectToAction("MovieDetail", commentVM.kullaniciId);
            }

            return View("MovieDetail");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}