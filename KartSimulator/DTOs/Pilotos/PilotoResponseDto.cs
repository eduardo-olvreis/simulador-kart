using System.ComponentModel.DataAnnotations;

namespace KartSimulator.DTOs.Pilotos
{
    public class PilotoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int CorridasVencidas { get; set; }
        public int HabilidadeCurvas { get; set; }
        public int Consistencia { get; set; }
    }
}
