using App.Game.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace App.Game.repository
{
    public class Repository<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //DbSet<TEntity> db;

        //public Repository(ApplicationDbContext db)
        //{
        //    this.db = db;
        //}
        public void Adicionar(TEntity obj)
        {
            db.Set<TEntity>().Add(obj);
        }

        public void Atualizar(TEntity obj)
        {
           db.Entry(obj).State = EntityState.Modified;
        }

        public IQueryable<TEntity> BuscarOpcao(Expression<Func<TEntity, bool>> buscar)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> BuscarTodos()
        {
            return db.Set<TEntity>();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void Excluir(TEntity obj)
        {
            db.Set<TEntity>().Remove(obj);
        }
    }
}