using System.ComponentModel;

namespace TrabajadoresPrueba.Modelos
{
    public class Provincia
    {
        public int Id { get; set; }

        [DisplayName("Departamento")]
        public int IdDepartamento { get; set; }

        [DisplayName("Provincia")]
        public string NombreProvincia { get; set; } =string.Empty;
    }
}
