using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Game.Models
{
    [Table("emprestimo")]
    public class EmprestimoModel
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Código")]
        public int Id { get; set; }

        //[ForeignKey("Pessoa")]
        //[Column(Order =1)]
        //public int solicitante_id { get; set; }


        //[ForeignKey("Pessoa")]
        //[Column(Order = 2)]
        //public int solicitado_id { get; set; }

        [ForeignKey("Game")]
        [Column(Order = 1)]
        public int Game_id { get; set; }

        [Display(Name = "Data Emprestimo")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyy}")]
        public DateTime Data_emprestimo { get; set; } = DateTime.Now;

        [Display(Name = "Data Devolução")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyy}")]
        public DateTime Data_devolucao { get; set; }

        [Display(Name = "Data Devolvido")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyy}")]
        public DateTime Data_devolvido { get; set; }

        public virtual PessoaModel PessoaSolicitada { get; set; }
        public virtual PessoaModel PessoaSolicitante { get; set; }
        public virtual GameModel Game { get; set; }
    }
}