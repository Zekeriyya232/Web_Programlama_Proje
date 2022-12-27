using Microsoft.EntityFrameworkCore;
using WebProje1.Models;

namespace WebProje1.Entity
{
    public class DatabaseContex:DbContext
    {
        public DatabaseContex(DbContextOptions options) : base(options)
        {
        }
        public DbSet<KullaniciDB> Kullanici { get; set; }

    }

}
