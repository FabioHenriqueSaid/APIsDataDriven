using System.ComponentModel.DataAnnotations;

namespace ApiDataDriven.Domain.Entity
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigátorio")]
        [MaxLength(60, ErrorMessage = "Este campo deve ter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve ter entre 3 e 60 caracteres")]
        public string Title { get; set; }
    }
}