using App.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Game.repository
{
    public interface IContextDB:IDisposable
    {
        IRepositorio<PessoaModel> PessoaRepositorio { get; }

        void Commit();
    }
}
