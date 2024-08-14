using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CategoriasMvc.Models
{
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }
        [Required(ErrorMessage ="A name is required!")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "A description is required!")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "A price is required!")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "An image is required!")]
        [Display(Name = "Image path")]
        public string? ImagemUrl { get; set; }
        [Display(Name = "Categoria Id")]
        public int CategoriaId { get; set; }
    }
}

