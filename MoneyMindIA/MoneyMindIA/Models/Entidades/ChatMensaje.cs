using System.ComponentModel.DataAnnotations;

namespace MoneyMindIA.Models.Entidades
{
    public class ChatMensaje
    {
        [Key]
        public int MensajeId { get; set; }

        [Required]
        public string Contenido { get; set; } // Contenido del mensaje

        [Required]
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;

        [Required]
        public bool EsUsuario { get; set; } // True si el mensaje es del usuario, False si es de la IA

        // Relaciones
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
