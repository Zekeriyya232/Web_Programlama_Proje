using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProje1.Entity
{
    [Table("Comment")]
    public class CommentDB
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int filmId { get; set; }
		[Required]
		public int kullaniciId { get; set; }
		[Required]
		public string comment { get; set; }
	}
}
