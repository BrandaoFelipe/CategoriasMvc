using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CategoriasMvc.Models
{
    public class CategoriaViewModel
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage="A name is required")]
        public string? Nome { get; set; }
        [Required]
        [DisplayName("Image")]
        public string? ImagemUrl { get; set; }
    }
}
