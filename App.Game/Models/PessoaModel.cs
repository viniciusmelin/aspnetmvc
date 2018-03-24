using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Game.Models
{
    [Table("pessoa")]
    public class PessoaModel
    {
        public PessoaModel()
        {
            PessoaMe = new HashSet<PessoaModel>();
            PessoaFrinds = new HashSet<PessoaModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "O tamanho do campo de conter no minímo 2 e máximo 50 caracteres", MinimumLength = 2)]
        [Required(ErrorMessage = "O nome é obrigatório!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }


        [StringLength(50, ErrorMessage = "O tamanho do campo de conter no minímo 2 e máximo 50 caracteres", MinimumLength = 2)]
        [Required]
        [Display(Name = "Sobrenome")]
        public string sobrenome { get; set; }

        [Required(ErrorMessage = "Ideda é obrigatório!")]
        public int Idade { get; set; }


       
        [Column(Order = 1)]
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<PessoaModel> PessoaMe { get; set; }
        public virtual ICollection<PessoaModel> PessoaFrinds { get; set; }


    }
}