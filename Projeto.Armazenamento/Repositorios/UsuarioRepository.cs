using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Projeto.Entidades;
using Projeto.Armazenamento.Contratos;
using Projeto.Armazenamento.Configuracoes;

namespace Projeto.Armazenamento.Repositorios
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {


        public bool EmailExistente(string email)
        {
            return context.Usuario
                .Where(u => u.Email.Equals(email)).Count() > 0;
        }


        public bool EmailExistente(string email, int idUsuario)
        {
            return context.Usuario
                .Where(u => u.Email.Equals(email) && u.IdUsuario != idUsuario).Count() > 0;
        }


        public Usuario ObterPorEmailSenha(string email, string senha)
        {
            return context.Usuario
                .FirstOrDefault(u => u.Email.Equals(email) && u.Senha.Equals(senha));
        }
    }
}
