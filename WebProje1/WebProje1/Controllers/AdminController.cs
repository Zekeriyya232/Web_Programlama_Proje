using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using WebProje1.Controllers;
using WebProje1.Entity;
using WebProje1.Models;

namespace WebProje1.Controllers
{
    [Authorize(Roles ="admin")]   //roles="admin,manager,user"
    public class AdminController : Controller
    {
        private readonly DatabaseContex _databaseContex;
        private readonly IConfiguration _configuration;

        public AdminController(DatabaseContex databaseContex, IConfiguration configuration)
        {
            _databaseContex = databaseContex;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            List<KullaniciDB> kullanicilar =_databaseContex.Kullanici.ToList();
            List<KullaniciVM> model = new List<KullaniciVM>();

           // _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
           //   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

            foreach (KullaniciDB item in kullanicilar)
            {
                model.Add(new KullaniciVM {
                    Id = item.Id,
                    kullaniciAdi=item.kullaniciAdi,
                    KullaniciSoyadi=item.KullaniciSoyadi,
                    kullaniciEmail=item.kullaniciEmail,
                    Role=item.Role,
                    KayitTarih=item.KayitTarih,
                    Locked=item.Locked,
                    Phone=item.Phone,
                });
            }
            //admin sayfasında kullanıcıları listelemek için kullan 
            

            return View(model);
        }


        public IActionResult MoviesList()
        {
            movieApiControllerResponse PAR = new();
            List<MovieDB> movies = PAR.GetAllMovies().Result;
            List<MovieVM> model2 = new List<MovieVM>();
            foreach (MovieDB item in movies)
            {
                model2.Add(new MovieVM
                {
                    Id = item.Id,
                    FilmAdi = item.FilmAdi,
                    Aciklama = item.Aciklama,
                    KategoriId = item.KategoriId,
                    FilmSure = item.FilmSure,
                    Yonetmen = item.Yonetmen,
                    filmImg = item.FilmImg
                });
            }

            return View(model2);
        }

        
        public IActionResult GenreList(string genreId)
        {
            int intId = Convert.ToInt32(genreId);
            List<MovieVM> movies;
            movies =(from x in _databaseContex.Movie where intId==x.KategoriId 
                     select new MovieVM(){Id=x.Id,FilmAdi=x.FilmAdi,Aciklama=x.Aciklama,KategoriId=x.KategoriId,FilmSure=x.FilmSure,Yonetmen=x.Yonetmen,filmImg=x.FilmImg}).ToList();
            return View("MoviesList",movies);

        }

        public IActionResult DeleteMovies(string fileName)
        {
            string temp = fileName;
            movieApiControllerResponse PAR = new();
            PAR.Delete(int.Parse(temp));

            return View("MovieList");
        }

        public IActionResult EditMovie(string fileName)
        {
            string temp = fileName;
            MovieDB movieDB = _databaseContex.Movie.FirstOrDefault(x=>x.Id==int.Parse(temp));
            return View(movieDB);
        }

        [HttpPost]
        public IActionResult EditMovie(MovieDB movieDB)
        {
            ModelState.Remove("Id");
            ModelState.Remove("FilmAdi");
            ModelState.Remove("KategoriId");
            ModelState.Remove("FilmSure");
            ModelState.Remove("Aciklama");
            ModelState.Remove("Oyuncular");
            ModelState.Remove("Yonetmen");
            ModelState.Remove("FilmImg");

            string movieId = (movieDB.Id).ToString();
            if (ModelState.IsValid)
            {
                movieApiControllerResponse PAR = new();
                PAR.Update(movieDB);
                return View("MovieList");
            }
            return View(movieDB);


        }
        [HttpPost]
        public IActionResult CreateMovie(MovieVM model)
        {
            if (ModelState.IsValid)
            {
                MovieDB movie = new MovieDB
                {
                    FilmAdi = model.FilmAdi,
                    Yonetmen = model.Yonetmen,
                    Aciklama = model.Aciklama,
                    FilmSure = model.FilmSure,
                    KategoriId = model.KategoriId,
                    FilmImg = model.filmImg

                };
                _databaseContex.Add(movie);
                _databaseContex.SaveChanges();
                List<MovieDB> filmlist = _databaseContex.Movie.ToList();
                List<MovieVM> model2 = new List<MovieVM>();

                // _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
                //   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

                foreach (MovieDB item in filmlist)
                {
                    model2.Add(new MovieVM
                    {
                        Id = item.Id,
                        FilmAdi = item.FilmAdi,
                        Aciklama = item.Aciklama,
                        KategoriId = item.KategoriId,
                        FilmSure = item.FilmSure,
                        Yonetmen = item.Yonetmen,
                        filmImg = item.FilmImg
                    });
                }
                //admin sayfasında kullanıcıları listelemek için kullan 


                return View("MoviesList", model2);
            }
            return View(model);
        }

        public IActionResult MovieDesign()
        {
            return View();
        }

        public IActionResult SeriesDesign()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateKullanici()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateKullanici(CreateKullaniciVM model)
        {
            if (ModelState.IsValid)
            {
                string md5Crypto = _configuration.GetValue<string>("AppSettings:MD5Crypto");
                string cryptoPassword = model.KullaniciSifre + md5Crypto;
                string hashedPassword = cryptoPassword.MD5();

                KullaniciDB kullanici = new KullaniciDB
                {
                    kullaniciAdi = model.kullaniciAdi,
                    KullaniciSoyadi = model.kullaniciSoyadi,
                    kullaniciSifre = hashedPassword,
                    kullaniciDogum = model.kullaniciDogum,
                    kullaniciEmail = model.kullaniciEmail,
                    Phone= model.Phone,
                    Role=model.Role,
                    Locked=model.Locked,
                };
                _databaseContex.Add(kullanici);
                _databaseContex.SaveChanges();

                List<KullaniciDB> kullanicilar = _databaseContex.Kullanici.ToList();
                List<KullaniciVM> model2 = new List<KullaniciVM>();

                // _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
                //   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

                foreach (KullaniciDB item in kullanicilar)
                {
                    model2.Add(new KullaniciVM
                    {
                        Id = item.Id,
                        kullaniciAdi = item.kullaniciAdi,
                        KullaniciSoyadi = item.KullaniciSoyadi,
                        kullaniciEmail = item.kullaniciEmail,
                        Role = item.Role,
                        KayitTarih = item.KayitTarih,
                        Locked = item.Locked,
                    });
                }
                //admin sayfasında kullanıcıları listelemek için kullan 


                return View("Index",model2);
            }
            return View(model);
        }


        public IActionResult EditKullanici(int Id)
        {
            KullaniciDB kullanici = _databaseContex.Kullanici.Find(Id);
            EditKullaniciVM editKullanici =new EditKullaniciVM();
            editKullanici.kullaniciAdi = kullanici.kullaniciAdi;
            editKullanici.kullaniciSoyadi = kullanici.KullaniciSoyadi;
            editKullanici.kullaniciEmail = kullanici.kullaniciEmail;
            editKullanici.kullaniciDogum= kullanici.kullaniciDogum;
            editKullanici.Locked = kullanici.Locked;
            editKullanici.Phone = kullanici.Phone;
            editKullanici.Role = kullanici.Role;
            
            
            return View(editKullanici);
        }

       [HttpPost]
       public IActionResult EditKullanici(int Id,EditKullaniciVM model)
       {
            if (ModelState.IsValid)
            {
                KullaniciDB kullanici = _databaseContex.Kullanici.Find(Id);
                kullanici.kullaniciAdi = model.kullaniciAdi;
                kullanici.KullaniciSoyadi = model.kullaniciSoyadi;
                kullanici.kullaniciEmail = model.kullaniciEmail;
                kullanici.kullaniciDogum = model.kullaniciDogum;
                kullanici.Locked = model.Locked;
                kullanici.Phone = model.Phone;
                kullanici.Role = model.Role;

                _databaseContex.SaveChanges();


                List<KullaniciDB> kullanicilar = _databaseContex.Kullanici.ToList();
                List<KullaniciVM> model2 = new List<KullaniciVM>();

                // _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
                //   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

                foreach (KullaniciDB item in kullanicilar)
                {
                    model2.Add(new KullaniciVM
                    {
                        Id = item.Id,
                        kullaniciAdi = item.kullaniciAdi,
                        KullaniciSoyadi = item.KullaniciSoyadi,
                        kullaniciEmail = item.kullaniciEmail,
                        Role = item.Role,
                        KayitTarih = item.KayitTarih,
                        Locked = item.Locked,
                    });
                }
                //admin sayfasında kullanıcıları listelemek için kullan 


                return View("Index", model2);
                
            }
            return View(model);
       }
        
        public IActionResult DeleteKullanici(int id)
        {
            KullaniciDB kullanici = _databaseContex.Kullanici.Find(id);
            if (kullanici != null)
            {
                _databaseContex.Kullanici.Remove(kullanici);
                _databaseContex.SaveChanges();

                List<KullaniciDB> kullanicilar = _databaseContex.Kullanici.ToList();
                List<KullaniciVM> model2 = new List<KullaniciVM>();

                // _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
                //   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

                foreach (KullaniciDB item in kullanicilar)
                {
                    model2.Add(new KullaniciVM
                    {
                        Id = item.Id,
                        kullaniciAdi = item.kullaniciAdi,
                        KullaniciSoyadi = item.KullaniciSoyadi,
                        kullaniciEmail = item.kullaniciEmail,
                        Role = item.Role,
                        KayitTarih = item.KayitTarih,
                        Locked = item.Locked,
                    });
                }
                //admin sayfasında kullanıcıları listelemek için kullan 
                return View("Index", model2);

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
