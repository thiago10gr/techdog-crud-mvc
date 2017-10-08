using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Projeto.Entidades.Tipos;

namespace Projeto.Entidades
{
    public class Usuario
    {
        public virtual int IdUsuario { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual SimNao Ativo { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual string Senha { get; set; }
    }
}
