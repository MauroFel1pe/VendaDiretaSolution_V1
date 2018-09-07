using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VendaDireta.Models
{
    public class Produto
    {
        [Key]
        [Display(Name = "Id")]
        public int ProdutoId { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [MaxLength(200)]
        [Required]
        public string Nome { get; set; }
        [Display(Name = "Preço")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }
        public bool Vendido { get; set; } = false;
    }
}
