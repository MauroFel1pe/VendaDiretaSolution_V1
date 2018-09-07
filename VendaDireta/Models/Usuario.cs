using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VendaDireta.Models
{
    public class Usuario
    {
        [Display(Name = "Id")]
        public int UsuarioId { get; set; }
        [MaxLength(60)]
        [Required]
        public string Nome { get; set; }
        [MaxLength(100)]
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EMail { get; set; }
        [MaxLength(20)]
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public decimal Receita { get; set; } = 0;
    }
}
