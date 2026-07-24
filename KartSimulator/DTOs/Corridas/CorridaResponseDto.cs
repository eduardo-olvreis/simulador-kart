using KartSimulator.Entities;

namespace KartSimulator.DTOs.Corridas
{
    public class CorridaResponseDto
    {
        public int Id { get; set; }
        public string NomePista { get; set; }
        public int NumeroVoltas { get; set; }
        public DateTime DataCorrida { get; set; }
        public List<PodioItemDto> Podio { get; set; } = new();
    }
}
