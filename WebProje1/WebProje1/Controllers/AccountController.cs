using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebProje1.Entity;
using WebProje1.Models;
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

                KullaniciDB Kullanici =_databaseContex.Kullanici.SingleOrDefault(x =>x.kullaniciEmail.ToLower()==loginVM.kullaniciEmail&&x.kullaniciSifre==hashedPassword);

                if (Kullanici != null)
                {
                    if (Kullanici.Locked)
                    {
                        ModelState.AddModelError(nameof(loginVM.kullaniciEmail), "Kullanıcı hesabı askıda.");
                        return View(loginVM);
                    }
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("ID", Kullanici.Id.ToString()));
                    claims.Add(new Claim("KullaniciAdi", Kullanici.kullaniciAdi ?? string.Empty));
                    claims.Add(new Claim("Role", Kullanici.Role));
                    claims.Add(new Claim("KullaniciEposta",Kullanici.kullaniciEmail ?? string.Empty));

                    ClaimsIdentity identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);

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
            if (ModelState.IsValid) {

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

                };


                _databaseContex.Kullanici.Add(kullanici);
                _databaseContex.SaveChanges();
                return View("Login");
            }
            return View(signUpVM);
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
    }
}
