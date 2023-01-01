using System.ComponentModel.DataAnnotations;

namespace WebProje1.Models
{
    public class MovieVM
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string FilmAdi { get; set; }
        public string Aciklama { get; set; }
        public string Yonetmen { get; set; }
        public string filmImg { get; set; }

        public int KategoriId { get; set; }

        public int FilmSure { get; set; }

        public string[] oyuncular { get; set; }
    }
}
