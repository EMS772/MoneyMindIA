using System.ComponentModel.DataAnnotations;

namespace MoneyMindIA.Models.Entidades
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string GoogleId { get; set; } // ID único de Google (para login con Gmail)

        public string PasswordHash { get; set; } // Contraseña encriptada (para registro normal)

        public bool EsRegistroNormal { get; set; } // True si es registro normal, False si es con Google

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;


        // Relaciones
        public Billetera Billetera { get; set; } // Un usuario tiene una billetera
        public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
        public ICollection<Meta> Metas { get; set; } = new List<Meta>();
        public ICollection<ChatMensaje> ChatMensajes { get; set; } = new List<ChatMensaje>();
        public ICollection<Recomendacion> Recomendaciones { get; set; } = new List<Recomendacion>();
    }
}
