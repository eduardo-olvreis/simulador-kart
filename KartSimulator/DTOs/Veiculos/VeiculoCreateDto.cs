using System.ComponentModel.DataAnnotations;

namespace KartSimulator.DTOs.Veiculos
{
    public class VeiculoCreateDto
    {
        [Required(ErrorMessage = "Campo 'Modelo' não pode ser nulo.")]
        [MaxLength(60, ErrorMessage = "Campo 'Modelo' não pode ter mais de 60 caracteres.")]
        public string Modelo { get; set; }

        [Range(45, 70, ErrorMessage = "Campo 'Velocidade Máxima' deve estar entre o intervalo de 45 a 70.")]
        public int VelocidadeMaxima { get; set; }
    }
}
