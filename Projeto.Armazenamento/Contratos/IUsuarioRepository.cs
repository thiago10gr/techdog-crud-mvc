using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Projeto.Entidades;


namespace Projeto.Armazenamento.Contratos
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario ObterPorEmailSenha(string email, string senha);

        bool EmailExistente(string email);

        bool EmailExistente(string email, int idUsuario);
    }
}
