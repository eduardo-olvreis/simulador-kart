using System.ComponentModel.DataAnnotations;

namespace KartSimulator.Entities
{
    public class Corrida
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Nome da Pista' não pode ser nulo.")]
        [MaxLength(60, ErrorMessage = "Campo 'Nome da Pista' não pode ter mais de 60 caracteres.")]
        public string NomePista { get; set; }

        [Range(10,50, ErrorMessage = "Campo 'Numero de Voltas' deve estar entre o intervalo de 10 a 50.")]
        public int NumeroVoltas { get; set; }

        public DateTime DataCorrida { get; set; } = DateTime.UtcNow;
    }
}
