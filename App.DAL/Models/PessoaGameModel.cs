using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.DAL
{
    [Table("pessoa_game")]
    public class PessoaGameModel
    {
        [Key]
        [ForeignKey("Pessoa")]
        [Column(Order = 0)]
        public int pessoa_pessoa_id { get; set; }

        [Key]
        [ForeignKey("Game")]
        [Column(Order = 1)]
        public int game_game_id { get; set; }

        public Boolean Emprestado { get; set; }

        public virtual PessoaModel Pessoa { get; set; }
                
        public virtual GameModel Game { get; set; }
    }
}