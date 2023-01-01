using System.ComponentModel.DataAnnotations;

namespace WebProje1.Models
{
	public class CommentVM
	{
        
        public int Id { get; set; }
        [Required]
        public int filmId { get; set; }
        [Required]
        public int kullaniciId { get; set; }
        [Required]
        public string comment { get; set; }
    }
}

