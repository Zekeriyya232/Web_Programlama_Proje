using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebProje1.Entity;
using WebProje1.Models;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using Microsoft.AspNetCore.Identity;

namespace WebProje1.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DatabaseContex _databaseContex;
        private readonly IConfiguration _configuration;

        public AccountController(DatabaseContex databaseContex, IConfiguration configuration)
        {
            _databaseContex = databaseContex;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                string md5Crypto = _configuration.GetValue<string>("AppSettings:MD5Crypto");
                string cryptoPassword = loginVM.KullaniciSifre + md5Crypto;
                string hashedPassword = cryptoPassword.MD5();

                KullaniciDB Kullanici = _databaseContex.Kullanici.SingleOrDefault(x => x.kullaniciEmail.ToLower() == loginVM.kullaniciEmail && x.kullaniciSifre == hashedPassword);



                if (Kullanici != null)
                {
                    if (Kullanici.Locked)
                    {
                        ModelState.AddModelError(nameof(loginVM.kullaniciEmail), "Kullanıcı hesabı askıda.");
                        return View(loginVM);
                    }
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, Kullanici.Id.ToString()));
                    claims.Add(new Claim("kullaniciAdi", Kullanici.kullaniciAdi));
                    claims.Add(new Claim(ClaimTypes.Role, Kullanici.Role));
                    claims.Add(new Claim(ClaimTypes.Email, Kullanici.kullaniciEmail ));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı ya da parola hatalı");
                }

                // return View("~/Views/Home/Index.cshtml");
            }
            return View(loginVM);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Kayit()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Kayit(signUpVM signUpVM)
        {
            if (ModelState.IsValid)
            {

                string md5Crypto = _configuration.GetValue<string>("AppSettings:MD5Crypto");
                string cryptoPassword = signUpVM.KullaniciSifre + md5Crypto;
                string hashedPassword = cryptoPassword.MD5();

                KullaniciDB kullanici = new KullaniciDB
                {

                    kullaniciAdi = signUpVM.kullaniciAdi,
                    KullaniciSoyadi = signUpVM.kullaniciSoyadi,
                    kullaniciSifre = hashedPassword,
                    kullaniciEmail = signUpVM.kullaniciEmail,
                    kullaniciDogum = signUpVM.kullaniciDogum,
                    Phone = signUpVM.Phone

                };


                _databaseContex.Kullanici.Add(kullanici);
                _databaseContex.SaveChanges();
                return View("Login");
            }
            return View(signUpVM);
        }

        public IActionResult Profile()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int intId = Convert.ToInt32(id);

            ViewBag.kullaniciId = intId;

            KullaniciDB kullanici = _databaseContex.Kullanici.Find(intId);
            ProfileVM kullaniciProfil= new ProfileVM();
            kullaniciProfil.kullaniciAdi = kullanici.kullaniciAdi;
            kullaniciProfil.kullaniciSoyadi = kullanici.KullaniciSoyadi;
            kullaniciProfil.kullaniciEmail = kullanici.kullaniciEmail;
            kullaniciProfil.kullaniciDogum = kullanici.kullaniciDogum;
            kullaniciProfil.Phone = kullanici.Phone;
            kullaniciProfil.ProfilImg = kullanici.ProfilImg;

            return View(kullaniciProfil);
        }


        public IActionResult EditProfile(int Id)
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
            editKullanici.ProfilImg = kullanici.ProfilImg;


            return View(editKullanici);
        }

        [HttpPost]
        public IActionResult EditProfile(int Id, EditKullaniciVM model)
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
                kullanici.ProfilImg = model.ProfilImg;

                _databaseContex.SaveChanges();

                //admin sayfasında kullanıcıları listelemek için kullan 


                return View("Profile", model);

            }
            return View(model);
        }


        [HttpPost]
        public IActionResult EditProfileImg([Required]IFormFile inputImg)
        {
            if (ModelState.IsValid)
            {
                string id2 = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int intId2 = Convert.ToInt32(id2);
                KullaniciDB kullanici2 = _databaseContex.Kullanici.SingleOrDefault(x => x.Id == intId2);

                string filename = $"p_{id2}.jpg";   //dosya adı isimlendirildi
                Stream stream = new FileStream($"wwwroot/kullaniciImg/{filename}", FileMode.OpenOrCreate);

                inputImg.CopyTo(stream);

                stream.Close();
                stream.Dispose();

                kullanici2.ProfilImg = filename;
                _databaseContex.SaveChanges();

                return RedirectToAction(nameof(Profile));

            }
            else {

                string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int intId = Convert.ToInt32(id);
                KullaniciDB kullanici = _databaseContex.Kullanici.SingleOrDefault(x => x.Id == intId);

                ProfileVM profileVM = new ProfileVM();
                profileVM.kullaniciAdi = kullanici.kullaniciAdi;
                profileVM.kullaniciSoyadi = kullanici.KullaniciSoyadi;
                profileVM.kullaniciEmail=kullanici.kullaniciEmail;
                profileVM.KayitTarih = kullanici.KayitTarih;
                profileVM.ProfilImg=kullanici.ProfilImg; 
                profileVM.kullaniciDogum=kullanici.kullaniciDogum;
                profileVM.Phone=kullanici.Phone;
                


                return View("Profile", profileVM);


            }

           
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
