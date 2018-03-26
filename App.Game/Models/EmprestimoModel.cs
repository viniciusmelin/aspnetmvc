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
        public int Solicitante_id { get; set; }


        
        //[Column(Order = 2)]
        public int Solicitado_id { get; set; }

        [ForeignKey("Game")]
        [Column(Order = 1)]
        public int Game_id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Emprestimo")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime Data_emprestimo { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Data Devolução")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime Data_devolucao { get; set; }

        [Display(Name = "Data Devolvido")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
 
        public Nullable<DateTime> Data_devolvido { get; set; }

        [ForeignKey("Solicitado_id")]
        public virtual PessoaModel PessoaSolicitada { get; set; }

        [ForeignKey("Solicitante_id")]
        public virtual PessoaModel PessoaSolicitante { get; set; }
        public virtual GameModel Game { get; set; }
    }

}