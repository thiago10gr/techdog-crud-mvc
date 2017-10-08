using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Projeto.Armazenamento.Contratos;
using Projeto.Armazenamento.Configuracoes;
using System.Data.Entity;

namespace Projeto.Armazenamento.Repositorios
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable 
        where TEntity : class
    {


        protected readonly DataContext context;
        protected DbContextTransaction transaction;


        public BaseRepository()
        {
            this.context = new DataContext();
        }

        public virtual void Inserir(TEntity obj)
        {
            context.Entry(obj).State = EntityState.Added;
            context.SaveChanges();
        }

        public virtual void Atualizar(TEntity obj)
        {
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public virtual void Excluir(TEntity obj)
        {
            context.Entry(obj).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public virtual List<TEntity> ListarTodos()
        {
            return context.Set<TEntity>().ToList();
        }

        public virtual TEntity ObterPorId(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual void BeginTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }

        public virtual void Detach(TEntity obj)
        {
            context.Entry(obj).State = EntityState.Detached;
        }

        public virtual void Attach(TEntity obj)
        {
            context.Set<TEntity>().Attach(obj);
        }

        public virtual void Commit()
        {
            transaction.Commit();
        }

        public virtual void Rollback()
        {
            if (transaction != null)
                transaction.Rollback();
        }

        public virtual void Dispose()
        {
            context.Dispose();
        }
    }
}
