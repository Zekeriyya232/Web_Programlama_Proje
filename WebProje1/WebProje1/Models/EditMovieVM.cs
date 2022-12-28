﻿using System.ComponentModel.DataAnnotations;

namespace WebProje1.Models
{
	public class EditMovieVM
	{
		[Required, StringLength(30)]
		public string FilmAdi { get; set; }
		public string Aciklama { get; set; }
		public string Yonetmen { get; set; }
		public string ResimURL { get; set; }

		public int KategoriId { get; set; }

		public int FilmSure { get; set; }
	}
}
}
