using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrabajadoresPrueba.Modelos
{
    public class Departamento
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Departamento")]
        public string NombreDepartamento { get; set; } = string.Empty;
    }
}
