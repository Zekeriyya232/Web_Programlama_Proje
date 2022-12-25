using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProje1.Entity;
using WebProje1.Models;

namespace WebProje1.Controllers
{
    [Authorize(Roles ="admin")]   //roles="admin,manager,user"
    public class AdminController : Controller
    {
        private readonly DatabaseContex _databaseContex;

        public AdminController(DatabaseContex databaseContex)
        {
            _databaseContex = databaseContex;
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
                });
            }
            //admin sayfasında kullanıcıları listelemek için kullan 
            

            return View();
        }

        public IActionResult MovieDesign()
        {
            return View();
        }

        public IActionResult SeriesDesign()
        {
            return View();
        }

        public IActionResult CreateKullanici()
        {

            return View();
        }
    }
}
