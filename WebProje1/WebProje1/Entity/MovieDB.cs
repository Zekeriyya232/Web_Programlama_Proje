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
        public string Description { get; set; }
        public string Director { get; set; }
        public int KategoriId { get; set; }

        public int FilmSure { get; set; }
        public string ImageURL { get; set; } = "no_img.jpg";
    }
}
