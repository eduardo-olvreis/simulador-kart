using System.ComponentModel.DataAnnotations;

namespace KartSimulator.DTOs.Corridas
{
    public class CorridaCreateDto
    {
        [Required(ErrorMessage = "Campo 'Nome da Pista' não pode ser nulo.")]
        [MaxLength(60, ErrorMessage = "Campo 'Nome da Pista' não pode ter mais de 60 caracteres.")]
        public string NomePista { get; set; }

        [Range(10, 50, ErrorMessage = "Campo 'Numero de Voltas' deve estar entre o intervalo de 10 a 50.")]
        public int NumeroVoltas { get; set; }

        [Required(ErrorMessage = "Campo 'Pilotos Ids' não pode ser nulo.")]
        [MinLength(2, ErrorMessage = "Campo 'Pilotos Ids' tem que ser no minimo 2")]
        [MaxLength(12, ErrorMessage = "Campo 'Pilotos Ids' tem que ser no máximo 12")]
        public List<int> PilotosIds { get; set; } = new();
    }
}
