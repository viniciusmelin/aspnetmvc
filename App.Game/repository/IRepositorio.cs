using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Game.repository
{
    public interface IRepositorio<TEntity> where TEntity:class
    {

        IQueryable<TEntity> BuscarTodos();
        IQueryable<TEntity> BuscarOpcao(Expression<Func<TEntity, bool>> buscar);

        void Atualizar(TEntity obj);
        void Adicionar(TEntity obj);

        void Excluir(TEntity obj);
    }
}
