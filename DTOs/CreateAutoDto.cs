using System.ComponentModel.DataAnnotations;

namespace ConcesionarioApi.DTOs
{
    public class CreateAutoDto
    {
        [Required] public string Marca { get; set; }
        [Required] public string Modelo { get; set; }
        [Range(1900, 2100)] public int Anio { get; set; }
        [Range(0, double.MaxValue)] public decimal Precio { get; set; }
    }
}
