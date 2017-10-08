using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Projeto.Entidades.Tipos;


namespace Projeto.Web.Areas.AreaRestrita.Models
{
    public class UsuarioViewModelConsulta
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public SimNao Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Senha { get; set; }
    }
}