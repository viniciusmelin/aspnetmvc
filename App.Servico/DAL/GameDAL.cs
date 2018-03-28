using App.Servico.Persistencia;
using App.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servico.DAL
{
    public class GameDAL
    {
        private GameContext context = new GameContext();



        public IQueryable ObterGameTodosGames()
        {
            return context.Game;
        }
    }
}
