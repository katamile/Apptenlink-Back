using Apptelink_Back.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apptelink_Back.Entities
{
    public partial class Usuario
    {
        [Required(ErrorMessage = "El código de Usuario no puede ser nulo.")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El código de Usuario debe ser un número entero mayor a cero.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El username no puede ser nulo.")]
        [StringLength(50, ErrorMessage = "El username debe contener máximo 50 caracteres.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña no puede ser nula.")]
        [StringLength(200, ErrorMessage = "La contraseña debe contener máximo 150 caracteres.")]
        public string Contraseña { get; set; } = null!;

        [RegularExpression("^[0-3]\\d*$", ErrorMessage = "El campo intentosfallidos debe ser un número entero mayor a cero.")]
        public int? IntentosFallidos { get; set; }

        [Required(ErrorMessage = "El estado no puede ser nulo.")]
        [RegularExpression("^[AI]$", ErrorMessage = $"El campo estado debe ser {Globales.ESTADO_ACTIVO} o {Globales.ESTADO_INACTIVO}.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "La fecha de estado no puede ser nula.")]
        public DateTime? FechaCreacion { get; set; }
    }
}
