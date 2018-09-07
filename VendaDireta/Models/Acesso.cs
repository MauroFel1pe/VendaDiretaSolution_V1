using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VendaDireta.Models
{
    public class Acesso
    {
        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "Informe o seu e-mail")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
        [Required(ErrorMessage = "Informe a sua senha")]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
