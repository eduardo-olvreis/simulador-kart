namespace KartSimulator.DTOs.Corridas
{
    public class PodioItemDto
    {
        public int Posicao { get; set; }
        public int PilotoId { get; set; }
        public string NomePiloto { get; set; }
        public string ModeloVeiculo { get; set; }
        public double TempoEmSegundos { get; set; }
    }
}
