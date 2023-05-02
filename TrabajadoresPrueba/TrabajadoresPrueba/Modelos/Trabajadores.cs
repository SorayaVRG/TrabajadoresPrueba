using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresPrueba.Modelos
{
    public class Trabajadores
    {
        public int Id { get; set; }
        
        [DisplayName("Tipo Documento")]
        public string TipoDocumento { get; set; } = string.Empty;
        
        [DisplayName("Numero Documento")]
        public string NumeroDocumento { get; set;} = string.Empty;
        
        [DisplayName("Nombres")]
        public string Nombres { get; set; } = string.Empty;
        
        [DisplayName("Sexo")]
        public string Sexo { get; set;} = string.Empty;

        [DisplayName("Departamento")]
        public int IdDepartamento { get; set;}

        [DisplayName("Provincia")]
        public int IdProvincia { get; set;}

        [DisplayName("Distrito")]
        public int IdDistrito { get; set;}

        [DisplayName("Ficha")]
        public string? Ficha { get; set; }
        [NotMapped]
        public IFormFile FichaIFormFile { get; set; }
        public string FichaURL => Ficha == null ? "" : Ficha;

        public string FichaURL2
        {
            get
            {
                if (string.IsNullOrEmpty(Ficha))
                {
                    return "";
                }
                else
                {
                    return $"https://localhost:7182/{Ficha}";
                }
            }
        }

        [DisplayName("Foto")]
        public string? Foto { get; set; }
        [NotMapped]
        public IFormFile FotoIFormFile { get; set;}
        public string FotoURL => Foto == null ? "" : Foto;

        public string FotoURL2
        {
            get
            {
                if (string.IsNullOrEmpty(Foto))
                {
                    return "";
                }
                else
                {
                    return $"https://localhost:7182/{Foto}";
                }
            }
        }


    }
}
