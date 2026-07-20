using System.ComponentModel.DataAnnotations;

namespace KartSimulator.Entities
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Modelo' não pode ser vazio.")]
        [MaxLength(60, ErrorMessage = "Modelo não pode ultrapassar 60 caracteres.")]
        public string Modelo { get; set; }

        [Range(45, 70, ErrorMessage = "A Velocidade Máxima deve ser entre 45 e 70.")]
        public int VelocidadeMaxima { get; set; }
        
        public Piloto? Piloto { get; set; }
    }
}
