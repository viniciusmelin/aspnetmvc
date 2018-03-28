using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.DAL
{
    [Table("game")]
    public class GameModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código")]
        [Column(Order = 0)]
        public int GameId { get; set; }

        [StringLength(50, ErrorMessage = "O tamanho do campo de conter no minímo 2 e máximo 50 caracteres", MinimumLength = 2)]
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}