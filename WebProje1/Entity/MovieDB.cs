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
        
        public string Aciklama { get; set; }

        public int KategoriId { get; set; }

        public int FilmSure { get; set; }
        public string[] Oyuncular { get; set; }
        public string Yonetmen { get; set; }
        public string FilmImg { get; set; } = "no_img.jpg";


    }
}
