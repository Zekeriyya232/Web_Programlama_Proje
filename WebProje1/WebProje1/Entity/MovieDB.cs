using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProje1.Entity
{
    [Table("Movie")]
    public class MovieDB
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string FilmAdi { get; set; }
        [Required, StringLength(20)]
        public string Description { get; set; }

        public int KategoriId { get; set; }

        public int FilmSure { get; set; }



    }
}
