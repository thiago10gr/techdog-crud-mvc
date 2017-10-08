using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Armazenamento.Contratos
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Inserir(TEntity obj);

        void Atualizar(TEntity obj);

        void Excluir(TEntity obj);

        List<TEntity> ListarTodos();

        TEntity ObterPorId(int id);

        void BeginTransaction();

        void Commit();

        void Rollback();

        void Dispose();
    }
}
