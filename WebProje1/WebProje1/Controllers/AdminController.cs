using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using WebProje1.Entity;
using WebProje1.Models;

namespace WebProje1.Controllers
{
	[Authorize(Roles = "admin")]   //roles="admin,manager,user"
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
			List<KullaniciDB> kullanicilar = _databaseContex.Kullanici.ToList();
			List<KullaniciVM> model = new List<KullaniciVM>();

			// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
			//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

			foreach (KullaniciDB item in kullanicilar)
			{
				model.Add(new KullaniciVM
				{
					Id = item.Id,
					kullaniciAdi = item.kullaniciAdi,
					KullaniciSoyadi = item.KullaniciSoyadi,
					kullaniciEmail = item.kullaniciEmail,
					Role = item.Role,
					KayitTarih = item.KayitTarih,
					Locked = item.Locked,
					Phone = item.Phone,
				});
			}
			//admin sayfasında kullanıcıları listelemek için kullan 


			return View(model);
		}

		public IActionResult Movie()
		{
			List<MovieDB> filmlist = _databaseContex.Film.ToList();
			List<MovieVM> model = new List<MovieVM>();

			// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
			//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

			foreach (MovieDB item in filmlist)
			{
				model.Add(new MovieVM
				{
					Id = item.Id,
					FilmAdi = item.FilmAdi,
					Aciklama = item.Description,
					KategoriId = item.KategoriId,
					FilmSure = item.FilmSure,
					Yonetmen = item.Director,
					ResimURL = item.ImageURL
				});
			}
			//admin sayfasında kullanıcıları listelemek için kullan 


			return View(model);
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
					Phone = model.Phone,
					Role = model.Role,
					Locked = model.Locked,
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


				return View("Index", model2);
			}
			return View(model);
		}


		public IActionResult EditKullanici(int Id)
		{
			KullaniciDB kullanici = _databaseContex.Kullanici.Find(Id);
			EditKullaniciVM editKullanici = new EditKullaniciVM();
			editKullanici.kullaniciAdi = kullanici.kullaniciAdi;
			editKullanici.kullaniciSoyadi = kullanici.KullaniciSoyadi;
			editKullanici.kullaniciEmail = kullanici.kullaniciEmail;
			editKullanici.kullaniciDogum = kullanici.kullaniciDogum;
			editKullanici.Locked = kullanici.Locked;
			editKullanici.Phone = kullanici.Phone;
			editKullanici.Role = kullanici.Role;


			return View(editKullanici);
		}
		public IActionResult EditMovie(int Id)
		{
			MovieDB movie = _databaseContex.Film.Find(Id);
			EditMovieVM editMovie = new EditMovieVM();
			editMovie.FilmAdi = movie.FilmAdi;
			editMovie.Aciklama = movie.Description;
			editMovie.ResimURL = movie.ImageURL;
			editMovie.KategoriId = movie.KategoriId;
			editMovie.FilmSure = movie.FilmSure;
			editMovie.Yonetmen = movie.Director;


			return View(editMovie);
		}
		[HttpPost]
		public IActionResult EditMovie(int id, EditMovieVM model)
		{
			if (ModelState.IsValid)
			{
				MovieDB movie = _databaseContex.Film.Find(id);
				movie.FilmAdi = model.FilmAdi;
				movie.Description = model.Aciklama;
				movie.ImageURL = model.ResimURL;
				movie.KategoriId = model.KategoriId;
				movie.FilmSure = model.FilmSure;
				movie.Director = model.Yonetmen;

				_databaseContex.SaveChanges();


				List<MovieDB> filmlist = _databaseContex.Film.ToList();
				List<MovieVM> model2 = new List<MovieVM>();

				// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
				//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

				foreach (MovieDB item in filmlist)
				{
					model2.Add(new MovieVM
					{
						Id = item.Id,
						FilmAdi = item.FilmAdi,
						Aciklama = item.Description,
						KategoriId = item.KategoriId,
						FilmSure = item.FilmSure,
						Yonetmen = item.Director,
						ResimURL = item.ImageURL
					});
				}
				//admin sayfasında kullanıcıları listelemek için kullan 


				return View("Index", model2);

			}
			return View(model);
		}


		[HttpPost]
		public IActionResult EditKullanici(int id, EditKullaniciVM model)
		{
			if (ModelState.IsValid)
			{
				KullaniciDB kullanici = _databaseContex.Kullanici.Find(id);
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
		[HttpPost]
		public IActionResult CreateMovie(CreateMovieVM model)
		{
			if (ModelState.IsValid)
			{
				MovieDB movie = new MovieDB
				{
					FilmAdi=model.FilmAdi,
					Director=model.Yonetmen,
					Description=model.Aciklama,
					FilmSure=model.FilmSure,
					KategoriId=model.KategoriId,
					ImageURL=model.ResimURL

				};
				_databaseContex.Add(movie);
				_databaseContex.SaveChanges();
				List<MovieDB> filmlist = _databaseContex.Film.ToList();
				List<MovieVM> model2 = new List<MovieVM>();

				// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
				//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

				foreach (MovieDB item in filmlist)
				{
					model2.Add(new MovieVM
					{
						Id = item.Id,
						FilmAdi = item.FilmAdi,
						Aciklama = item.Description,
						KategoriId = item.KategoriId,
						FilmSure = item.FilmSure,
						Yonetmen = item.Director,
						ResimURL = item.ImageURL
					});
				}
				//admin sayfasında kullanıcıları listelemek için kullan 


				return View("Index", model2);
			}
			return View(model);
		}
		public IActionResult DeleteMovie(int id)
		{
			MovieDB movie = _databaseContex.Film.Find(id);
			if (movie != null)
			{
				_databaseContex.Film.Remove(movie);
				_databaseContex.SaveChanges();

				List<MovieDB> filmlist = _databaseContex.Film.ToList();
				List<MovieVM> model = new List<MovieVM>();

				// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
				//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

				foreach (MovieDB item in filmlist)
				{
					model.Add(new MovieVM
					{
						Id = item.Id,
						FilmAdi = item.FilmAdi,
						Aciklama = item.Description,
						KategoriId = item.KategoriId,
						FilmSure = item.FilmSure,
						Yonetmen = item.Director,
						ResimURL = item.ImageURL
					});
				}
				//admin sayfasında kullanıcıları listelemek için kullan 


				return View(model);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
