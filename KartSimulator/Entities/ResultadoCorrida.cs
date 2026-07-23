using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartSimulator.Entities
{
    public class ResultadoCorrida
    {
        [Key]
        public int Id { get; set; }

        public int CorridaId { get; set; }

        [ForeignKey(nameof(CorridaId))]
        public Corrida? Corrida { get; set; }

        public int PilotoId { get; set; }

        [ForeignKey(nameof(PilotoId))]
        public Piloto? Piloto { get; set; }

        public int Posicao { get; set; }

        public double TempoEmSegundos { get; set; }
    }
}
