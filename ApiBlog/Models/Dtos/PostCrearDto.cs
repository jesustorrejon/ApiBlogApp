using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models.Dtos
{
    public class PostCrearDto
    {

        [Required(ErrorMessage = "El titulo es obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }
        public string RutaImagen { get; set; }

        [Required(ErrorMessage = "Las etiquetas son obligatorias")]
        public string Etiquetas { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
