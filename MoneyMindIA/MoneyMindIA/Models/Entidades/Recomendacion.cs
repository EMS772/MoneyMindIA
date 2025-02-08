﻿using System.ComponentModel.DataAnnotations;

namespace MoneyMindIA.Models.Entidades
{
    public class Recomendacion
    {
        [Key]
        public int RecomendacionId { get; set; }

        [Required]
        [StringLength(1500)]
        public string Mensaje { get; set; } // Texto de la recomendación

        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;

        // Relaciones
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
