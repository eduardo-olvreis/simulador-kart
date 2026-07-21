using System.ComponentModel.DataAnnotations;

namespace KartSimulator.DTOs.Pilotos
{
    public class PilotoCreateDto
    {
        [Required(ErrorMessage = "Campo 'Nome' não pode ser nulo.")]
        [MaxLength(100, ErrorMessage = "Campo 'Nome' não pode ser maior que 100 caracteres.")]
        public string Nome { get; set; }

        [Range(18, 120, ErrorMessage = "Campo 'Idade' deve estar entre o intervalo de 18 a 120.")]
        public int Idade { get; set; }

        [Range(0, 10, ErrorMessage = "Campo 'Habilidade Curvas' deve estar entre o intervalo de 0 a 10.")]
        public int HabilidadeCurvas {  get; set; }

        [Range(0, 10, ErrorMessage = "Campo 'Consistência' deve estar entre o intervalo de 0 a 10.")]
        public int Consistencia { get; set; }
    }
}
