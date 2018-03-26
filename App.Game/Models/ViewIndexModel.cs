using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Game.Models
{
    public class ViewIndexModel
    {
        public virtual IEnumerable<PessoaModel> PessoaModel { get; set; }
        public virtual IEnumerable<PessoaGameModel> PessoaGame { get; set; }
        public virtual ICollection<GameModel> Game { get; set; }

        public virtual IEnumerable<EmprestimoModel> Emprestimo { get; set; }
    }
}