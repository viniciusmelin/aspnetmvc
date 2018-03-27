using System;
using System.Collections;
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



        [Key]
        [Column(Order = 0)]
        //[Required]
        public int PessoaMeId { get; set; }


        [Key]
       [Column(Order = 1)]
        //[Required]
        public int PessoaFriendsId { get; set; }


        [ForeignKey("PessoaMeId")]
        public virtual PessoaModel PessoaMe { get; set; }


        [ForeignKey("PessoaFriendsId")]
        public virtual PessoaModel PessoaFriends { get; set; }


    }
}