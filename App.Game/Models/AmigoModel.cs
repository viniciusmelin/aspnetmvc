using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Game.Models
{

    [Table("amigo")]
    public class AmigoModel
    {


        //[ForeignKey("Pessoa")]
        //[Key]
        //[Column(Order = 0)]
        //public int SolicitanteId { get; set; }



        [Key]
        [Column(Order = 0)]
        public int SolicitadoId { get; set; }

        ////[Key, Column(Order = 1)]
        ////[ForeignKey("SolicitanteId,SolicitadoId")]
        public ICollection<PessoaModel> PessoaFriends { get; set; }
        public ICollection<PessoaModel> Pessoa { get; set; }



    }
}