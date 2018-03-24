using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Game.Models;

namespace App.Game.repository
{
    public class DbContextImplementar : IContextDB,IDisposable
    {
        private ApplicationDbContext _contexto = null;
        public IRepositorio<PessoaModel> PessoaRepositorio => throw new NotImplementedException();

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}