using System;
using System.ComponentModel.DataAnnotations;

namespace BlogWeb.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(255)]
        public string Resumo { get; set; }

        public string Categoria { get; set; }
        public bool Publicado { get; set; }
        public DateTime? DataPublicacao { get; set; }

        public string AutorId { get; set; }
        public virtual Usuario Autor { get; set; }

    }
}