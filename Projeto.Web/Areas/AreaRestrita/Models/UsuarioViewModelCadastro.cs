using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Projeto.Web.Areas.AreaRestrita.Models
{
    public class UsuarioViewModelCadastro
    {
        [MinLength(3, ErrorMessage = "O nome deve conter no mínimo 4 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome deve conter no máximo 12 caracteres.")]
        [Required(ErrorMessage = "Informe o nome.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [MinLength(3, ErrorMessage = "O email deve conter no mínimo 4 caracteres.")]
        [MaxLength(100, ErrorMessage = "O email conter no máximo 12 caracteres.")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required(ErrorMessage = "Informe o email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Senha inválida. A senha deve conter apenas letras e números.")]
        [MinLength(4, ErrorMessage = "A senha deve conter no mínimo 4 caracteres.")]
        [MaxLength(12, ErrorMessage = "A senha deve conter no máximo 12 caracteres.")]
        [Required(ErrorMessage = "Informe a senha.")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas são diferentes.")]
        [Required(ErrorMessage = "Repita a senha.")]
        [Display(Name = "*Repita a senha")]
        public string RepitaSenha { get; set; }
    }
}