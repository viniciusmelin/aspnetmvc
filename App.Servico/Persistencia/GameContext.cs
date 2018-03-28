using App.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servico.Persistencia
{
    public class GameContext : DbContext
    {



        public DbSet<PessoaModel> Pessoa { get; set; }

        public DbSet<GameModel> Game { get; set; }
        public DbSet<PessoaGameModel> PessoaGame { get; set; }
        public DbSet<AmigoModel> PessoaAmigo { get; set; }

        public DbSet<EmprestimoModel> Emprestismo { get; set; }
    }

}
