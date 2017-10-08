using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using Projeto.Entidades.Tipos;

namespace Projeto.Web.Areas.AreaRestrita.Models
{
    public class UsuarioViewModelExclusao
    {
        [Required(ErrorMessage = "Informe o id.")]
        public int IdUsuario { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string RepitaSenha { get; set; }
        public SimNao Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}