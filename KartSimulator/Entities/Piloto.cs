using System.ComponentModel.DataAnnotations;

namespace KartSimulator.Entities
{
    public class Piloto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' não pode ser vazio.")]
        [MaxLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
        public string Nome { get; set; }

        [Range(18, 120, ErrorMessage = "A idade deve estar entre o intervalo de 18 a 120.")]
        public int Idade { get; set; }

        public int CorridasVencidas { get; set; } = 0;

        [Range(0, 100, ErrorMessage = "A Habilidade de Curva deve ser entre o intervalo de 0 a 10.")]
        public int HabilidadeCurvas { get; set; }

        [Range(0, 10, ErrorMessage = "A Consistência deve estar entre o intervalo de 0 a 10.")]
        public int Consistencia { get; set; }

        public int VeiculoId { get; set; }

        public Veiculo Veiculo { get; set; }
    }
}
